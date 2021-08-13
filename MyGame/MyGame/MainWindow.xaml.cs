using MyGame.Models;
using MyGame.ViewModels;
using MyGame.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

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
                currentHealth1 = 0;
                txtHP1.Text = currentHealth1 + " / " + gengar.hp;

            }
            if (currentHealth2 <= 0)
            {
                currentHealth2 = 0;
                txtHP2.Text = currentHealth2 + "/" + pikachu.hp;
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

            txtUpdateUser.Text = "Choose a move " + game.PlayerName;


        }
        //Check for crits or misses
        private string critOrMiss()
        {
            string critOrMiss = "";
            int rValue = r.Next(1, 101);
            if (rValue <= 80)
            {
                critOrMiss = "Hit";
            }
            if (rValue >= 91 && rValue <= 100)
            {
                critOrMiss = "Crit";
            }
            if (rValue >= 81 && rValue <= 90)
            {
                critOrMiss = "Missed";
            }

            return critOrMiss;
        }
        //Check totalDmgDealt including defense values
        private int totalDmgDealt(int moveDmg, int pDefense, string critOrMiss)
        {
            int totalDmgDealt = moveDmg - pDefense / 4;
            if (critOrMiss == "Hit")
            {
                totalDmgDealt = moveDmg - pDefense / 4;
            }
            if (critOrMiss == "Missed")
            {
                totalDmgDealt = 0;

            }
            if (critOrMiss == "Crit")
            {
                totalDmgDealt = totalDmgDealt * 2;
            }

            return totalDmgDealt;
        }
        //Quits game
        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //Gets random move for cpu to choose
        private void cpuMove(string critOrMis)
        {

            int move1Dmg = 45;
            int move2dmg = 55;
            int move3dmg = 65;
            int move4dmg = 60;
            int whichMove = r.Next(1, 5);

            if (whichMove == 1)
            {
                currentHealth2 -= totalDmgDealt(move1Dmg, gengar.defense, critOrMis);
                Hbar_2.Value = currentHealth2;
                updateCHealth();
            }
            if (whichMove == 2)
            {
                currentHealth2 -= totalDmgDealt(move2dmg, gengar.defense, critOrMis);
                Hbar_2.Value = currentHealth2;
                updateCHealth();
            }
            if (whichMove == 3)
            {
                currentHealth2 -= totalDmgDealt(move3dmg, gengar.defense, critOrMis);
                Hbar_2.Value = currentHealth2;
                updateCHealth();
            }
            if (whichMove == 4)
            {
                currentHealth2 -= totalDmgDealt(move4dmg, gengar.defense, critOrMis);
                Hbar_2.Value = currentHealth2;
                updateCHealth();
            }
        }
        //This is to check move dmg and using total dmg dealt deals dmg (Want to add speed stats to see who goes first)
        private void btnMove_click(object sender, RoutedEventArgs e)
        {
            string currentMove = (sender as Button).Name;
            string critOrMis = critOrMiss();
            string critOrMis2 = critOrMiss();
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1.5) };

            switch (currentMove)
            {
                case "btnMove1":
                    mGrid.Visibility = Visibility.Hidden;
                    int dmgDealt = 100;
                    if (check_Speed() == pikachu.Name)
                    {
                        currentHealth1 -= totalDmgDealt(dmgDealt, gengar.defense, critOrMis);
                        Hbar_1.Value = currentHealth1;
                        txtUpdateUser.Text = "Pikachu used " + btnMove1.Content + " and it " + critOrMis + " attack";
                        gif.Visibility = Visibility.Visible;
                        timer.Start();
                        timer.Tick += (sender, args) =>
                        {
                            timer.Stop();
                            txtUpdateUser.Text = "Gengar attack " + critOrMis2;
                            cpuMove(critOrMis2);
                            updateCHealth();
                            
                            sGrid.Visibility = Visibility.Visible;
                            mGrid.Visibility = Visibility.Hidden;
                            txtUpdateUser.Text = "Choose a move " + game.PlayerName;
                            gif.Visibility = Visibility.Hidden;
                            checkForWin();

                        };
                    }
                    if (check_Speed() == gengar.Name)
                    {
                        cpuMove(critOrMis2);
                        txtUpdateUser.Text = "Gengar attack " + critOrMis2;
                        gif.Visibility = Visibility.Visible;
                        timer.Start();
                        timer.Tick += (sender, args) =>
                        {
                            timer.Stop();
                            txtUpdateUser.Text = "Pikachu used " + btnMove1.Content + " and it was a " + critOrMis + " attack";
                            currentHealth1 -= totalDmgDealt(dmgDealt, gengar.defense, critOrMis);
                            Hbar_1.Value = currentHealth1;
                            updateCHealth();
                            checkForWin();
                            sGrid.Visibility = Visibility.Visible;
                            mGrid.Visibility = Visibility.Hidden;
                            txtUpdateUser.Text = "Choose a move " + game.PlayerName;
                            gif.Visibility = Visibility.Hidden;

                            checkForWin();
                        };
                    }
                    break;
                case "btnMove2":
                    dmgDealt = 50;
                    mGrid.Visibility = Visibility.Hidden;
                    if (check_Speed() == pikachu.Name)
                    {

                        currentHealth1 -= totalDmgDealt(dmgDealt, gengar.defense, critOrMis);
                        Hbar_1.Value = currentHealth1;
                        txtUpdateUser.Text = "Pikachu used " + btnMove2.Content + " and it was a " + critOrMis + " attack";
                        gif.Visibility = Visibility.Visible;
                        timer.Start();
                        timer.Tick += (sender, args) =>
                        {
                            timer.Stop();
                            txtUpdateUser.Text = "Gengar attack " + critOrMis2;
                            cpuMove(critOrMis2);
                            updateCHealth();
  
                            sGrid.Visibility = Visibility.Visible;
                            mGrid.Visibility = Visibility.Hidden;
                            txtUpdateUser.Text = "Choose a move " + game.PlayerName;
                            gif.Visibility = Visibility.Hidden;

                            checkForWin();
                        };
                    }
                    if (check_Speed() == gengar.Name)
                    {
                        cpuMove(critOrMis2);
                        txtUpdateUser.Text = "Gengar attack " + critOrMis2;
                        gif.Visibility = Visibility.Visible;
                        timer.Start();
                        timer.Tick += (sender, args) =>
                        {
                            timer.Stop();
                            txtUpdateUser.Text = "Pikachu used " + btnMove2.Content + " and it was a " + critOrMis + " attack";
                            currentHealth1 -= totalDmgDealt(dmgDealt, gengar.defense, critOrMis);
                            Hbar_1.Value = currentHealth1;
                            updateCHealth();
                            sGrid.Visibility = Visibility.Visible;
                            mGrid.Visibility = Visibility.Hidden;
                            txtUpdateUser.Text = "Choose a move " + game.PlayerName;
                            gif.Visibility = Visibility.Hidden;
                            checkForWin();


                        };
                    }
                    break;
                case "btnMove3":
                    pikachu.speed = 5000;
                    dmgDealt = 45;
                    mGrid.Visibility = Visibility.Hidden;

                    if (check_Speed() == pikachu.Name)
                    {
                        currentHealth1 -= totalDmgDealt(dmgDealt, gengar.defense, critOrMis);
                        Hbar_1.Value = currentHealth1;
                        txtUpdateUser.Text = "Pikachu used " + btnMove3.Content + " and it was a " + critOrMis + " attack";
                        gif.Visibility = Visibility.Visible;
                        timer.Start();
                        timer.Tick += (sender, args) =>
                        {
                            timer.Stop();
                            txtUpdateUser.Text = "Gengar attack " + critOrMis2;
                            cpuMove(critOrMis2);
                            updateCHealth();
                            pikachu.speed = 166;
                            sGrid.Visibility = Visibility.Visible;
                            mGrid.Visibility = Visibility.Hidden;
                            txtUpdateUser.Text = "Choose a move " + game.PlayerName;
                            gif.Visibility = Visibility.Hidden;
                            checkForWin();


                        };

                    }
                    break;
                case "btnMove4":
                    dmgDealt = 65;
                    mGrid.Visibility = Visibility.Hidden;
                    if (check_Speed() == pikachu.Name)
                    {
                        currentHealth1 -= totalDmgDealt(dmgDealt, gengar.defense, critOrMis);
                        Hbar_1.Value = currentHealth1;
                        txtUpdateUser.Text = "Pikachu used " + btnMove4.Content + " and it was a " + critOrMis + " attack";
                        gif.Visibility = Visibility.Visible;

                        timer.Start();
                        timer.Tick += (sender, args) =>
                        {
                            timer.Stop();
                            txtUpdateUser.Text = "Gengar attack " + critOrMis2;
                            cpuMove(critOrMis2);
                            updateCHealth();
                            sGrid.Visibility = Visibility.Visible;
                            mGrid.Visibility = Visibility.Hidden;
                            txtUpdateUser.Text = "Choose a move " + game.PlayerName;
                            gif.Visibility = Visibility.Hidden;
                            checkForWin();


                        };
                    }
                    if (check_Speed() == gengar.Name)
                    {
                        cpuMove(critOrMis2);
                        txtUpdateUser.Text = "Gengar attack " + critOrMis2;
                        gif.Visibility = Visibility.Visible;

                        timer.Start();

                        timer.Tick += (sender, args) =>
                        {
                            timer.Stop();
                            txtUpdateUser.Text = "Pikachu used " + btnMove4.Content + " and it was a " + critOrMis + " attack";
                            currentHealth1 -= totalDmgDealt(dmgDealt, gengar.defense, critOrMis);
                            Hbar_1.Value = currentHealth1;
                            updateCHealth();
                            
                            sGrid.Visibility = Visibility.Visible;
                            mGrid.Visibility = Visibility.Hidden;
                            gif.Visibility = Visibility.Hidden;

                            txtUpdateUser.Text = "Choose a move " + game.PlayerName;
                            checkForWin();

                        };
                        
                    }
                    break;
            }
        }
        private string check_Speed()
        {
            string whoFirst = "";
            if (gengar.speed > pikachu.speed)
            {
                whoFirst = gengar.Name;
            }
            if (pikachu.speed > gengar.speed)
            {
                whoFirst = pikachu.Name;
            }
            return whoFirst;
        }
        //Checks to see if someone won
        private void checkForWin()
        {
          
            if (Hbar_1.Value <= 0)
            {
                updateCHealth();
                txtUpdateUser.Text = "Gengar has feinted the winner is " + game.PlayerName;
            }
            if (Hbar_2.Value <= 0)
            {
                updateCHealth();
                txtUpdateUser.Text = "Pikachu has feineted the winner is CPU";
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
