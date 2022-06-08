using AutoMapper;

namespace Thoughts.UI.MVC.Infrastructure.AutoMapper;

public class PostsProfile : Profile
{
    public PostsProfile()
    {
        CreateMap<Domain.Base.Entities.Post, DAL.Entities.Post>()
           .ReverseMap();
    }
}
