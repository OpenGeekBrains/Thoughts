using AutoMapper;

using CategoryDomain = Thoughts.Domain.Base.Entities.Category;
using CategoryDal = Thoughts.DAL.Entities.Category;

namespace Thoughts.Extensions.Maps
{
    public static class Maps
    {
        public static CategoryDomain ToDomain(this CategoryDal categoryDal)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDal, CategoryDomain>()
                .ForMember("Id", opt => opt.MapFrom(s => s.Id))
                .ForMember("Name", opt => opt.MapFrom(s => s.Name))
                .ForMember("Status", opt => opt.MapFrom(s => s.Status))
                .ForMember("Posts", opt => opt.MapFrom(s => s.Posts)));

            var mapper = new Mapper(config);

            return mapper.Map<CategoryDomain>(categoryDal);
        }
    }
}
