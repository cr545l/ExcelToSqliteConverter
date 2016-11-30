using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace xlsToSqliteConverter
{
	public class ExcelAssist
	{
		static public void Read( Excel.Application application, string filePath )
		{
			SQLiteAssist.Info info = new SQLiteAssist.Info( filePath );
			Excel.Workbook workBook = application.Workbooks.Open( info._filePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0 );

			for( int i = 1; i <= workBook.Worksheets.Count; i++ )
			{
				SQLiteAssist.DataSet dataSet = SheetToDataSet( info, workBook.Worksheets.get_Item( i ) );
				SQLiteAssist.CreateTable( info, dataSet );
			}
		}

		static SQLiteAssist.DataSet SheetToDataSet( SQLiteAssist.Info info, Excel.Worksheet workSheet )
		{
			Excel.Range range = workSheet.UsedRange;

			SQLiteAssist.DataSet dataSet = new SQLiteAssist.DataSet();
			dataSet._name = workSheet.Name;
			dataSet._columns = new String[range.Columns.Count];
			dataSet._types = new Type[range.Columns.Count];
			dataSet._datas = new dynamic[range.Rows.Count - 1, range.Columns.Count];

			for( int j = 1; j <= range.Rows.Count; j++ )
			{
				for( int i = 1; i <= range.Columns.Count; i++ )
				{
					Excel.Range value = range.Cells[j, i];

					if( null != value )
					{
						dynamic cellData = value.Value2;
						dataSet.Set( cellData, j, i );
					}
				}
			}
			return dataSet;
		}
	}
}
