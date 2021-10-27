using Microsoft.Win32;
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
//using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
//using System.Drawing;
using System.Windows.Media;
using Ookii.Dialogs.Wpf;
//using System.Windows.Media;

namespace _14_File_Extractor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var f = Path.GetFileName(@"D:\0_DW\result_20211016.pdf");
        }

        private void Window_DragEnter(object sender, DragEventArgs e)
        {
            label.FontSize *= 3;
            label.Content = "Drop To One Of Text Fields";
        }

        private void Object_Drop(object sender, DragEventArgs e)
        {
            var sen = sender as TextBox;

            if (e.Data is DataObject && ((DataObject)e.Data).ContainsFileDropList())
            {
                if(((DataObject)e.Data).GetFileDropList().Count < 1)
                {
                    label.Content = "Not A Valid Input";
                }
                else if (((DataObject)e.Data).GetFileDropList().Count > 1)
                    foreach (string filePath in ((DataObject)e.Data).GetFileDropList())
                    {
                        label.Content = "Not Implemented Fot Multiple Paths";

                    }
                else {
                    sen.Text = ((DataObject)e.Data).GetFileDropList()[0];
                    label.Content = "One Path Loaded";

                }
            }
        }

        private void TextBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TextBox sen = sender as TextBox;
            while (sen.LineCount * sen.FontSize > 110) sen.FontSize *= .5;
            while (sen.LineCount * sen.FontSize < 10) sen.FontSize = 35;

        }


        private void Grid_PreviewDragEnter(object sender, DragEventArgs e)
        {
            label.FontSize *= 3;
            label.Content = "Drop To One Of Open Buttons";
            (sender as Window).PreviewDragEnter -= Grid_PreviewDragEnter;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ookiiDialog = new VistaFolderBrowserDialog();
            if (ookiiDialog.ShowDialog() == true)
            {
                // do something with the folder path
                input.Text=ookiiDialog.SelectedPath;
            }
            // Create OpenFileDialog 
            //Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Multiselect = false;

            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    System.IO.FileInfo fInfo = new System.IO.FileInfo(openFileDialog.FileName);
            //    input.Text =  fInfo.DirectoryName;
            //}

            //Gat.Controls.OpenDialogView openDialog = new Gat.Controls.OpenDialogView();
            //Gat.Controls.OpenDialogViewModel vm = (Gat.Controls.OpenDialogViewModel)openDialog.DataContext;
            //vm.IsDirectoryChooser = true;
            //vm.Show();

            //label.Content=vm.SelectedFilePath.ToString();

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var ookiiDialog = new VistaFolderBrowserDialog();
            if (ookiiDialog.ShowDialog() == true)
            {
                // do something with the folder path
                output.Text = ookiiDialog.SelectedPath;
            }

        }

        private void Extract(string input,string output)
        {
           var list= GetAllFiles(input);
            int n = 0;
            foreach (string file in list)
            {
                string dest = Path.Join(output, Path.GetFileName(file));
                if (!File.Exists(dest)) File.Copy(file, dest);
                else n++;
            }
            MessageBox.Show($"{n} files were duplicit");
            Button_Bun.Background = Brushes.Green;
        }
        private List<string> GetAllFiles(string path)
        {
            List<string> list = new();

                list.AddRange(Directory.GetFiles(path));

            foreach (var dir in Directory.GetDirectories(path))
            {
                list.AddRange(GetAllFiles(dir));
            }

            return list;
        }


        private void Button_Bun_Click(object sender, RoutedEventArgs e)
        {
            string inp = input.Text;
            string ou = output.Text;
            if (!Directory.Exists(inp)|| !Directory.Exists(ou)) { MessageBox.Show("Not valid folders");return; }


            Extract(inp,ou);
        }

        private void Button_Drop(object sender, DragEventArgs e)
        {
            if (e.Data is DataObject && ((DataObject)e.Data).ContainsFileDropList())
            {
                if (((DataObject)e.Data).GetFileDropList().Count < 1)
                {
                    label.Content = "Not A Valid Input";
                }
                else if (((DataObject)e.Data).GetFileDropList().Count > 1)
                    foreach (string filePath in ((DataObject)e.Data).GetFileDropList())
                    {
                        label.Content = "Not Implemented Fot Multiple Paths";

                    }
                else
                {
                    input.Text = ((DataObject)e.Data).GetFileDropList()[0];
                    label.Content = "One Path Loaded";

                }
            }
        }

        private void Button_Drop_1(object sender, DragEventArgs e)
        {
            if (e.Data is DataObject && ((DataObject)e.Data).ContainsFileDropList())
            {
                if (((DataObject)e.Data).GetFileDropList().Count < 1)
                {
                    label.Content = "Not A Valid Input";
                }
                else if (((DataObject)e.Data).GetFileDropList().Count > 1)
                    foreach (string filePath in ((DataObject)e.Data).GetFileDropList())
                    {
                        label.Content = "Not Implemented Fot Multiple Paths";

                    }
                else
                {
                    output.Text = ((DataObject)e.Data).GetFileDropList()[0];
                    label.Content = "One Path Loaded";

                }
            }

        }

        private void input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Directory.Exists(input.Text)) buttIn.Background = Brushes.LightGreen;
            else buttIn.Background = Brushes.LightPink;
        }

        private void output_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Directory.Exists(output.Text)) buttou.Background = Brushes.LightGreen;
            else buttou.Background = Brushes.LightPink;
        }
    }
}
