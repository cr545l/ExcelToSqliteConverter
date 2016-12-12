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
		public class GenerateCodeInfo
		{
			static public readonly string _DEFAULT_CODE_FILE_FULLNAME = "SQLiteTable.cs";

			private string _path = null;
			private string _codeFileFullName = null;
			private string _code = null;

			public string Path
			{
				get { return _path; }

				set
				{
					if( null == _path )
					{
						_path = value;
					}
				}
			}

			public string Code
			{
				get { return _code; }
				set { _code = value; }
			}

			public string FileFullName
			{
				get { return _codeFileFullName; }
				set { _codeFileFullName = value; }
			}
		}

		public class ProgressInfo
		{
			public float _insert = 0.0f;
			public float _convert = 0.0f;

			public ProgressInfo( float fileProgress, float sqliteInsert )
			{
				_convert = fileProgress;
				_insert = sqliteInsert;
			}
		}
		static private readonly string[] _targetExtras = new[] { ".xls", ".xlsx" };

		static public GenerateCodeInfo Directorys( string[] directoryPaths, Action<ProgressInfo, string> callback = null )
		{
			List<string> fileList = new List<string>();
			for( int i = 0; i < directoryPaths.Length; i++ )
			{
				fileList.AddRange( Directory.GetFiles( directoryPaths[i], "*.*", SearchOption.TopDirectoryOnly ).Where( s => _targetExtras.Contains( Path.GetExtension( s ), StringComparer.OrdinalIgnoreCase ) ) );
			}
			return Files( fileList.ToArray(), callback );
		}

		static public GenerateCodeInfo Files( string[] filePaths, Action<ProgressInfo, string> callback = null )
		{
			GenerateCodeInfo result = new GenerateCodeInfo();
			StringBuilder code = new StringBuilder();
			
			using( ExcelAssist excelAssist = new ExcelAssist() )
			{
				for( int i = 0; i < filePaths.Length; i++ )
				{
					using( SQLitePathInfo info = new SQLitePathInfo( filePaths[i] ) )
					{
						result.Path = info.DirectoryPath;

						SQLiteData[] dataSets = excelAssist.Read( filePaths[i] );
						if( null != dataSets )
						{
							for( int j = 0; j < dataSets.Length; j++ )
							{
								SQLiteAssist.CreateTable( info.SqlConnection, dataSets[j] );
								SQLiteAssist.Insert( info.SqlConnection, dataSets[j], ( insertPercent ) =>
								{
									callback( new ProgressInfo( i / (float)filePaths.Length, insertPercent ),
									String.Format( "{0} ({1}/{2})", info.FileName, j + 1, dataSets.Length ) );
								} );

								code.Append( dataSets[j].GetCode( info.FileName ) );
								code.Append( "\n" );
							}
						}
						callback?.Invoke( new ProgressInfo( (i + 1) / (float)filePaths.Length, 1.0f ),
						String.Format( "{0} ({1}/{1})", info.FileName, dataSets.Length ) );
					}
				}
			}
			result.Code = code.ToString();
			return result;
		}
	}
}
