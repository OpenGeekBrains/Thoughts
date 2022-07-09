using Thoughts.DAL.Entities;
using Thoughts.Interfaces.Base.Mapping;
using Thoughts.Interfaces.Mapping;
using CategoryDom = Thoughts.Domain.Base.Entities.Category;
using CommentDom = Thoughts.Domain.Base.Entities.Comment;
using PostDom = Thoughts.Domain.Base.Entities.Post;
using RoleDom = Thoughts.Domain.Base.Entities.Role;
using TagDom = Thoughts.Domain.Base.Entities.Tag;
using UserDom = Thoughts.Domain.Base.Entities.User;

namespace Thoughts.Extensions.Mapping
{
    public class MapperCollector : IMapperCollector
    {
        IMapper<CategoryDom, Category>? _categoryMapper;
        IMapper<CommentDom, Comment>? _commentMapper;
        IMapper<PostDom, Post>? _postMapper;
        IMapper<RoleDom, Role>? _roleMapper;
        IMapper<TagDom, Tag>? _tagMapper;
        IMapper<UserDom, User>? _userMapper;

        public MapperCollector(
            IMapper<CategoryDom, Category> categoryMapper,
            IMapper<CommentDom, Comment> commentMapper,
            IMapper<PostDom, Post> postMapper,
            IMapper<RoleDom, Role> roleMapper,
            IMapper<TagDom, Tag> tagMapper,
            IMapper<UserDom, User> userMapper
            )
        {
            _categoryMapper = categoryMapper;
            _commentMapper = commentMapper;
            _postMapper = postMapper;
            _roleMapper = roleMapper;
            _tagMapper = tagMapper;
            _userMapper = userMapper;
        }
        public IMapper<CategoryDom, Category>? CategoryMapper
        {
            get 
            {
                if (_categoryMapper is null) return null;
                return _categoryMapper; 
            }
        }
        public IMapper<CommentDom, Comment>? CommentMapper
        {
            get
            {
                if (_commentMapper is null) return null;
                return _commentMapper;
            }
        }
        public IMapper<PostDom, Post>? PostMapper
        {
            get
            {
                if (_postMapper is null) return null;
                return _postMapper;
            }
        }
        public IMapper<RoleDom, Role>? RoleMapper
        {
            get
            {
                if (_roleMapper is null) return null;
                return _roleMapper;
            }
        }
        public IMapper<TagDom, Tag>? TagMapper
        {
            get
            {
                if (_tagMapper is null) return null;
                return _tagMapper;
            }
        }
        public IMapper<UserDom, User>? UserMapper
        {
            get
            {
                if (_userMapper is null) return null;
                return _userMapper;
            }
        }
    }
}
