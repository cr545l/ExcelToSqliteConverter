using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lofle.XlsToSqliteConverter
{
	public class SQLitePathInfo : IDisposable
	{
		private string _excelFileName;
		private string _excelFilePath;
		private string[] _excelFilePathTrees;

		private string _fileName;
		private string _sqliteFileName;

		private string _directoryPath;
		private string _sqliteFilePath;

		private SQLiteConnection _sqliteConnection;

		public string FilePath
		{
			get { return _excelFilePath; }
		}

		public SQLiteConnection SqlConnection
		{
			get { return _sqliteConnection; }
		}

		public string FileName
		{
			get { return _fileName; }
		}

		public string DirectoryPath
		{
			get { return _directoryPath; }
		}

		public SQLitePathInfo( string filePath )
		{
			Set( filePath );
		}

		public void Dispose()
		{
			_sqliteConnection.Close();
		}

		private void Set( string filePath )
		{
			_excelFilePath = filePath;
			_excelFilePathTrees = _excelFilePath.Split( '\\' );
			_excelFileName = _excelFilePathTrees[_excelFilePathTrees.Length - 1];

			_fileName = _excelFileName.Split( '.' )[0];
			_sqliteFileName = FileName + ".sqlite";

			_directoryPath = _excelFilePath.Substring( 0, _excelFilePath.Length - _excelFileName.Length );
			_sqliteFilePath = DirectoryPath + _sqliteFileName;

			if( System.IO.File.Exists( _sqliteFilePath ) )
			{
				System.IO.File.Delete( _sqliteFilePath );
			}

			SQLiteConnection.CreateFile( _sqliteFilePath );
			_sqliteConnection = new SQLiteConnection( String.Format( "Data Source={0};Version=3;", _sqliteFilePath ) );
			_sqliteConnection.Open();
		}
	}
}
