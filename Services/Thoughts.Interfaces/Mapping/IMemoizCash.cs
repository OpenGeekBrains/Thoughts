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

namespace Thoughts.Interfaces.Mapping
{
    public interface IMemoizCash: IDisposable
    {
        ICash<int, CategoryDal> CategorysDal { get; }
        ICash<int, CategoryDom> CategorysDomain { get; }
        ICash<int, CommentDal> CommentsDal { get; }
        ICash<int, CommentDom> CommentsDomain { get; }
        ICash<int, PostDal> PostsDal { get; }
        ICash<int, PostDom> PostsDomain { get; }
        ICash<int, RoleDal> RolesDal { get; }
        ICash<int, RoleDom> RolesDomain { get; }
        ICash<int, TagDal> TagsDal { get; }
        ICash<int, TagDom> TagsDomain { get; }
        ICash<string, UserDal> UsersDal { get; }
        ICash<string, UserDom> UsersDomain { get; }
    }
}
