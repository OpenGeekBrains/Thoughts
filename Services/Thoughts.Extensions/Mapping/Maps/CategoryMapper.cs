﻿using Thoughts.DAL.Entities;

using PostDAL = Thoughts.DAL.Entities.Post;
using PostDom = Thoughts.Domain.Base.Entities.Post;
using CategoryDom = Thoughts.Domain.Base.Entities.Category;
using Thoughts.Interfaces.Base.Mapping;

namespace Thoughts.Extensions.Mapping.Maps;

public class CategoryMapper : IMapper<CategoryDom, Category>, IMapper<Category, CategoryDom>
{
    //private readonly IMapper<PostDom, Post> _PostMapper;
    //private readonly Dictionary<int, PostDAL> _PostsDAL = new();

    //public CategoryMapper(IMapper<PostDom, PostDAL> PostMapper)
    //{
    //    _PostMapper = PostMapper;
    //}

    public Category? Map(CategoryDom? item)
    {
        if (item is null) return default;
        var cat = new Category
        {
            Id = item.Id,
            Name = item.Name,
            Status = (Status)item.Status,
        };
        MapsCash.CategoryDalCash.Add(cat);

        foreach (var post in item.Posts)
            cat.Posts.Add(MapsHelper.FindPostOrMapNew(post));

        return cat;
    }

    //private readonly Dictionary<int, PostDom> _PostsDom = new();

    public CategoryDom? Map(Category? item)
    {
        if (item is null) return default;

        var cat = new CategoryDom
        {
            Id = item.Id,
            Name = item.Name,
            Status = (Domain.Base.Entities.Status)item.Status,
        };
        MapsCash.CategoryDomCash.Add(cat);

        foreach (var post in item.Posts)
            cat.Posts.Add(MapsHelper.FindPostOrMapNew(post));

        return cat;
    }
}