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
using WPFUserInterface.Annotations;

namespace WPFUserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Cell> _cells;

        public ObservableCollection<Cell> Cells
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
            Cells = new ObservableCollection<Cell>();
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

    public class Cell:INotifyPropertyChanged
    {
        private bool _state; 

        #region Properties

        public int Size { get; }

        public Point Position { get; }

        public bool State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged();
            }
        }

        #endregion
        
        public Cell(Point position, int size)
        {
            Position = position;
            Size = size;
            State = (new Random().Next() % 2) == 0;
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class BoolToColorConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Brushes.White;
            if (value is bool val)
            {
                return val ? Brushes.ForestGreen : Brushes.White;
            }

            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            if (value is SolidColorBrush brush)
            {
                if (brush == Brushes.Black)
                    return false;
                else
                    return true;
            }

            return false;
        }
    }
}
