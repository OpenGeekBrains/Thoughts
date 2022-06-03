using AutoMapper;
using Thoughts.Domain.Base.Entities;

using CategoryDomain = Thoughts.Domain.Base.Entities.Category;
using StatusDomain = Thoughts.Domain.Base.Entities.Status;
using CommentDomain = Thoughts.Domain.Base.Entities.Comment;
using CategoryDal = Thoughts.DAL.Entities.Category;
using StatusDal = Thoughts.DAL.Entities.Status;
using CommentDal = Thoughts.DAL.Entities.Comment;
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
                .ForMember("ChildrenComment", opt => opt.MapFrom(s => s.ChildrenComment)) // здесь у нас тоже рекурсия будет
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
    }
}
