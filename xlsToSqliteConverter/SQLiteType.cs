using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lofle.XlsToSqliteConverter
{
	class SQLiteType
	{
		static private readonly Dictionary<string, string> _parseDic = new Dictionary<string, string> {
			{ "integer primary key", typeof(Int32).ToString() },
			{ "int", typeof(Int32).ToString() },
			{ "integer", typeof(Int32).ToString() },
			{ "bigint", typeof(Int64).ToString() },
			{ "real", typeof(Double).ToString() },
			{ "float", typeof(Double).ToString() },
			{ "text", typeof(String).ToString() },
			{ "string", typeof(String).ToString() },
			{ "varchar", typeof(String).ToString() },
			{ "datetime", typeof(DateTime).ToString() },
			{ "blob", typeof(byte[]).ToString() },
			{ "varchar(36)", typeof(Guid).ToString() },
		};

		static public string ConvertCShapeType( string sqliteType )
		{
			if( null == sqliteType )
			{
				return _parseDic["string"];
			}
			else
			{
				return _parseDic.ContainsKey( sqliteType.ToLower() ) ? _parseDic[sqliteType.ToLower()] : _parseDic["string"];
			}
		}
	}
}
