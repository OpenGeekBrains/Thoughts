using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Thoughts.UI.WPF.Infrastructure.Commands;
using Thoughts.UI.WPF.ViewModels.Base;
using Thoughts.UI.WPF.Views;

namespace Thoughts.UI.WPF.ViewModels
{
    internal class MainWindowViewModel: ViewModel
    {
        public static string Title => "Hello";
        private ViewModel _CurrrentView;


        public ViewModel CurrentView
        {
            get => _CurrrentView;
            set
            {
                _CurrrentView = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        private ICommand _RecordsButtonCheckedCommand;
        public ICommand RecordButtonCheckedCommand => _RecordsButtonCheckedCommand ?? new RelayCommand(OnRecordsButtonCheckedCommand, CanRecordsButtonCheckedCommandExecute);

        private bool CanRecordsButtonCheckedCommandExecute(object? p) => true;

        private void OnRecordsButtonCheckedCommand(object? p) => CurrentView = new RecordsViewModel();

        private ICommand _FilesButtonCheckedCommand;
        public ICommand FilesButtonCheckedCommand => _FilesButtonCheckedCommand ?? new RelayCommand(OnFilesButtonCheckedCommand, CanFilesButtonCheckedCommandExecute);

        private bool CanFilesButtonCheckedCommandExecute(object? p) => true;

        private void OnFilesButtonCheckedCommand(object? p) => CurrentView = new FilesViewModel();


        private ICommand _UsersButtonCheckedCommand;
        public ICommand UsersButtonCheckedCommand => _UsersButtonCheckedCommand ?? new RelayCommand(OnUsersButtonCheckedCommand, CanUsersButtonCheckedCommandExecute);
        private bool CanUsersButtonCheckedCommandExecute(object? p) => true;
        private void OnUsersButtonCheckedCommand(object? p) => CurrentView = new UsersViewModel();

        #endregion


        public MainWindowViewModel()
        {
            
        }
    }
}
