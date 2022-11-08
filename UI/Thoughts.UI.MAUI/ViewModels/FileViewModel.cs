using Thoughts.UI.MAUI.ViewModels.Base;

namespace Thoughts.UI.MAUI.ViewModels
{
    public class FileViewModel : ViewModel
    {
        #region Constructors

        public FileViewModel()
        {

        }

        #endregion

        #region Bindable properties

        private string _hash;

        public string Hash
        {
            get => _hash;

            set => Set(ref _hash, value);
        }

        private string _name;

        public string Name
        {
            get => _name;

            set => Set(ref _name, value);
        }

        private int _linksCount;

        public int LinksCount
        {
            get => _linksCount;

            set => Set(ref _linksCount, value);
        }

        private long _size;

        public long Size
        {
            get => _size;

            set => Set(ref _size, value);
        }

        private DateTimeOffset _created;

        public DateTimeOffset Created
        {
            get => _created;

            set => Set(ref _created, value);
        }

        private bool _active;

        public bool Active
        {
            get => _active;

            set => Set(ref _active, value);
        }

        #endregion
    }
}
