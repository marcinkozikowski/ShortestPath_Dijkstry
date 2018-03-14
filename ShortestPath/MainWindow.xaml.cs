using ShortestPath.IO_Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        int[,] mainGraph;

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

        public int[,] MainGraph
        {
            get
            {
                return mainGraph;
            }

            set
            {
                mainGraph = value;
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
                    MainGraph = fv.getGraphPaths(CitiesNumber);
                    StringTextBox.Text = WriteGraphAsString();
                    ShowGraphInDataGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Nie można odczytać pliku z danymi: " + ex.Message);
            }
        }

        private void ShowGraphInDataGrid()
        {
            DataTable dt = new DataTable();
            int nbColumns = CitiesNumber;
            int nbRows = CitiesNumber;

            for (int i = 0; i < nbColumns; i++)
            {
                dt.Columns.Add(("City "+(i+1)).ToString());
            }
            //dt.Rows.Add();

            for (int row = 0; row < nbRows; row++)
            {
                DataRow dr = dt.NewRow();
                
                for (int col = 0; col < nbColumns; col++)
                {
                        dr[col] = MainGraph[row, col]; 
                }
                dt.Rows.Add(dr);
            }

            InputDataGrid.ItemsSource = dt.DefaultView;
        }

        private string WriteGraphAsString()
        {
            string graphString="";

            for(int i=0;i<CitiesNumber;i++)
            {
                for(int j=0;j<CitiesNumber;j++)
                {
                    graphString = graphString +" "+ MainGraph[i, j]; 
                }
                graphString = graphString + "\n";
            }
            return graphString;

        }   //wypisz grasz jako liste dwuwymiarowa

        private void ExitAppClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void SaveFileClick(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        private void DijkstryClick(object sender, RoutedEventArgs e)
        {
            int INF = Dijkstry.INF;
            int SRC = SourceNumber;
            int DEST = DestNumber;
            var dijkstra = new Dijkstry(MainGraph);
            int[] path = dijkstra.GetPath(SRC-1, DEST-1);

            string pathDi = "Shortest path from "+SourceNumber+" to: "+DestNumber+" is:\n";
            for(int i=0;i<path.Length;i++)
            {
                pathDi = pathDi+ (path[i]+1)+ " -> ";
            }
            pathDi = pathDi+ "\nIt costs: " + dijkstra.getPathDistance();

            StringTextBox.Text = pathDi;
            
        }

        private void BFSClick(object sender, RoutedEventArgs e)
        {
            int INF = Dijkstry.INF;
            int SRC = SourceNumber;
            int DEST = DestNumber;
            var bfs = new BFS(MainGraph,SRC);
            bfs.getBFSPath();
            List<int> path = bfs.getBFSPathToPoint(DEST);

            string pathS="BFS Shortes way is: \n";
            path.Reverse();
            foreach (int a in path)
            {
                pathS = pathS + " -> "+ (a+1);
            }

            pathS = pathS + "\nThis way costs: " + (path.Count()-1);
            StringTextBox.Text = pathS;
        }
    }
}
