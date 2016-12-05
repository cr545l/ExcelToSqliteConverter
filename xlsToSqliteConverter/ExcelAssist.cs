using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace Lofle.XlsToSqliteConverter
{
	public class ExcelAssist : IDisposable
	{
		public Excel.Application _application = new Excel.Application();

		public SQLiteData[] Read( string filePath )
		{
			List<SQLiteData> result = new List<SQLiteData>();

			if( !System.IO.File.Exists( filePath ) )
			{
				Debug.LogError( "{0} 파일 찾기 실패", filePath );
				return null;
			}

			Excel.Workbook workBook = null;
			try
			{
				workBook = _application.Workbooks.Open( filePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0 );
				
				if( null != workBook )
				{
					// UsedRange.Value2로 가져온 배열의 인덱스가 1부터 시작
					for( int i = 1; i <= workBook.Worksheets.Count; i++ )
					{
						result.Add( Convert( workBook.Worksheets.get_Item( i ) ) );
					}
				}
			}
			catch( Exception e )
			{
				Debug.LogError( e.ToString() );
			}
			finally
			{
				Program.Release( workBook );
			}

			return result.ToArray();
		}

		static private SQLiteData Convert( Excel.Worksheet workSheet )
		{
			Excel.Range range = workSheet.UsedRange;
			Excel.Range rows = range.Rows;
			Excel.Range columns = range.Columns;

			SQLiteData dataSet = new SQLiteData();
			dataSet.SheetName = workSheet.Name;
			dataSet.Datas = workSheet.UsedRange.Value2;

			Program.Release( workSheet );

			return dataSet;
		}

		public void Dispose()
		{
			_application.Quit();
			Program.Release( _application );
		}
	}
}
