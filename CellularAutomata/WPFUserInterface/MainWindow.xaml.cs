using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Microsoft.Xaml.Behaviors.Core;
using WPFUserInterface.Annotations;
using WPFUserInterface.Domain;

namespace WPFUserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<SimpleCell> _cells;

        public ObservableCollection<SimpleCell> Cells
        {
            get => _cells;
            set
            {
                _cells = value; 
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            WidthInput.Text = "10";
            HeightInput.Text = "10";
            CellSizeInput.Text = "15";
            Cells = new ObservableCollection<SimpleCell>();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void DrawBoard(int width, int height, int cellSize)
        {
            Cells.Clear();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var position = new Point(j * cellSize + 1.0, i * cellSize + 1.0);
                    Cells.Add(new(position, cellSize));
                }
            }
        }

        public void DrawBoardCommand(object sender, RoutedEventArgs routedEventArgs)
        {
            Int32.TryParse(WidthInput.Text, out int width);
            Int32.TryParse(HeightInput.Text, out int height);
            Int32.TryParse(CellSizeInput.Text, out int size);
            DrawBoard(width, height, size);

        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

