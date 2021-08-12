using System;
using MyGame.Models;
using System.Windows;
using MyGame.ViewModels;
using MyGame.EventHandlers;

namespace MyGame.Views
{
    /// <summary>
    /// Interaction logic for Difficulty.xaml
    /// </summary>
    public partial class GameSettings : Window
    {
        public GameSettings(Game game)
        {
            InitializeComponent();
            DataContext = new GameSettingsViewModel(game);
            ((GameSettingsViewModel)DataContext).GameUpdated += GameSettings_GameUpdated;
        }

        private void GameSettings_GameUpdated(object sender, EventHandlers.GameUpdatedHandler e)
        {
            this.Close();
        }

       
    }
}
