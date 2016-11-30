using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace xlsToSqliteConverter
{
	class Program
	{
		static void Main( string[] args )
		{
			Excel.Application application = new Excel.Application();

			for( int i = 0; i < args.Length; i++ )
			{
				ExcelAssist.Read( application, args[i] );
			}
		}
	}
}
