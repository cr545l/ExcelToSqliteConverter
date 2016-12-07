using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lofle.XlsToSqliteConverter
{
	public class IOAssist
	{
		static public void CreateFile( string path, string value )
		{
			if( System.IO.File.Exists( path ) )
			{
				System.IO.File.Delete( path );
			}

			StreamWriter streamWriter = null;
			try
			{
				streamWriter = System.IO.File.CreateText( path );
				streamWriter.Write( value );
			}
			catch(Exception e)
			{
				throw e;
			}
			finally
			{
				if( null != streamWriter )
				{
					streamWriter.Close();
				}
			}
		}
	}
}
