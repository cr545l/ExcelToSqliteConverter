using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lofle.XlsToSqliteConverter
{
	public class SQLiteData
	{
		private string _sheetName;
		private string[] _columns;
		private string[] _types;
		private object[,] _datas;

		public string SheetName { get { return _sheetName; } set { _sheetName = value; } }

		public object[,] Datas
		{
			get { return _datas; }
			set
			{
				_datas = value;
				_columns = GetArray<string>( _datas, Constant._COLUMNS_INDEX );
				_types = GetArray<string>( _datas, Constant._TYPES_INDEX );
			}
		}

		static private T[] GetArray<T>( object[,] source, int index )
		{
			// UsedRange.Value2로 가져온 배열의 인덱스가 1부터 시작
			T[] result = new T[source.GetLength( 1 )];

			for( int i = 0; i < result.Length; i++ )
			{
				result[i] = (T)source[index, i + 1];
			}

			return result;
		}

		public string GetColumns()
		{
			return AppendToString( ( s, i ) =>
			{
				s.Append( _columns[i] );
			} );
		}

		public string GetColumnAndTypes()
		{
			return AppendToString( ( s, i ) =>
			{
				s.Append( _columns[i] );
				s.Append( " " );
				s.Append( _types[i] );
			} );
		}

		private string AppendToString( Action<StringBuilder, int> callback )
		{
			StringBuilder result = new StringBuilder();

			for( int i = 0; i < _columns.Length; i++ )
			{
				if( 0 != i )
				{
					result.Append( ", " );
				}

				callback( result, i );
			}

			return result.ToString();
		}

		public string GetCode(string fileName)
		{
			StringBuilder result = new StringBuilder();
			result.Append( "namespace " );
			result.Append( fileName );
			result.Append( "\n{\n" );

			result.Append( "\tpublic class " );
			result.Append( _sheetName );
			result.Append( "\n\t{\n" );

			for( int i = 0; i < _types.Length; i++ )
			{
				if( null != _types[i] && isPrimaryKey( _types[i] ) )
				{
					result.Append( "\t\t[SQLite4Unity3d.PrimaryKey, SQLite4Unity3d.AutoIncrement]\n" );
				}

				result.Append( "\t\tpublic " );
				result.Append( ConvertCShapeType( _types[i] ));
				result.Append( " " );
				result.Append( _columns[i] );
				result.Append( " { get; set; }\n" );
			}
			result.Append( "\t}\n" );
			result.Append( "}\n" );

			return result.ToString();
		}

		static public bool isPrimaryKey(string type)
		{
			return 0 == String.Compare( type.ToLower(), Constant._INTEGER_PRIMARY_KEY );
		}

		static public string ConvertCShapeType( string sqliteType )
		{
			if( null == sqliteType || !Constant._PARSE_DATA.ContainsKey( sqliteType.ToLower() ) )
			{
				return Constant._PARSE_DATA["string"];
			}
			else
			{
				return Constant._PARSE_DATA[sqliteType.ToLower()];
			}
		}
	}
}
