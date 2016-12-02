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
	public class Converter : IDisposable
	{
		public Excel.Application _application = new Excel.Application();
		private readonly string[] _targetExtras = new[] { ".xls", ".xlsx" };

		public void Directorys( string[] directoryPaths )
		{
			for( int i = 0; i < directoryPaths.Length; i++ )
			{
				Debug.Log( "D {0}/{1} {2}", i + 1, directoryPaths.Length, directoryPaths[i] );
				string[] files = Directory.GetFiles( directoryPaths[i], "*.*", SearchOption.TopDirectoryOnly ).Where( s => _targetExtras.Contains( Path.GetExtension( s ), StringComparer.OrdinalIgnoreCase ) ).ToArray();
				
				Files( files );
			}
		}

		public void Files( string[] filePaths )
		{
			for( int i = 0; i < filePaths.Length; i++ )
			{
				Debug.Log( "F {0}/{1} {2}", i + 1, filePaths.Length, filePaths[i] );				
				ExcelAssist.Read( _application, filePaths[i] );
			}
		}

		public void Dispose()
		{
			_application.Quit();
			Program.Release( _application );
		}
	}
}
