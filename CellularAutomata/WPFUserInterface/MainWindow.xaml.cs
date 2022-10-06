using System.Windows;
using WPFUserInterface.ViewModels;

namespace WPFUserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BoardVM BoardVM { get; set; }

        public int BoardHeight
        {
            get => BoardVM.BoardHeight;
            set => BoardVM.BoardHeight = value;
        }

        public int BoardWidth
        {
            get => BoardVM.BoardWidth;
            set => BoardVM.BoardWidth=value;
        }

        public MainWindow()
        {
            InitializeComponent();
            BoardVM = new BoardVM();
            DataContext = this;
        }
    }
}

