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

			SQLPathInfo info = null;
			Excel.Workbook workBook = null;

			try
			{
				info = new SQLPathInfo( filePath );
				workBook = application.Workbooks.Open( info.FilePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0 );
				
				if( null != workBook )
				{
					// UsedRange.Value2로 가져온 배열의 인덱스가 1부터 시작
					for( int i = 1; i <= workBook.Worksheets.Count; i++ )
					{
						SQLDataSet dataSet = SheetToDataSet( info, workBook.Worksheets.get_Item( i ) );
						SQLiteAssist.CreateTable( info, dataSet );
					}
				}
			}
			catch(Exception e)
			{
				Debug.LogError( e.ToString() );
			}
			finally
			{
				Program.Release( workBook );
			}
		}

		static SQLDataSet SheetToDataSet( SQLPathInfo info, Excel.Worksheet workSheet )
		{
			Excel.Range range = workSheet.UsedRange;
			Excel.Range rows = range.Rows;
			Excel.Range columns = range.Columns;

			SQLDataSet dataSet = new SQLDataSet();
			dataSet.SheetName = workSheet.Name;
			dataSet.Datas = workSheet.UsedRange.Value2;

			Program.Release( workSheet );

			return dataSet;
		}
	}
}
