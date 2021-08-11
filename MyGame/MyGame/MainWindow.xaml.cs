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
        int currentHealth1;
        int currentHealth2;
        Random r = new Random();
        Pokemon pikachu = new Electric("Pikachu", 180, 103, 76, 166, "Ground");
        Pokemon gengar = new Dark("Gengar", 230, 121, 112, 202, "Ground/Ghost/Psychic/Dark");

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

        }

        public int Maximum { get; set; }

        //Starts Game
        private void gameStart()
        {
            currentHealth1 = gengar.hp;
            currentHealth2 = pikachu.hp;

            Hbar_1.Maximum = gengar.hp;
            Hbar_2.Maximum = pikachu.hp;

            txtHP1.Text = currentHealth1 + " / " + gengar.hp;
            txtHP2.Text = currentHealth2 + "/" + pikachu.hp;
        }
        //Updates Current Health in healthBar
        public void updateCHealth()
        {
            if (currentHealth1 <= 0)
            {
                txtHP1.Text = "0 / " + gengar.hp;
            }
            if (currentHealth2 <= 0)
            {
                txtHP2.Text = "0 / " + pikachu.hp;
            }
            else
            {
                txtHP1.Text = currentHealth1 + " / " + gengar.hp;
                txtHP2.Text = currentHealth2 + "/" + pikachu.hp;
            }


        }
        //Swaps to move menu
        private void btnFight_Click(object sender, RoutedEventArgs e)
        {
            sGrid.Visibility = Visibility.Hidden;
            mGrid.Visibility = Visibility.Visible;
            btnMove1.Content = "Thunderbolt";
            btnMove2.Content = "ElectroShock";
            btnMove3.Content = "Quick Attack";
            btnMove4.Content = "Iron Tail";


        }
        //Check for crits or misses
        private string critOrMiss()
        {
            string critOrMiss = "";
            int rValue = r.Next(1, 101);
            if (rValue <= 80)
            {
                critOrMiss = "Regular";
            }
            if (rValue >= 91 && rValue <= 100)
            {
                critOrMiss = "Miss";
            }
            if (rValue >= 81 && rValue <= 90)
            {
                critOrMiss = "Miss";
            }

            return critOrMiss;
        }
        //Check totalDmgDealt including defense values
        private int totalDmgDealt(int moveDmg, int pDefense, string critOrMiss)
        {
            int totalDmgDealt = moveDmg - pDefense / 4;
            if (critOrMiss == "Regular")
            {
                totalDmgDealt = moveDmg - pDefense / 4;
            }
            if (critOrMiss == "Miss")
            {
                totalDmgDealt = 0;

            }
            if (critOrMiss == "Crit")
            {
                int basicDmg = moveDmg - pDefense;
                totalDmgDealt = basicDmg * 2;
            }

            return totalDmgDealt;
        }
        //Unimplimented
        private void btnSwitch_Click(object sender, RoutedEventArgs e)
        {

        }
        //Unimplimented
        private void btnBag_Click(object sender, RoutedEventArgs e)
        {

        }
        //Quits game
        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //Gets random move for cpu to choose
        private void cpuMove()
        {
            int move1Dmg = 20;
            int move2dmg = 30;
            int move3dmg = 40;
            int move4dmg = 50;
            int whichMove = r.Next(1, 5);

            if (whichMove == 1)
            {
                currentHealth2 -= totalDmgDealt(move1Dmg, gengar.defense, critOrMiss());
                Hbar_2.Value = currentHealth2;
                updateCHealth();
            }
            if (whichMove == 2)
            {
                currentHealth2 -= totalDmgDealt(move2dmg, gengar.defense, critOrMiss());
                Hbar_2.Value = currentHealth2;
                updateCHealth();
            }
            if (whichMove == 3)
            {
                currentHealth2 -= totalDmgDealt(move3dmg, gengar.defense, critOrMiss());
                Hbar_2.Value = currentHealth2;
                updateCHealth();
            }
            if (whichMove == 4)
            {
                currentHealth2 -= totalDmgDealt(move4dmg, gengar.defense, critOrMiss());
                Hbar_2.Value = currentHealth2;
                updateCHealth();
            }
        }
        //This is to check move dmg and using total dmg dealt deals dmg (Want to add speed stats to see who goes first)
        private void btnMove_click(object sender, RoutedEventArgs e)
        {
            string currentMove = (sender as Button).Name;

            switch (currentMove)
            {
                case "btnMove1":
                    int dmgDealt = 80;
                    currentHealth1 -= totalDmgDealt(dmgDealt, gengar.defense, critOrMiss());
                    Hbar_1.Value = currentHealth1;
                    updateCHealth();
                    cpuMove();
                    checkForWin();
                    sGrid.Visibility = Visibility.Visible;
                    mGrid.Visibility = Visibility.Hidden;
                    break;
                case "btnMove2":
                    dmgDealt = 30;
                    currentHealth1 -= totalDmgDealt(dmgDealt, gengar.defense, critOrMiss());
                    Hbar_1.Value = currentHealth1;
                    updateCHealth();
                    cpuMove();
                    checkForWin();
                    sGrid.Visibility = Visibility.Visible;
                    mGrid.Visibility = Visibility.Hidden;
                    break;
                case "btnMove3":
                    dmgDealt = 40;
                    currentHealth1 -= totalDmgDealt(dmgDealt, gengar.defense, critOrMiss());
                    Hbar_1.Value = currentHealth1;
                    updateCHealth();
                    cpuMove();
                    checkForWin();
                    sGrid.Visibility = Visibility.Visible;
                    mGrid.Visibility = Visibility.Hidden;
                    break;
                case "btnMove4":
                    dmgDealt = 60;
                    currentHealth1 -= totalDmgDealt(dmgDealt, gengar.defense, critOrMiss());
                    Hbar_1.Value = currentHealth1;
                    updateCHealth();
                    cpuMove();
                    checkForWin();
                    sGrid.Visibility = Visibility.Visible;
                    mGrid.Visibility = Visibility.Hidden;
                    break;
            }


        }
        //Checks to see if someone won
        private void checkForWin()
        {

            if (Hbar_1.Value <= 0)
            {
                updateCHealth();
                txtUpdateUser.Text = "Gengar has feighnted the winner is " + game.PlayerName;
            }
            if (Hbar_2.Value <= 0)
            {
                updateCHealth();
                txtUpdateUser.Text = "Pikachu has feighneted the winner is CPU";
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
    class Dark : Pokemon
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
