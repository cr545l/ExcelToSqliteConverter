using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace xlsToSqliteConverter
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

		public class DataSet
		{
			public string _name;
			public string[] _columns;
			public Type[] _types;
			public dynamic[,] _datas;

			public void Set( dynamic data, int row, int range )
			{
				if( 1 == row )
				{
					_columns[range - 1] = data as String;
				}
				else
				{
					if( 2 == row )
					{
						_types[range - 1] = data.GetType();
					}
					_datas[row - 2, range - 1] = data;
				}
			}
		}

		static public void CreateTable( Info path, DataSet dataSet )
		{
			try
			{
				CreateTable( path._sqlConnection, dataSet );
				Insert( path._sqlConnection, dataSet );
			}
			catch( Exception e )
			{
				System.Console.WriteLine( e );
			}
		}

		static private void Insert( SQLiteConnection connection, DataSet dataSet )
		{
			string insert = "insert into "+dataSet._name+" (";
			for( int i = 0; i < dataSet._columns.Length; i++ )
			{
				if( 0 != i )
					insert += ", ";
				insert += dataSet._columns[i];
			}
			insert += ") values (";

			for( int j = 0; j < dataSet._datas.GetLength( 0 ); j++ )
			{
				string command = insert;
				for( int i = 0; i < dataSet._datas.GetLength( 1 ); i++ )
				{
					if( 0 != i )
						command += ", ";
					command += dataSet._datas[j, i];
				}
				command += ")";
				Command( connection, command );
			}
		}

		static private void CreateTable( SQLiteConnection connection, DataSet dataSet )
		{
			string command = "create table " + dataSet._name + " (";
			for( int i = 0; i < dataSet._columns.Length; i++ )
			{
				if( 0 != i )
					command += ", ";
				command += String.Format( "{0} {1}", dataSet._columns[i], GetType( dataSet._types[i] ) );
			}
			command += ")";

			Command( connection, command );
		}

		static private string GetType( Type type )
		{
			string result = type.ToString();

			if( typeof( string ) == type )
			{
				result = "varchar(255)";
			}
			else if( typeof( float ) == type || typeof( double ) == type )
			{
				result = "float";
			}
			else if( typeof( int ) == type )
			{
				result = "int";
			}
			else if( typeof( bool ) == type )
			{
				result = "bool";
			}

			return result;
		}

		static private void Command( SQLiteConnection connection, string command )
		{
			SQLiteCommand sql = new SQLiteCommand( command, connection );
			sql.ExecuteNonQuery();
		}
	}
}
