using MyGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.EventHandlers
{
    public class GameUpdatedHandler : EventArgs
    {
        public Game Game { get; set; }

        public GameUpdatedHandler(Game game)
        {
            Game = game;
        }
    }
}
