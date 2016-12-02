using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lofle.XlsToSqliteConverter
{
	public class Command
	{
		public enum eOption
		{
			none,
			files,
			directorys,
		}

		private const string _MESSAGE_LACK_ARGS = "인수 갯수가 부족함";
		private const string _MESSAGE_NOT_FOUND_OPTION = "{0} 잘못된 옵션";

		static public void Invoke( string[] args )
		{
			eOption option = GetOption( args );

			if( eOption.none != option )
			{
				using( Converter converter = new Converter() )
				{
					string[] targetPaths = new string[args.Length - 1];
					Array.Copy( args, 1, targetPaths, 0, targetPaths.Length );

					switch( option )
					{
						case eOption.files:
							converter.Files( targetPaths );
							break;

						case eOption.directorys:
							converter.Directorys( targetPaths );
							break;

						default:
							break;
					}
				}
			}
		}

		static private eOption GetOption( string[] args )
		{
			if( args.Length < 2 )
			{
				Debug.LogError( _MESSAGE_LACK_ARGS );
				return eOption.none;
			}

			return ParseOption( args[0] );
		}

		static private eOption ParseOption( string arg )
		{
			try
			{
				return (eOption)Enum.Parse( typeof( eOption ), arg.ToLower() );
			}
			catch 
			{
				Debug.LogError( _MESSAGE_NOT_FOUND_OPTION, arg );
				return eOption.none;
			}
		}
	}
}
