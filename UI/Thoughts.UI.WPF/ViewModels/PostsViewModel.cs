using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;

using Thoughts.DAL.Entities;
using Thoughts.Interfaces.Base.Repositories;
using Thoughts.Services;
using Thoughts.Services.Data;
using Thoughts.UI.WPF.Infrastructure.Commands;
using Thoughts.UI.WPF.ViewModels.Base;

namespace Thoughts.UI.WPF.ViewModels
{
    internal class PostsViewModel: ViewModel
    {
        private Post _selectedPost;


        public ObservableCollection<Post> Posts { get; } = new();
        public ObservableCollection<Category> Categories { get; } = new();
        public ObservableCollection<Tag> Tags { get; } = new();
        public ObservableCollection<Tag> UnusedTags { get; set; } = new();



        public Post SelectedPost
        {
            get => _selectedPost;
            set
            {
                Set(ref _selectedPost, value);
                var temp = Tags.Where(tag => !SelectedPost.Tags.Contains(tag)).ToList();
                UnusedTags.Clear();
                foreach (var tag in temp)
                {
                    UnusedTags.Add(tag);
                }
            }
        }


        #region Commands

        private RelayCommand _addPostComand;

        public RelayCommand AddPostComand => _addPostComand ??=
            (_addPostComand = new RelayCommand(OnAddPostCommand, CanAddPostCommandExecute));
        public bool CanAddPostCommandExecute(object? arg) => true;

        public void OnAddPostCommand(object? obj)
        {
            Post x = new Post();
            Posts.Add(x);
            SelectedPost = x;
        }


        private RelayCommand _deleteTagCommand;
        public RelayCommand DeleteTagCommand => _deleteTagCommand ??=
            (_deleteTagCommand = new RelayCommand(OnDeleteTagCommand, CanDeleteTagCommandExecute));


        private bool CanDeleteTagCommandExecute(object? arg) => true;

        private void OnDeleteTagCommand(object? obj)
        {

        }
        #endregion




        public PostsViewModel()
        {
            foreach (var item in TestDbData.Posts)
            {
                Posts.Add(item);
            }

            foreach (var category in TestDbData.Categories)
            {
                Categories.Add(category);
            }

            foreach (var tag in TestDbData.Tags)
            {
                Tags.Add(tag);
            }
        }

        //private static async void Load<T>(ObservableCollection<T> collection, IRepository<T> rep) where T : Entity
        //{
        //    collection.Clear();
        //    var temp = await rep.GetAll().ConfigureAwait(false);
        //    foreach (var item in temp)
        //       collection.Add(item);
        //}
        
    }
}
