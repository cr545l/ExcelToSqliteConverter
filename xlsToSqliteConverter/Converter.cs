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
	public class Converter
	{
		public class Percents
		{
			public float _insert = 0.0f;
			public float _convert = 0.0f;

			public Percents( float fileProgress, float sqliteInsert )
			{
				_convert = fileProgress;
				_insert = sqliteInsert;
			}
		}

		static private readonly string[] _targetExtras = new[] { ".xls", ".xlsx" };

		static public void Directorys( string[] directoryPaths, Action<Percents, string> callback = null )
		{
			List<string> fileList = new List<string>();
			for( int i = 0; i < directoryPaths.Length; i++ )
			{
				fileList.AddRange( Directory.GetFiles( directoryPaths[i], "*.*", SearchOption.TopDirectoryOnly ).Where( s => _targetExtras.Contains( Path.GetExtension( s ), StringComparer.OrdinalIgnoreCase ) ) );
			}
			Files( fileList.ToArray(), callback );
		}

		static public void Files( string[] filePaths, Action<Percents, string> callback = null )
		{
			using( ExcelAssist excelAssist = new ExcelAssist() )
			{
				for( int i = 0; i < filePaths.Length; i++ )
				{
					using( SQLitePathInfo info = new SQLitePathInfo( filePaths[i] ) )
					{
						SQLiteData[] dataSets = excelAssist.Read( filePaths[i] );
						if( null != dataSets )
						{
							for( int j = 0; j < dataSets.Length; j++ )
							{
								SQLiteAssist.CreateTable( info.SqlConnection, dataSets[j] );
								SQLiteAssist.Insert( info.SqlConnection, dataSets[j], ( insertPercent ) =>
								{
									callback( new Percents( i / (float)filePaths.Length, insertPercent ),
									String.Format( "{0} ({1}/{2})", info.FileName, j + 1, dataSets.Length ) );
								} );
							}
						}

						callback?.Invoke( new Percents( ( i + 1 ) / (float)filePaths.Length, 1.0f ),
						String.Format( "{0} ({1}/{1})", info.FileName, dataSets.Length ) );
					}
				}
			}
		}
	}
}
