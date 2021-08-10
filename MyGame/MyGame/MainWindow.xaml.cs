using MyGame.Models;
using MyGame.ViewModels;
using MyGame.Views;
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

namespace MyGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Game game;
        int pH;
        int p2H;
        int currentHealth1;
        int currentHealth2;
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

            gameStart();
            

            //Game should have values set
            
        }

        public int Maximum { get; set; }

        private void gameStart()
        {
            Pokemon pikachu = new Electric("Pikachu", 180, 103, 76, 166, "Ground");
            Pokemon gengar = new Dark("Gengar", 230, 121, 112, 202, "Ground/Ghost/Psychic/Dark");

            currentHealth1 = gengar.hp;
            currentHealth2 = pikachu.hp;

            Hbar_1.Maximum = gengar.hp;
            Hbar_2.Maximum = pikachu.hp;

            txtHP1.Text = currentHealth1 + " / " + gengar.hp;
            txtHP2.Text = currentHealth2 + "/" + pikachu.hp;
        }
        
        public void updateCHealth()
        {
            Pokemon pikachu = new Electric("Pikachu", 180, 103, 76, 166, "Ground");
            Pokemon gengar = new Dark("Gengar", 230, 121, 112, 202, "Ground/Ghost/Psychic/Dark");
            txtHP1.Text = currentHealth1 + " / " + gengar.hp;
            txtHP2.Text = currentHealth2 + "/" + pikachu.hp;
        }
        //For Buttons to test health
        private void HPCheck_Click(object sender, RoutedEventArgs e)
        {
            Hbar_1.Value -= 10;
            Hbar_2.Value -= 20;
        }

        private void HPCheck2_Click(object sender, RoutedEventArgs e)
        {
            Hbar_2.Value += 10;
            Hbar_2.Value += 20;
        }

        private void btnFight_Click(object sender, RoutedEventArgs e)
        {
            sGrid.Visibility = Visibility.Hidden;
            mGrid.Visibility = Visibility.Visible;
            btnMove1.Content = "Thunderbolt";
            btnMove2.Content = "ElectroShock";
            btnMove3.Content = "Quick Attack";
            btnMove4.Content = "Iron Tail";

        
        }

        private void btnSwitch_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnBag_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Hbar_2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }

        private void Hbar_1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }

        private void cpuMove()
        {
            int move1Dmg = 20;
            int move2dmg = 30;
            int move3dmg = 40;
            int move4dmg = 50;
            Random r = new Random();
            int whichMove = r.Next(1, 5);

            if (whichMove == 1)
            {
                Hbar_2.Value -= move1Dmg;
                currentHealth2 -= move1Dmg;
                updateCHealth();
            }
            if (whichMove == 2)
            {
                Hbar_2.Value -= move2dmg;
                currentHealth2 -= move2dmg;
                updateCHealth();
            }
            if (whichMove == 3)
            {
                Hbar_2.Value -= move3dmg;
                currentHealth2 -= move3dmg;
                updateCHealth();
            }
            if (whichMove == 4)
            {
                Hbar_2.Value -= move4dmg;
                currentHealth2 -= move4dmg;
                updateCHealth();
            }
        }
        private void btnMove_click(object sender, RoutedEventArgs e)
        {
            string currentMove = (sender as Button).Name;

            switch (currentMove)
            {
                case "btnMove1":
                    int dmgDealt = 80;
                    currentHealth1 -= dmgDealt;
                    Hbar_1.Value -= dmgDealt;
                    updateCHealth();
                    cpuMove();
                    checkForWin();
                    break;
                case "btnMove2":
                    dmgDealt = 30;
                    currentHealth1 -= dmgDealt;
                    Hbar_1.Value -= dmgDealt;
                    updateCHealth();
                    cpuMove();
                    checkForWin();
                    break;
                case "btnMove3":
                    dmgDealt = 40;
                    currentHealth1 -= dmgDealt;
                    Hbar_1.Value -= dmgDealt;
                    updateCHealth(); 
                    cpuMove();
                    checkForWin();
                    break;
                case "btnMove4":
                    dmgDealt = 60;
                    currentHealth1 -= dmgDealt;
                    Hbar_1.Value -= dmgDealt;
                    updateCHealth();
                    cpuMove();
                    checkForWin();
                    break;
            }
                
          
        }

        private void checkForWin()
        {
            if (Hbar_1.Value <= 0)
            {
                txtUpdateUser.Text = "Congrat player 2 wins";
            }
            if (Hbar_2.Value <= 0)
            {
                txtUpdateUser.Text = "Aw boo cpu wins";
            }
        }
    }


    public abstract class Pokemon
    {
        public int hp { get; set; }
        public int attack { get; set; }
        public int defense { get; set; }
        public int speed { get; set; }
        public string Name
        {
            get;
            set;
        }

        public Pokemon() { }

        
        public Pokemon(string name, int hp, int attack, int defense, int speed)
        {
            this.Name = name;
            this.hp = hp;
            this.attack = attack;
            this.defense = defense;
            this.speed = speed;
        }

    }
    class Electric : Pokemon
    {
        private string weakness;

        public Electric(string name, int hp, int attack, int defense, int speed, string weakness)
        {
            this.Name = name;
            this.hp = hp;
            this.attack = attack;
            this.defense = defense;
            this.speed = speed;
            this.weakness = weakness;
        }
    }
    class Dark: Pokemon
    {
        private string weakness;
        public Dark(string name, int hp, int attack, int defense, int speed, string weakness)
        {
            this.Name = name;
            this.hp = hp;
            this.attack = attack;
            this.defense = defense;
            this.speed = speed;
            this.weakness = weakness;
        }
    }
}