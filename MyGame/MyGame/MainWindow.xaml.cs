using MyGame.Models;
using MyGame.ViewModels;
using MyGame.Views;
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

namespace MyGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Game game;

        public MainWindow()
        {
            InitializeComponent();
            game = new Game();


            //setup for GameSettings dialog. Event called when its updated.
            GameSettings settings = new GameSettings(game);

            //Show settings, and wait for it to complete. 
            settings.ShowDialog();

            //set dataContext to Game
            DataContext = game;
            

            //Game should have values set
            
        }

        private void Hbar_2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

       
        private void HPCheck_Click(object sender, RoutedEventArgs e)
        {
            Hbar_1.Value -= 10;
            Hbar_2.Value -= 20;
        }

        private void HPCheck2_Click(object sender, RoutedEventArgs e)
        {
            Hbar_1.Value += 10;
            Hbar_2.Value += 20;
        }

        private void btnFight_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSwitch_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnBag_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}