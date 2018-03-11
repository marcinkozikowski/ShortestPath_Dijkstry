﻿using ShortestPath.IO_Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        int citiesNumber;
        int pathsNumber;
        int sourceNumber;
        int destNumber;

        #region Properties

        public int CitiesNumber
        {
            get
            {
                return citiesNumber;
            }

            set
            {
                citiesNumber = value;
            }
        }

        public int PathsNumber
        {
            get
            {
                return pathsNumber;
            }

            set
            {
                pathsNumber = value;
            }
        }

        public int SourceNumber
        {
            get
            {
                return sourceNumber;
            }

            set
            {
                sourceNumber = value;
            }
        }

        public int DestNumber
        {
            get
            {
                return destNumber;
            }

            set
            {
                destNumber = value;
            }
        }

        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }


        private void setCityAndPathsNumberLabels(int cities,int paths)
        {
            CitiesNumber = cities;
            PathsNumber = paths;
            CitiesNumberLabel.Content = CitiesNumber;
            PathsNumberLabel.Content = PathsNumber;
        }

        private void setSourceAndDestNumberLabels(int source, int dest)
        {
            SourceNumber = source;
            DestNumber = dest;
            SourceNumberLabel.Content = SourceNumber;
            DestenationNumberLabel.Content = DestNumber;
        }

        #region MenuItemsHandlers

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
                    int[] sourceAndDestNumbers = fv.getSourceAndDestanationPoints();
                    setCityAndPathsNumberLabels(cityAndPathsNumber[0], cityAndPathsNumber[1]);
                    setSourceAndDestNumberLabels(sourceAndDestNumbers[0], sourceAndDestNumbers[1]);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }

        private void ExitAppClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void SaveFileClick(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
