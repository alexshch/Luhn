using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace WpfApplicationLuhn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///     


    public delegate void pathHendlerDelegate(object sender, pathEventArgs e);

    public partial class MainWindow : Window
    {
        private String inputFilePath;
        private String outputPath = @".\output.txt";//AppDomain.CurrentDomain.BaseDirectory + "\\output.txt";        

        public event pathHendlerDelegate pathHandler;

        public MainWindow()
        {
            InitializeComponent();           
        }

        private void openFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                // Open document
                inputFilePath = dlg.FileName;
                //pathHandler(this, new pathEventArgs(filename));
                if (null != inputFilePath)
                    handleFile(inputFilePath);
                
            }
        }

        private void handleFile(String path)
        {
            String line;
            StringBuilder strBuilder = new StringBuilder();
            using (System.IO.StreamReader file = new System.IO.StreamReader(path, System.Text.Encoding.GetEncoding(1251)))
            {
                int lineNumber = 1;

                while ((line = file.ReadLine()) != null)
                {
                    CardNumberStringHandler.handleCardNumber(line, strBuilder, lineNumber);
                    lineNumber++;
                }
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(outputPath))
            {
                String[] separator = {Environment.NewLine, "\n"};
                String[] lines = strBuilder.ToString().Split(separator, StringSplitOptions.None);
                foreach (String str  in lines)
                {
                    file.WriteLine(str);
                }
            }

            outText.Text = strBuilder.ToString();
        }

        private void setOutputPathButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                outputPath = filename;
            }
        }

        private void fixFileButton_Click(object sender, RoutedEventArgs e)
        {
            FixFileWindow ffw = new FixFileWindow();
            pathHandler += ffw.setFilePath;
            pathHandler(this, new pathEventArgs(inputFilePath));
            ffw.ShowDialog();
        }        
    }

    public class pathEventArgs : EventArgs
    {
        private String path;
        public pathEventArgs(String _path)
        {
            this.path = _path; 
        }

        public String Path
        {
            get { return path;}
            private set {}
        }
    }

   
}
