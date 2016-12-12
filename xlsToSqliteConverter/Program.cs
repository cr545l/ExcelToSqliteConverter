using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lofle.XlsToSqliteConverter
{
	class Program
	{
		static void Main( string[] args )
		{
			Command.Invoke( args );
		}

		public static void Release( object obj )
		{
			try
			{
				if( obj != null )
				{
					Marshal.ReleaseComObject( obj );
					obj = null;
				}
			}
			catch( Exception e )
			{
				obj = null;
				throw e;
			}
			finally
			{
				GC.Collect();
			}
		}
	}
}
