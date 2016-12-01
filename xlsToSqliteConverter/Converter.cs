using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Lofle.XlsToSqliteConverter
{
	class Converter
	{
		static private Excel.Application _application = new Excel.Application();
		static private readonly string[] _extras = new[] { ".xls", ".xlsx" };

		static public void Directorys( string[] directorys )
		{
			for( int i = 0; i < directorys.Length; i++ )
			{
				Debug.Log( "D {0}/{1} {2}", i + 1, directorys.Length, directorys[i] );
				string[] files = Directory.GetFiles( directorys[i], "*.*", SearchOption.TopDirectoryOnly ).Where( s => _extras.Contains( Path.GetExtension( s ), StringComparer.OrdinalIgnoreCase ) ).ToArray();
				Files( files );
			}
		}

		static public void Files( string[] files )
		{
			for( int i = 0; i < files.Length; i++ )
			{
				Debug.Log( "F {0}/{1} {2}", i + 1, files.Length, files[i] );				
				ExcelAssist.Read( _application, files[i] );
			}
		}
	}
}
