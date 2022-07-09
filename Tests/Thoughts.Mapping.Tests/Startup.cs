using Microsoft.Extensions.DependencyInjection;
using CategoryDom = Thoughts.Domain.Base.Entities.Category;
using CommentDom = Thoughts.Domain.Base.Entities.Comment;
using PostDom = Thoughts.Domain.Base.Entities.Post;
using RoleDom = Thoughts.Domain.Base.Entities.Role;
using TagDom = Thoughts.Domain.Base.Entities.Tag;
using UserDom = Thoughts.Domain.Base.Entities.User;
using Thoughts.DAL.Entities;
using Thoughts.Interfaces.Base.Mapping;
using Thoughts.Extensions.Mapping.Maps;
using Thoughts.Interfaces.Mapping;
using Thoughts.Extensions.Mapping;
using Thoughts.Extensions.Mapping.Maps2;

namespace Thoughts.Mapping.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IMemoizCash, MemoizCash>();
            services.AddScoped<IMapperCollector, MapperCollector>();
            services.AddScoped<IMapper<CategoryDom, Category>, CategoryMapper2>();
            services.AddScoped<IMapper<CommentDom, Comment>, CommentMapper2>();
            services.AddScoped<IMapper<PostDom, Post>, PostMapper2>();
            services.AddScoped<IMapper<RoleDom, Role>, RoleMapper2>();
            services.AddScoped<IMapper<TagDom, Tag>, TagMapper2>();
            services.AddScoped<IMapper<UserDom, User>, UserMapper2>();
        }
    }
}
