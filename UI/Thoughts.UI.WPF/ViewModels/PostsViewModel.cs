using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Thoughts.DAL.Entities.Base;
using Thoughts.Domain.Base.Entities;
using Thoughts.Interfaces;
using Thoughts.Interfaces.Base.Repositories;
using Thoughts.Services;
using Thoughts.Services.Data;
using Thoughts.UI.WPF.Infrastructure.Commands;
using Thoughts.UI.WPF.ViewModels.Base;

namespace Thoughts.UI.WPF.ViewModels
{
    internal class PostsViewModel: ViewModel
    {
        private readonly IRepository<Post> _Posts;
        private RepositoryBlogPostManager _repo;
        private Post _selectedPost;


        public ObservableCollection<Post> Posts { get; } = new();
        public RepositoryBlogPostManager Repo
        {
            get => _repo;
            set => Set(ref _repo, value);
        }


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

        public PostsViewModel(RepositoryBlogPostManager repo, IRepository<Post> posts)
        {
            _repo = Repo;
            _Posts = posts;
        }

        private static async void Load<T>(ObservableCollection<T> collection, IRepository<T> rep) where T : Entity
        {
            collection.Clear();
            var temp = await rep.GetAll().ConfigureAwait(false);
            foreach (var item in temp)
               collection.Add(item);
        }

        private void LoadData()
        {
            Load<Post>(Posts, _Posts);
        }
    }
}
