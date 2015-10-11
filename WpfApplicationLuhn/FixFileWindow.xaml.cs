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
using System.Windows.Shapes;

namespace WpfApplicationLuhn
{
    /// <summary>
    /// Interaction logic for FixFileWindow.xaml
    /// </summary>
    public partial class FixFileWindow : Window
    {
        private String fixFilePath = "";
        private String newFilePath;
        public FixFileWindow()
        {
            InitializeComponent();
        }

        public void setFilePath(object sender, pathEventArgs e)
        {
            fixFilePath = e.Path;
            fullTextBox();
        }

        private void fullTextBox()
        {
            String line;
            StringBuilder strBuilder = new StringBuilder();
            if (fixFilePath != null)
            {
                using (System.IO.StreamReader file = new System.IO.StreamReader(fixFilePath, System.Text.Encoding.GetEncoding(1251)))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        fixTextBox.Text += line + "\n";
                    
                    }
                }                
            }  
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                // Open document
                newFilePath = dlg.FileName;
            }

            if (null != newFilePath)
                saveChanges();
        }

        private void saveChanges()
        {            
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(newFilePath, false, Encoding.GetEncoding("Windows-1251")))
            {                
                String[] separator = { Environment.NewLine, "\n" };
                String[] lines = fixTextBox.Text.Split(separator, StringSplitOptions.None);
                foreach (String str in lines)
                {
                    file.WriteLine(str);
                }
            }
            this.Close();
        }
    }
}
