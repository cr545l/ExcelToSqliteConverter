using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace Lofle.XlsToSqliteConverter
{
	public class ExcelAssist
	{
		static public void Read( Excel.Application application, string filePath )
		{
			if( !System.IO.File.Exists( filePath ) )
			{
				Debug.LogError( "{0} 파일 찾기 실패", filePath );
				return;
			}

			SQLiteAssist.Info info = null;
			Excel.Workbook workBook = null;
			try
			{
				info = new SQLiteAssist.Info( filePath );
				workBook = application.Workbooks.Open( info._filePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0 );
			}
			catch(Exception e)
			{
				Debug.LogError( e.ToString() );
				return;
			}

			if( null != workBook )
			{
				for( int i = 1; i <= workBook.Worksheets.Count; i++ )
				{
					DataSet dataSet = SheetToDataSet( info, workBook.Worksheets.get_Item( i ) );
					SQLiteAssist.CreateTable( info, dataSet );
				}
			}
		}

		static DataSet SheetToDataSet( SQLiteAssist.Info info, Excel.Worksheet workSheet )
		{
			Excel.Range range = workSheet.UsedRange;
			Excel.Range rows = range.Rows;
			Excel.Range columns = range.Columns;

			DataSet dataSet = new DataSet();
			dataSet._sheetName = workSheet.Name;
			dataSet._columns = new String[columns.Count];
			dataSet._types = new Type[columns.Count];
			dataSet._datas = new dynamic[rows.Count - 1, columns.Count];
			
			for( int j = 1; j <= rows.Count; j++ )
			{
				for( int i = 1; i <= columns.Count; i++ )
				{
					Excel.Range value = range.Cells[j, i];

					if( null != value )
					{
						dynamic cellData = value.Value2;
						dataSet.Set( cellData, j, i );
					}
				}
				if( j == 2 )
					break;
			}

			dataSet._datas = workSheet.UsedRange.Value2;

			return dataSet;
		}
	}
}
