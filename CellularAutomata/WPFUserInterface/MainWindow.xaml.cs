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

