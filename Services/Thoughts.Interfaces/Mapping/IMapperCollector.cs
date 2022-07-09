using Thoughts.DAL.Entities;
using Thoughts.Interfaces.Base.Mapping;
using CategoryDom = Thoughts.Domain.Base.Entities.Category;
using CommentDom = Thoughts.Domain.Base.Entities.Comment;
using PostDom = Thoughts.Domain.Base.Entities.Post;
using RoleDom = Thoughts.Domain.Base.Entities.Role;
using TagDom = Thoughts.Domain.Base.Entities.Tag;
using UserDom = Thoughts.Domain.Base.Entities.User;

namespace Thoughts.Interfaces.Mapping
{
    public interface IMapperCollector
    {
        IMapper<CategoryDom, Category>? CategoryMapper { get; }
        IMapper<CommentDom, Comment>? CommentMapper { get; }
        IMapper<PostDom, Post>? PostMapper { get; }
        IMapper<RoleDom, Role>? RoleMapper { get; }
        IMapper<TagDom, Tag>? TagMapper { get; }
        IMapper<UserDom, User>? UserMapper { get; }
    }
}
