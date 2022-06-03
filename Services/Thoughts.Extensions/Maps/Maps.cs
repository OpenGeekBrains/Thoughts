using AutoMapper;
using Thoughts.Domain.Base.Entities;

using CategoryDomain = Thoughts.Domain.Base.Entities.Category;
using StatusDomain = Thoughts.Domain.Base.Entities.Status;
using CommentDomain = Thoughts.Domain.Base.Entities.Comment;
using PostDomain = Thoughts.Domain.Base.Entities.Post;
using RoleDomain = Thoughts.Domain.Base.Entities.Role;
using CategoryDal = Thoughts.DAL.Entities.Category;
using StatusDal = Thoughts.DAL.Entities.Status;
using CommentDal = Thoughts.DAL.Entities.Comment;
using PostDal = Thoughts.DAL.Entities.Post;
using RoleDal = Thoughts.DAL.Entities.Role;
using File = Thoughts.DAL.Entities.File;

namespace Thoughts.Extensions.Maps
{
    /// <summary>Класс маппинга из БД в Domain</summary>
    public static class Maps
    {
        //зачем категории хранить в себе коллекцию постов, которые в неё входят?
        //зачем ролям хранить коллекцию юзеров?
        //зачем тэгам хранить коллекцию постов?

        /// <summary>Преобразование статуса из БД в Domain форму</summary>
        /// <param name="dalEntity">Статус из БД</param>
        /// <returns>Статус Domian</returns>
        public static StatusDomain ToDomain(this StatusDal dalEntity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<StatusDal, StatusDomain>()
                .ForMember("Id", opt => opt.MapFrom(s => s.Id))
                .ForMember("Name", opt => opt.MapFrom(s => s.Name)));

            var mapper = new Mapper(config);

            return mapper.Map<StatusDomain>(dalEntity);
        }

        /// <summary>Преобразование категории из БД в Domain форму</summary>
        /// <param name="dalEntity">Категория из БД</param>
        /// <returns>Категория Domian</returns>
        public static CategoryDomain ToDomain(this CategoryDal dalEntity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDal, CategoryDomain>()
                .ForMember("Id", opt => opt.MapFrom(s => s.Id))
                .ForMember("Name", opt => opt.MapFrom(s => s.Name))
                .ForMember("Status", opt => opt.MapFrom(s => s.Status))
                .ForMember("Posts", opt => opt.MapFrom(s => s.Posts))); //зачем коллекцию постов хранить?

            var mapper = new Mapper(config);

            return mapper.Map<CategoryDomain>(dalEntity);
        }


        /// <summary>Преобразование комментария из БД в Domain форму</summary>
        /// <param name="dalEntity">Комментарий из БД</param>
        /// <returns>Комментарий Domian</returns>
        public static CommentDomain ToDomain(this CommentDal dalEntity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CommentDal, CommentDomain>()
                .ForMember("Id", opt => opt.MapFrom(s => s.Id))
                .ForMember("Date", opt => opt.MapFrom(s => s.Date))
                .ForMember("Post", opt => opt.MapFrom(s => s.Post)) //здесь нужно метод маппинга добавить
                .ForMember("User", opt => opt.MapFrom(s => s.Post)) //здесь нужно метод маппинга добавить
                .ForMember("Body", opt => opt.MapFrom(s => s.Body))
                .ForMember("ParentComment", opt => opt.MapFrom(s => s.ParentComment.ToDomain())) // здесь у нас рекурсия пошла
                .ForMember("ChildrenComment", opt => opt.MapFrom(s => s.ChildrenComment.Select(c => c.ChildrenComment.Select(cc => cc.ToDomain()).ToList()))) // здесь у нас тоже рекурсия будет
                .ForMember("isDeleted", opt => opt.MapFrom(s => s.IsDeleted)));

            var mapper = new Mapper(config);

            return mapper.Map<CommentDomain>(dalEntity);
        }

        /// <summary>Преобразование фаила из БД в Domain форму</summary>
        /// <param name="dalEntity">Фаил из БД</param>
        /// <returns>Фаил Domian</returns>
        public static FileModel ToDomain(this File dalEntity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<File, FileModel>()
                .ForMember("Id", opt => opt.MapFrom(s => s.Id))
                .ForMember("Name", opt => opt.MapFrom(s => s.Name))
                .ForMember("Description", opt => opt.MapFrom(s => s.FileDescription))
                .ForMember("Content", opt => opt.MapFrom(s => s.FileBody)));

            var mapper = new Mapper(config);

            return mapper.Map<FileModel>(dalEntity);
        }

        /// <summary>Преобразование поста из БД в Domain форму</summary>
        /// <param name="dalEntity">Пост из БД</param>
        /// <returns>Пост Domian</returns>
        public static PostDomain ToDomain(this PostDal dalEntity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PostDal, PostDomain>()
                .ForMember("Id", opt => opt.MapFrom(s => s.Id))
                .ForMember("Date", opt => opt.MapFrom(s => s.Date)) // тут переделывают дату на DateTimeOffset
                .ForMember("User", opt => opt.MapFrom(s => s.User)) // тут нужно метод маппинга добавить
                .ForMember("Title", opt => opt.MapFrom(s => s.Title))
                .ForMember("Body", opt => opt.MapFrom(s => s.Body))
                .ForMember("Category", opt => opt.MapFrom(s => s.Category.ToDomain())) // здесь мы вызвали наш метод маппинга
                .ForMember("Tags", opt => opt.MapFrom(s => s.Tags))
                .ForMember("Comments", opt => opt.MapFrom(s => s.Comments.Select(c => c.ToDomain()).ToList()))  // здесь мы вызвали наш метод маппинга
                .ForMember("PublicationsDate", opt => opt.MapFrom(s => s.DatePublicatione))
                .ForMember("Files", opt => opt.MapFrom(s => s.Files.Select(f => f.ToDomain()).ToList())));  // здесь мы вызвали наш метод маппинга

            var mapper = new Mapper(config);

            return mapper.Map<PostDomain>(dalEntity);
        }

        /// <summary>Преобразование роли из БД в Domain форму</summary>
        /// <param name="dalEntity">Роль из БД</param>
        /// <returns>Роль Domian</returns>
        public static RoleDomain ToDomain(this RoleDal dalEntity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RoleDal, RoleDomain>()
                .ForMember("Id", opt => opt.MapFrom(s => s.Id))
                .ForMember("Name", opt => opt.MapFrom(s => s.Name))
                .ForMember("Users", opt => opt.MapFrom(s => s.Users))); // здесь нужно будет добавить метод маппинга

            var mapper = new Mapper(config);

            return mapper.Map<RoleDomain>(dalEntity);
        }
    }
}
