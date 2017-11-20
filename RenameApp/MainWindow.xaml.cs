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
//using Microsoft.Win32;
using System.Windows.Forms;

namespace RenameApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> filenames;
        private int startValue;
        private string outputPath;

        public MainWindow()
        {
            InitializeComponent();
            filenames = new List<string>();
            startValue = 0;
            outputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "",
                Title = "Select Files to Rename..",
                Filter = " All Files... (*.*)|*.*",
                Multiselect = true
            };

            string concatenatedFilenames = "";
            if (dialog.ShowDialog() == true)
            {
                filenames.Clear();
                foreach (string filename in dialog.FileNames)
                {
                    concatenatedFilenames += filename + "\n";
                    filenames.Add(filename);
                }

                FileListTextBlock.Text = concatenatedFilenames;
                dialog.Reset();
            }
        }

        private void PrefixTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (RenameButton.IsEnabled && PrefixTextBox.Text == "")
            {
                RenameButton.IsEnabled = false;
            }
            else if (!RenameButton.IsEnabled)
            {
                RenameButton.IsEnabled = true;
            }
        }

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            var transparancyConverter = new TransparancyConverter(this);
            transparancyConverter.MakeTransparent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MainWindow1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void RenameButton_Click(object sender, RoutedEventArgs e)
        {
            string prefix = PrefixTextBox.Text;
            Rename win = new Rename(filenames, prefix, outputPath, startValue);
            win.ShowDialog();
            //this.Close();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
            if (e.NewValue != e.OldValue)
            {
                SliderDisplay.Text = "Start Value: " + e.NewValue;
                startValue = (int) e.NewValue;
                
            }
            
        }

        private void OutputDirButton_Click(object sender, RoutedEventArgs e)
        {
            var diag = new FolderBrowserDialog();

            diag.ShowDialog();

            outputPath = diag.SelectedPath;



            

        }
    }
}
