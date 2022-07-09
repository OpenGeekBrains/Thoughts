using PostDal = Thoughts.DAL.Entities.Post;
using PostDom = Thoughts.Domain.Base.Entities.Post;
using StatusDom = Thoughts.Domain.Base.Entities.Status;
using CategoryDom = Thoughts.Domain.Base.Entities.Category;
using PostDAL = Thoughts.DAL.Entities.Post;
using Thoughts.DAL.Entities;
using Thoughts.Interfaces.Base.Mapping;
using Thoughts.Interfaces.Mapping;

namespace Thoughts.Extensions.Mapping.Maps;

public class CategoryMapper : IMapper<CategoryDom, Category>
{
    private readonly IMemoizCash _memoiz;
    private readonly IMapper<PostDom, PostDAL> _postMapper;

    //public CategoryMapper(IMemoizCash memoiz, IMapper<PostDom, PostDal> postMapper)
    //{
    //    _memoiz = memoiz;
    //    _postMapper = postMapper;
    //}
    public CategoryMapper(IMemoizCash memoiz, IMapperCollector collector)
    {
        _memoiz = memoiz;
        _postMapper = collector.PostMapper!;
    }

    public Category? Map(CategoryDom? item)
    {
        if (item is null) return default;
        var cat = new Category
        {
            Id = item.Id,
            Name = item.Name,
            Status = (Status)item.Status,
        };
        _memoiz.CategorysDal.Cash.Add(cat.Id, cat);

        foreach (var post in item.Posts)
        {
            PostDal tmpPost;
            if (_memoiz.PostsDal.Cash.ContainsKey(post.Id))
            {
                tmpPost = _memoiz.PostsDal.Cash[post.Id];
            }
            else
            {
                tmpPost = _postMapper.Map(post);
            }
            cat.Posts.Add(tmpPost);
        }

        return cat;
    }

    public CategoryDom? MapBack(Category? item)
    {
        if (item is null) return default;

        var cat = new CategoryDom
        {
            Id = item.Id,
            Name = item.Name,
            Status = (StatusDom)item.Status,
        }; 
        _memoiz.CategorysDomain.Cash.Add(cat.Id, cat);

        foreach (var post in item.Posts)
        {
            PostDom tmpPost;
            if (_memoiz.PostsDomain.Cash.ContainsKey(post.Id))
            {
                tmpPost = _memoiz.PostsDomain.Cash[post.Id];
            }
            else
            {
                tmpPost = _postMapper.MapBack(post);
            }
            cat.Posts.Add(tmpPost);
        }

        return cat;
    }
}