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
		static public void CreateTable( SQLiteConnection connection, SQLiteData dataSet )
		{
			string command = String.Format( Constant._COMMAND_CREATE_TABLE, dataSet.SheetName, dataSet.GetColumnAndTypes() );
			Command( connection, command );
		}

		static public void Insert( SQLiteConnection connection, SQLiteData dataSet, Action<float> percent = null )
		{
			string defaultCommand = String.Format( Constant._COMMAND_INSERT_INTO, dataSet.SheetName, dataSet.GetColumns() );
			StringBuilder command = null;

			int rowLength = dataSet.Datas.GetLength( 0 );
			int columnLength = dataSet.Datas.GetLength( 1 );

			Command( connection, Constant._BEGIN );
			for( int j = 3; j <= rowLength; j++ )
			{
				command = new StringBuilder( defaultCommand );
				for( int i = 1; i <= columnLength; i++ )
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
							string stringData = data as string; ;
							stringData = stringData.Replace( "\'", "\'\'" );
							data = stringData;
						}
						command.Append( data );
					}
				}
				
				command.Append( "\')" );
				Command( connection, command.ToString() );

				float lowLength = 0.0f != (float)( rowLength - 3 ) ? (float)( rowLength - 3 ) : 1;
				percent?.Invoke( (j - 3) / lowLength );
			}
			Command( connection, Constant._COMMIT );
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
				// 시트명이 숫자로 되어 있으면 예외발생
				Debug.LogError( "{0} {1}", command, e.ToString() );
			}
		}
	}
}
