using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Lofle.XlsToSqliteConverter
{
	public class SQLiteAssist
	{
		public class Info : IDisposable
		{
			public string[] _paths;
			public string _fileName;
			public string _filePath;

			public string _name;
			public string _sqlName;

			public string _path;
			public string _sqlPath;

			public SQLiteConnection _sqlConnection;

			public Info( string filePath )
			{
				Set( filePath );
			}

			public void Dispose()
			{
				_sqlConnection.Close();
			}

			private void Set( string filePath )
			{
				_filePath = filePath;
				_paths = _filePath.Split( '\\' );
				_fileName = _paths[_paths.Length - 1];

				_name = _fileName.Split( '.' )[0];
				_sqlName = _name + ".sqlite";

				_path = _filePath.Substring( 0, _filePath.Length - _fileName.Length );
				_sqlPath = _path + _sqlName;

				if( System.IO.File.Exists( _sqlPath ) )
				{
					System.IO.File.Delete( _sqlPath );
				}

				SQLiteConnection.CreateFile( _sqlPath );
				_sqlConnection = new SQLiteConnection( String.Format( "Data Source={0};Version=3;", _sqlPath ) );
				_sqlConnection.Open();
			}
		}

		static public void CreateTable( Info path, DataSet dataSet )
		{
			CreateTable( path._sqlConnection, dataSet );
			Insert( path._sqlConnection, dataSet );
		}

		static private bool CreateTable( SQLiteConnection connection, DataSet dataSet )
		{
			string command = "create table " + dataSet._sheetName + " (" + dataSet.GetColumnAndTypes() + ")";
			return Command( connection, command );
		}

		static private void Insert( SQLiteConnection connection, DataSet dataSet )
		{
			string insert = "insert into " + dataSet._sheetName + " (" + dataSet.GetColumns() + ") values (";
			StringBuilder command = null;

			for( int j = 1; j <= dataSet._datas.GetLength( 0 ); j++ )
			{
				command = new StringBuilder( insert );
				for( int i = 1; i <= dataSet._datas.GetLength( 1 ); i++ )
				{
					dynamic data = dataSet._datas[j, i];
					if( 1 != i )
					{
						command.Append( "\', " );
					}
					command.Append( "\'" );

					if( null != data )
					{
						if( data.GetType() == typeof( string ) )
						{
							string stringData = data as string;
							stringData = stringData.Replace( "\'", "\'\'" );
							data = stringData;
						}
						command.Append( data );
					}
				}
				command.Append( "\')" );
				Command( connection, command.ToString() );
			}
		}

		static private bool Command( SQLiteConnection connection, string command )
		{
			try
			{
				SQLiteCommand sql = new SQLiteCommand( command, connection );
				sql.ExecuteNonQuery();
				return true;
			}
			catch( Exception e )
			{
				// 시트명이 숫자로 되어 있으면 예외발생
				Debug.LogError( "{0} {1}", command, e.ToString() );
				return false;
			}
		}
	}
}
