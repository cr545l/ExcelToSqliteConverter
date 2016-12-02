
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lofle.XlsToSqliteConverter
{
	public class SQLDataSet
	{
		private const int _COLUMNS_INDEX = 1;
		private const int _TYPES_INDEX = 2;

		private string _sheetName;
		private string[] _columns;
		private string[] _types;
		private object[,] _datas;

		public string SheetName
		{
			get { return _sheetName; }
			set { _sheetName = value; }
		}

		public object[,] Datas
		{
			get { return _datas; }
			set
			{
				_datas = value;
				_columns = GetArray<string>( _datas, _COLUMNS_INDEX );
				_types = GetArray<string>( _datas, _TYPES_INDEX );
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
	}
}
