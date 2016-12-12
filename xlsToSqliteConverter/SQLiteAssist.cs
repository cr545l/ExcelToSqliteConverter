﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Lofle.XlsToSqliteConverter
{
	public class SQLiteAssist
	{
		static private readonly string _COMMAND_CREATE_TABLE = "create table {0} ({1})";
		static private readonly string _COMMAND_INSERT_INTO = "insert into {0} ({1}) values (";
		static private readonly string _BEGIN = "Begin;";
		static private readonly string _COMMIT = "Commit;";

		static public void CreateTable( SQLiteConnection connection, SQLiteData dataSet )
		{
			string command = String.Format( _COMMAND_CREATE_TABLE, dataSet.SheetName, dataSet.GetColumnAndTypes() );
			Command( connection, command );
		}

		static public void Insert( SQLiteConnection connection, SQLiteData dataSet, Action<float> percent = null )
		{
			string defaultCommand = String.Format( _COMMAND_INSERT_INTO, dataSet.SheetName, dataSet.GetColumns() );
			StringBuilder command = null;

			int rowLength = dataSet.Datas.GetLength( 0 );
			int columnLength = dataSet.Datas.GetLength( 1 );

			Command( connection, _BEGIN );
			for( int j = SQLiteData._ROW_START_INDEX; j <= rowLength; j++ )
			{
				command = new StringBuilder( defaultCommand );
				for( int i = 1; i <= columnLength; i++ )
				{
					if( 1 != i )
					{
						command.Append( "\', " );
					}
					command.Append( "\'" );

					dynamic data = dataSet.Datas[j, i];
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

				float lowLength = 0.0f != (float)( rowLength - SQLiteData._ROW_START_INDEX) ? (float)( rowLength - SQLiteData._ROW_START_INDEX ) : 1;
				percent?.Invoke( (j - SQLiteData._ROW_START_INDEX) / lowLength );
			}
			Command( connection, _COMMIT );
		}

		static private void Command( SQLiteConnection connection, string command )
		{
			try
			{
				SQLiteCommand sql = new SQLiteCommand( command, connection );
				sql.ExecuteNonQuery();
			}
			catch( Exception e )
			{
				Debug.Log( "{0} {1}", command, e.ToString() );
				throw e;
			}
		}
	}
}
