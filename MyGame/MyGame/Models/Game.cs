using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Models
{
    public class Game : INotifyPropertyChanged
    {
        public Game()
        {
            this.Difficulty = 1;
        }

        public Game(string playerName, int difficulty)
        {
            this.PlayerName = playerName += "s Turn";
            this.Difficulty = difficulty;
        }

        private string playerName;

        public string PlayerName
        {
            get { return playerName; }
            set { 
                playerName = value;
                OnPropertyChanged("PlayerName");
            }
        }

        private int difficulty;

        public int Difficulty
        {
            get { return difficulty; }
            set { 
                difficulty = value;
                OnPropertyChanged("Difficulty");
            }
        }



        #region INotifiyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
