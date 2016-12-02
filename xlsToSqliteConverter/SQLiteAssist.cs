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
		private const string _COMMAND_CREATE_TABLE = "create table {0} ({1})";
		private const string _COMMAND_INSERT_INTO = "insert into {0} ({1}) values (";

		static public void CreateTable( SQLPathInfo path, SQLDataSet dataSet )
		{
			CreateTable( path.SqlConnection, dataSet );
			Insert( path.SqlConnection, dataSet );
		}

		static private bool CreateTable( SQLiteConnection connection, SQLDataSet dataSet )
		{
			string command = String.Format( _COMMAND_CREATE_TABLE, dataSet.SheetName, dataSet.GetColumnAndTypes() );
			return Command( connection, command );
		}

		static private void Insert( SQLiteConnection connection, SQLDataSet dataSet )
		{
			string defaultCommand = String.Format( _COMMAND_INSERT_INTO, dataSet.SheetName, dataSet.GetColumns() );
			StringBuilder command = null;

			for( int j = 3; j <= dataSet.Datas.GetLength( 0 ); j++ )
			{
				command = new StringBuilder( defaultCommand );
				for( int i = 1; i <= dataSet.Datas.GetLength( 1 ); i++ )
				{
					dynamic data = dataSet.Datas[j, i];
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
