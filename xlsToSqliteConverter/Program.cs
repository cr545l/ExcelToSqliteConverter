using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lofle.XlsToSqliteConverter
{
	class Program
	{
		public enum eOption
		{
			none,
			files,
			directorys,
		}

		static void Main( string[] args )
		{	
			if( args.Length < 2 )
			{
				Debug.LogError( "인수 갯수가 부족함" );
				return;
			}

			string[] targets = new string[args.Length - 1];
			Array.Copy( args, 1, targets, 0, targets.Length );

			switch ( ParseOption( args[0] ) )
			{
				case eOption.files:
					Converter.Files( targets );
					break;

				case eOption.directorys:
					Converter.Directorys( targets );
					break;

				default:
					Debug.LogError( "인수가 옳바르지 않음" );
					break;
			}
		}

		static private eOption ParseOption(string arg)
		{
			try
			{
				return (eOption)Enum.Parse( typeof( eOption ), arg.ToLower() );
			}
			catch( Exception e )
			{
				Debug.LogError( e.ToString() );
				return eOption.none;
			}
		}
	}
}
