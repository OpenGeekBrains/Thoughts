using Thoughts.DAL.Entities;

using PostDal = Thoughts.DAL.Entities.Post;
using StatusDal = Thoughts.DAL.Entities.Status;
using CategoryDal = Thoughts.DAL.Entities.Category;
using CommentDal = Thoughts.DAL.Entities.Comment;
using RoleDal = Thoughts.DAL.Entities.Role;
using TagDal = Thoughts.DAL.Entities.Tag;
using UserDal = Thoughts.DAL.Entities.User;
using PostDom = Thoughts.Domain.Base.Entities.Post;
using StatusDom = Thoughts.Domain.Base.Entities.Status;
using CategoryDom = Thoughts.Domain.Base.Entities.Category;
using CommentDom = Thoughts.Domain.Base.Entities.Comment;
using RoleDom = Thoughts.Domain.Base.Entities.Role;
using TagDom = Thoughts.Domain.Base.Entities.Tag;
using UserDom = Thoughts.Domain.Base.Entities.User;
using Thoughts.Interfaces.Base.Mapping;
using Thoughts.Interfaces.Mapping;

namespace Thoughts.Extensions.Mapping.Maps;

public class TagMapper : IMapper<TagDom, TagDal>
{
    private readonly IMapper<PostDom, PostDal> _mapper;
    private readonly IMemoizCash _memoiz;

    public TagMapper(IMapper<PostDom, PostDal> mapper, IMemoizCash memoiz)
    {
        _mapper = mapper;
        _memoiz = memoiz;
    }


    public TagDal? Map(TagDom? item)
    {
        if (item is null) return default;

        var tag = new TagDal
        {
            Id = item.Id,
            Name = item.Name,
        };
        _memoiz.TagsDal.Cash.Add(tag.Id, tag);

        foreach (var post in item.Posts)
        {
            PostDal tmpPost;
            if (_memoiz.PostsDal.Cash.ContainsKey(post.Id))
                tmpPost = _memoiz.PostsDal.Cash[post.Id];
            else
                tmpPost = _mapper.Map(post);

            tag.Posts.Add(tmpPost);
        }

        return tag;
    }

    public TagDom? MapBack(TagDal? item)
    {
        if (item is null) return default;

        var tag = new TagDom
        {
            Id = item.Id,
            Name = item.Name,
        };
        _memoiz.TagsDomain.Cash.Add(tag.Id, tag);

        foreach (var post in item.Posts)
        {
            PostDom tmpPost;
            if (_memoiz.PostsDomain.Cash.ContainsKey(post.Id))
                tmpPost = _memoiz.PostsDomain.Cash[post.Id];
            else
                tmpPost = _mapper.MapBack(post);

            tag.Posts.Add(tmpPost);
        }

        return tag;
    }
}