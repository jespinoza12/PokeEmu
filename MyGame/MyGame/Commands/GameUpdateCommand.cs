using MyGame.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyGame.Commands
{
    internal class GameUpdateCommand : ICommand
    {
        private GameSettingsViewModel _ViewModel;

        /// <summary>
        /// Initializes a new instance of the GameUpdateCommand class.
        /// </summary>
        /// <param name="model"></param>
        public GameUpdateCommand(GameSettingsViewModel model)
        {
            this._ViewModel = model;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _ViewModel.CanUpdate;
        }

        public void Execute(object parameter)
        {
            _ViewModel.SaveChanges();
        }
    }
}
