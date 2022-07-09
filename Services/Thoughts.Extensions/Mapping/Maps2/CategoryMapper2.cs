using PostDal = Thoughts.DAL.Entities.Post;
using PostDom = Thoughts.Domain.Base.Entities.Post;
using StatusDom = Thoughts.Domain.Base.Entities.Status;
using CategoryDom = Thoughts.Domain.Base.Entities.Category;
using Thoughts.DAL.Entities;
using Thoughts.Interfaces.Base.Mapping;
using Thoughts.Interfaces.Mapping;

namespace Thoughts.Extensions.Mapping.Maps2;

public class CategoryMapper2 : IMapper<Category, CategoryDom>
{
    private readonly IMemoizCash _memoiz;
    public CategoryMapper2(IMemoizCash memoiz)
    {
        _memoiz = memoiz;
    }

    public Category? MapBack(CategoryDom? item)
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
                tmpPost = new PostDal() { Id = post.Id };
            }
            cat.Posts.Add(tmpPost);
        }

        return cat;
    }

    public CategoryDom? Map(Category? item)
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
                tmpPost = new PostDom() { Id = post.Id };
            }
            cat.Posts.Add(tmpPost);
        }

        return cat;
    }
}