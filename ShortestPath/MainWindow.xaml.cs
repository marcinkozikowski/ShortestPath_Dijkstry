using ShortestPath.IO_Operations;
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

namespace ShortestPath
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            string filePath = "";
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

                try
                {
                    openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    if (openFileDialog1.ShowDialog() == true)
                    {
                        filePath = openFileDialog1.FileName;
                        FileReader fr = new FileReader(filePath);
                    string[] readedFile = fr.ReadAllLines();
                    ReadedLinesValidation fv = new ReadedLinesValidation(readedFile);
                    int[] cityAndPathsNumber = fv.getCitiesAndPathNumber();
                    setCityAndPathsNumberLabels(cityAndPathsNumber[0], cityAndPathsNumber[1]);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
        }

        private void setCityAndPathsNumberLabels(int cities,int paths)
        {
            CitiesNumberLabel.Content = cities;
            PathsNumberLabel.Content = paths;
        }
    }
}
