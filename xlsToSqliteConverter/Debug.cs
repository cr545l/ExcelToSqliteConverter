using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lofle.XlsToSqliteConverter
{
	public class Debug
	{
		static public void Log( string format, params object[] arg )
		{
			System.Console.WriteLine( format, arg );
		}

		static public void Log( string arg )
		{
			System.Console.WriteLine( arg );
		}

		static public void LogError( string arg )
		{
			Console.ForegroundColor = ConsoleColor.Red;
			System.Console.WriteLine( arg );
			Console.ReadKey();
			Console.ForegroundColor = ConsoleColor.White;
		}

		static public void LogError( string format, params object[] arg )
		{
			Console.ForegroundColor = ConsoleColor.Red;
			System.Console.WriteLine( format, arg );
			Console.ReadKey();
			Console.ForegroundColor = ConsoleColor.White;
		}
	}
}
