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
				string[] targetPaths = new string[args.Length - 1];
				Array.Copy( args, 1, targetPaths, 0, targetPaths.Length );
				Converter.GenerateCodeInfo info = null;
				switch( option )
				{
					case eOption.files:
						info = Converter.Files( targetPaths, ( percent, file ) => { Debug.Log( "{0}% ({1}%) {2}", percent._convert * 100.0f, percent._insert * 100.0f, file ); } );
						break;

					case eOption.directorys:
						info = Converter.Directorys( targetPaths, ( percent, file ) => { Debug.Log( "{0}% ({1}%) {2}", percent._convert * 100.0f, percent._insert * 100.0f, file ); } );
						break;

					default:
						break;
				}
				IOAssist.CreateFile( info.Path + Converter.GenerateCodeInfo._DEFAULT_CODE_FILE_FULLNAME, info.Code );
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
