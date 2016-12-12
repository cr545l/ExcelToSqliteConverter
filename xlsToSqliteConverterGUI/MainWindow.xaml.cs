using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lofle.XlsToSqliteConverter;

namespace Lofle.XlsToSqliteConverterGUI
{
	/// <summary>
	/// MainWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow : Window
	{
		private bool _bStart = false;
		private string _titleDefault = "";

		public MainWindow()
		{
			InitializeComponent();
			_titleDefault = Title;
			convertProgress.Minimum = 0;
			convertProgress.Maximum = 1;
			insertProgress.Minimum = 0;
			insertProgress.Maximum = 1;

			checkBox.IsChecked = true;
			textBox.Text = Converter.GenerateCodeInfo._DEFAULT_CODE_FILE_FULLNAME;
		}

		private void DropFile( object sender, DragEventArgs e )
		{
			if( e.Data.GetDataPresent( DataFormats.FileDrop ) )
			{
				Convert( (string[])e.Data.GetData( DataFormats.FileDrop ) );
			}
		}

		private void Convert( string[] files )
		{
			if( !_bStart )
			{
#if true//!DEBUG
				try
				{
#endif
					Converter.GenerateCodeInfo info = Converter.Files( files, CallbackPercent );

					if( true == checkBox.IsChecked )
					{
						IOAssist.CreateFile( info.Path + textBox.Text, info.Code );
					}
#if true//!DEBUG
				}
				catch( Exception e )
				{
					MessageBox.Show( e.Message + "\n\n" + e.ToString(), "확인", MessageBoxButton.OK );
				}
#endif
			}
		}

		private void CallbackPercent( Converter.ProgressInfo percent, string fileName )
		{
			RefreshUI( percent, fileName );
		}

		private void RefreshUI( Converter.ProgressInfo value, string fileName )
		{
			convertProgress.Value = value._convert;
			insertProgress.Value = value._insert;

			this.Dispatcher.Invoke( (System.Threading.ThreadStart)(() => { }), System.Windows.Threading.DispatcherPriority.ApplicationIdle );

			if( 1.0f <= value._convert )
			{
				Title = _titleDefault;
				_bStart = false;

				convertProgress.Value = 0;
				insertProgress.Value = 0;
			}
			else
			{
				Title = fileName;
				_bStart = true;
			}

		}

		private void checkBox_Checked( object sender, RoutedEventArgs e )
		{
			if( false == checkBox.IsChecked )
			{
				textBox.IsEnabled = false;
			}
			else
			{
				textBox.IsEnabled = true;
			}
		}
	}
}
