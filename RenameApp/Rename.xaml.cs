using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace RenameApp
{
    /// <summary>
    /// Interaction logic for Rename.xaml
    /// </summary>
    public partial class Rename : Window
    {
        private List<string> filenames;
        private string outputDir;
        private string prefix;
        private char[] separator;
        private int startValue;
        private List<string> finalNames;

        public Rename(List<string> filenames, string prefix, string outputDir, int startValue, char[] separator)
        {
            InitializeComponent();
            this.filenames = new List<string>(filenames);
            this.outputDir = outputDir;
            this.prefix = prefix;
            this.startValue = startValue;
            this.separator = separator;

            finalNames = new List<string>();
            
            string concatenated = "";
            foreach(string file in this.filenames)
            {
                concatenated += file + "\n";
            }

            FileListTextBlock.Text = concatenated;
            
        }

        private void StartRenamingFiles()
        {
            //var i = startValue;

            Parallel.ForEach(filenames, (file) =>
            {
                var i = filenames.IndexOf(file) + startValue;
                var number = "";

                if( i < 10)
                {
                    number = "0" + i;
                } else
                {
                    number += i;
                }

                string extension = System.IO.Path.GetExtension(file);
                string newFilename = number + new string(separator) + prefix + extension;

                string newPath = System.IO.Path.Combine(outputDir, newFilename);

                try
                {
                    Debug.WriteLine($"Moving {newFilename} on {Thread.CurrentThread.ManagedThreadId}");
                    File.Move(file, newPath);
                }
                catch (Exception e)
                {
                    MessageBox.Show("There was an error moving files, some files may not have been copied!");
                    return;

                }

                finalNames.Add(newFilename);

               
            });
            
            var concatenated = "";

            foreach(string file in finalNames)
            {
                concatenated += file + "\n";
            }

            outputNames.Text = concatenated;

            MessageBox.Show("Finished Moving Files");


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //StartRenamingFiles();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            StartRenamingFiles();
        }
    }
}
