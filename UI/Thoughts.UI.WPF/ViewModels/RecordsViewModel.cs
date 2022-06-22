using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Thoughts.Domain.Base.Entities;
using Thoughts.Interfaces;
using Thoughts.Services;
using Thoughts.Services.Data;
using Thoughts.UI.WPF.Infrastructure.Commands;
using Thoughts.UI.WPF.ViewModels.Base;

namespace Thoughts.UI.WPF.ViewModels
{
    internal class RecordsViewModel: ViewModel
    {
        private Post _selectedPost;


        public TestDbData TestDb { get; set; }


        public Post SelectedPost
        {
            get => _selectedPost; 
            set => Set(ref _selectedPost, value);
        }



        private RelayCommand _deleteTagCommand;
        public RelayCommand DeleteTagCommand => _deleteTagCommand ??= (_deleteTagCommand = new RelayCommand(OnDeleteTagCommand, CanDeleteTagCommandExecute));


        private bool CanDeleteTagCommandExecute(object? arg) => true;

        private void OnDeleteTagCommand(object? obj)
        {
            
        }

        public RecordsViewModel(TestDbData testData) => TestDb = testData;
    }
}
