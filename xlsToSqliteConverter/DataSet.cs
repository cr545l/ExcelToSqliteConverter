
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lofle.XlsToSqliteConverter
{
	public class DataSet
	{
		public string _sheetName;
		public string[] _columns;
		public Type[] _types;
		public dynamic[,] _datas;

		public string GetColumns()
		{
			StringBuilder result = new StringBuilder();

			for( int i = 0; i < _columns.Length; i++ )
			{
				if( 0 != i )
				{
					result.Append( ", " );
				}
				result.Append( _columns[i] );
			}

			return result.ToString();
		}

		public string GetColumnAndTypes()
		{
			StringBuilder result = new StringBuilder();

			for( int i = 0; i < _columns.Length; i++ )
			{
				if( 0 != i )
				{
					result.Append( ", " );
				}
				result.Append( _columns[i] );
				result.Append( " " );
				result.Append( GetType( _types[i] ) );
			}

			return result.ToString();
		}

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

		static public string GetType( Type type )
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
	}
}
