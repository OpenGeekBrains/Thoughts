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
using Thoughts.Interfaces.Base.Repositories;
using Thoughts.Services.Mapping;
using Thoughts.Services.InSQL;

namespace Thoughts.Mapping.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IMemoizCash, MemoizCash>();
            services.AddScoped<IMapperCollector, MapperCollector>();
            services.AddScoped<IMapper<Category, CategoryDom>, CategoryMapper2>();
            services.AddScoped<IMapper<Comment, CommentDom>, CommentMapper2>();
            services.AddScoped<IMapper<Post, PostDom>, PostMapper2>();
            services.AddScoped<IMapper<Role, RoleDom>, RoleMapper2>();
            services.AddScoped<IMapper<Tag, TagDom>, TagMapper2>();
            services.AddScoped<IMapper<User, UserDom>, UserMapper2>();
        }
    }
}
