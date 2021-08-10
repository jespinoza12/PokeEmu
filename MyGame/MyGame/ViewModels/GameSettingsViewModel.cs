using MyGame.Commands;
using MyGame.EventHandlers;
using MyGame.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;

namespace MyGame.ViewModels
{
    public class GameSettingsViewModel
    {
        public event EventHandler<GameUpdatedHandler> GameUpdated;

        public GameSettingsViewModel(Game game)
        {
            Game = game;
            UpdateCommand = new GameUpdateCommand(this);
        }

        /// <summary>
        /// Gets of sets a System.Boolean value indicating whether the Game can be updated
        /// </summary>
        public bool CanUpdate
        {
            get
            {
                if(Game == null)
                {
                    return false;
                }
                return !String.IsNullOrWhiteSpace(Game.PlayerName);
            }
        }


        public Game Game { get; set; }

        /// <summary>
        /// Get the UpdateCommand for the view model
        /// </summary>
        public ICommand UpdateCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Persist the changes to the datasource
        /// Save changes made to Game instance
        /// </summary>
        public void SaveChanges()
        {
            GameUpdated(this, new GameUpdatedHandler(Game));
        }
    }
}
