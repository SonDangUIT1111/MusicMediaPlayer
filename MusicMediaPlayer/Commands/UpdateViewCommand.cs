using MusicMediaPlayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MusicMediaPlayer.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModel;
        public UpdateViewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Home")
            {
                viewModel.SelectedViewmodel = new UCHomeViewModel();
            }    
            else if (parameter.ToString() == "Profile")
            {
                viewModel.SelectedViewmodel = new UCProfileViewModel();
            }
            else if (parameter.ToString() == "Playlist")
            {
                viewModel.SelectedViewmodel = new PlayListViewModel();
            }
        }
    }
}
