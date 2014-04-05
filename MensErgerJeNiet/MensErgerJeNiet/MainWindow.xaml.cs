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
using MensErgerJeNiet.Controller;

namespace MensErgerJeNiet
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

        private void btn4Speler_Click(object sender, RoutedEventArgs e)
        {
            Board myBoard = new Board(4);
            this.Close();
        }

        private void btn3Speler_Click(object sender, RoutedEventArgs e)
        {
            Board myBoard = new Board(3);
            this.Close();
        }

        private void btn2Speler_Click(object sender, RoutedEventArgs e)
        {
            Board myBoard = new Board(2);
            this.Close();
        }

    }
}
