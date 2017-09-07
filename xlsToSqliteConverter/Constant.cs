using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lofle.XlsToSqliteConverter
{
	class Constant
	{
		/// <summary>
		/// 컬럼
		/// </summary>
		public const int _COLUMNS_INDEX = 1;

		/// <summary>
		/// 자료형
		/// </summary>
		public const int _TYPES_INDEX = 2;

		/// <summary>
		/// 주석
		/// </summary>
		public const int _COMMENT_INDEX = 3;

		/// <summary>
		/// 데이터 시작부분
		/// </summary>
		public const int _ROW_START_INDEX = _COMMENT_INDEX+1;

		public const string _COMMAND_CREATE_TABLE = "create table {0} ({1})";
		public const string _COMMAND_INSERT_INTO = "insert into {0} ({1}) values (";
		public const string _BEGIN = "Begin;";
		public const string _COMMIT = "Commit;";

		public const string _INTEGER_PRIMARY_KEY = "integer primary key";

		static public readonly Dictionary<string, string> _PARSE_DATA = new Dictionary<string, string>
		{
			{ _INTEGER_PRIMARY_KEY,		typeof(Int32).ToString()        },
			{ "int",					typeof(Int32).ToString()		},
			{ "integer",				typeof(Int32).ToString()		},

			{ "bigint",					typeof(Int64).ToString()		},

			{ "real",					typeof(Double).ToString()		},
			{ "float",					typeof(Double).ToString()		},

			{ "text",					typeof(String).ToString()		},
			{ "string",					typeof(String).ToString()		},
			{ "varchar",				typeof(String).ToString()		},

			{ "datetime",				typeof(DateTime).ToString()		},
			{ "blob",					typeof(byte[]).ToString()		},
			{ "varchar(36)",			typeof(Guid).ToString()			},
		};
	}
}
