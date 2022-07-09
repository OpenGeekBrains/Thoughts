using PostDal = Thoughts.DAL.Entities.Post;
using CategoryDal = Thoughts.DAL.Entities.Category;
using CommentDal = Thoughts.DAL.Entities.Comment;
using RoleDal = Thoughts.DAL.Entities.Role;
using TagDal = Thoughts.DAL.Entities.Tag;
using UserDal = Thoughts.DAL.Entities.User;
using PostDom = Thoughts.Domain.Base.Entities.Post;
using CategoryDom = Thoughts.Domain.Base.Entities.Category;
using CommentDom = Thoughts.Domain.Base.Entities.Comment;
using RoleDom = Thoughts.Domain.Base.Entities.Role;
using TagDom = Thoughts.Domain.Base.Entities.Tag;
using UserDom = Thoughts.Domain.Base.Entities.User;
using Thoughts.Interfaces.Base.Mapping;
using Thoughts.Interfaces.Mapping;
using Thoughts.Extensions.Mapping.Cash.DAL;
using Thoughts.Extensions.Mapping.Cash.Domain;

namespace Thoughts.Extensions.Mapping
{
    // экземпляры свойств кэша будут создаваться при первом запросе к ним
    // Scoped сущность
    public class MemoizCash : IMemoizCash
    {
        public ICash<int, CategoryDal> CategorysDal
        {
            get 
            { 
                if(_categorysDal is null)
                {
                    _categorysDal = new CategoryDalCash();
                }
                return _categorysDal;
            }
        }

        public ICash<int, CategoryDom> CategorysDomain
        {
            get
            {
                if (_categorysDomain is null)
                {
                    _categorysDomain = new CategoryDomainCash();
                }
                return _categorysDomain;
            }
        }

        public ICash<int, CommentDal> CommentsDal
        {
            get
            {
                if (_commentsDal is null)
                {
                    _commentsDal = new CommentDalCash();
                }
                return _commentsDal;
            }
        }

        public ICash<int, CommentDom> CommentsDomain
        {
            get
            {
                if (_commentsDomain is null)
                {
                    _commentsDomain = new CommentDomainCash();
                }
                return _commentsDomain;
            }
        }

        public ICash<int, PostDal> PostsDal
        {
            get
            {
                if (_postsDal is null)
                {
                    _postsDal = new PostDalCash();
                }
                return _postsDal;
            }
        }

        public ICash<int, PostDom> PostsDomain
        {
            get
            {
                if (_postsDomain is null)
                {
                    _postsDomain = new PostDomainCash();
                }
                return _postsDomain;
            }
        }

        public ICash<int, RoleDal> RolesDal
        {
            get
            {
                if (_rolesDal is null)
                {
                    _rolesDal = new RoleDalCash();
                }
                return _rolesDal;
            }
        }

        public ICash<int, RoleDom> RolesDomain
        {
            get
            {
                if (_rolesDomain is null)
                {
                    _rolesDomain = new RoleDomainCash();
                }
                return _rolesDomain;
            }
        }

        public ICash<int, TagDal> TagsDal
        {
            get
            {
                if (_tagsDal is null)
                {
                    _tagsDal = new TagDalCash();
                }
                return _tagsDal;
            }
        }

        public ICash<int, TagDom> TagsDomain
        {
            get
            {
                if (_tagsDomain is null)
                {
                    _tagsDomain = new TagDomainCash();
                }
                return _tagsDomain;
            }
        }

        public ICash<string, UserDal> UsersDal
        {
            get
            {
                if (_usersDal is null)
                {
                    _usersDal = new UserDalCash();
                }
                return _usersDal;
            }
        }

        public ICash<string, UserDom> UsersDomain
        {
            get
            {
                if (_usersDomain is null)
                {
                    _usersDomain = new UserDomainCash();
                }
                return _usersDomain;
            }
        }

        ICash<int, CategoryDal>? _categorysDal = null;
        ICash<int, CategoryDom>? _categorysDomain = null;
        ICash<int, CommentDal>? _commentsDal = null;
        ICash<int, CommentDom>? _commentsDomain = null;
        ICash<int, PostDal>? _postsDal = null;
        ICash<int, PostDom>? _postsDomain = null;
        ICash<int, RoleDal>? _rolesDal = null;
        ICash<int, RoleDom>? _rolesDomain = null;
        ICash<int, TagDal>? _tagsDal = null;
        ICash<int, TagDom>? _tagsDomain = null;
        ICash<string, UserDal>? _usersDal = null;
        ICash<string, UserDom>? _usersDomain = null;

        bool disposed = false;

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                _categorysDal?.Dispose();
                _categorysDomain?.Dispose();
                _commentsDal?.Dispose();
                _commentsDomain?.Dispose();
                _postsDal?.Dispose();
                _postsDomain?.Dispose();
                _rolesDal?.Dispose();
                _rolesDomain?.Dispose();
                _tagsDal?.Dispose();
                _tagsDomain?.Dispose();
                _usersDal?.Dispose();
                _usersDomain?.Dispose();
            }
            disposed = true;
        }
    }
}
