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

        #region Fields


        public static string Title => "Hello";
        private ViewModel? _currentView;
        private string _loggedUser;

        #endregion


        #region Propperties


        public string LoggedUser => "admin";

        public PostsViewModel Rvm { get; }

        public FilesViewModel Fvm { get; }

        public UsersViewModel Uvm { get; }

        public ViewModel? CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }



        #endregion


        #region Commands

        private ICommand _RecordsButtonCheckedCommand;

        /// <summary>
        /// Loads RecordVM to CurrentView property.
        /// </summary>
        public ICommand RecordButtonCheckedCommand => _RecordsButtonCheckedCommand ?? new RelayCommand(OnRecordsButtonCheckedCommand, CanRecordsButtonCheckedCommandExecute);

        private bool CanRecordsButtonCheckedCommandExecute(object? p) => true;

        private void OnRecordsButtonCheckedCommand(object? p) => CurrentView = Rvm;

        
        private ICommand _FilesButtonCheckedCommand;

        /// <summary>
        /// Loads FilesVM to CurrentVew property.
        /// </summary>
        public ICommand FilesButtonCheckedCommand => _FilesButtonCheckedCommand ?? new RelayCommand(OnFilesButtonCheckedCommand, CanFilesButtonCheckedCommandExecute);

        private bool CanFilesButtonCheckedCommandExecute(object? p) => true;

        private void OnFilesButtonCheckedCommand(object? p) => CurrentView = Fvm;


        private ICommand _UsersButtonCheckedCommand;
        

        /// <summary>
        /// Loads UsersVM to CurrentView property.
        /// </summary>
        public ICommand UsersButtonCheckedCommand => _UsersButtonCheckedCommand ?? new RelayCommand(OnUsersButtonCheckedCommand, CanUsersButtonCheckedCommandExecute);


        private bool CanUsersButtonCheckedCommandExecute(object? p) => true;
        private void OnUsersButtonCheckedCommand(object? p) => CurrentView = Uvm;

        #endregion


        public MainWindowViewModel(PostsViewModel rvm, FilesViewModel fvm, UsersViewModel uvm)
        {
            Rvm = rvm;
            Fvm = fvm;
            Uvm = uvm;
        }
    }
}
