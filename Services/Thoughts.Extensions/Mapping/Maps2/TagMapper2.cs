using PostDal = Thoughts.DAL.Entities.Post;
using TagDal = Thoughts.DAL.Entities.Tag;
using PostDom = Thoughts.Domain.Base.Entities.Post;
using TagDom = Thoughts.Domain.Base.Entities.Tag;
using Thoughts.Interfaces.Base.Mapping;
using Thoughts.Interfaces.Mapping;

namespace Thoughts.Extensions.Mapping.Maps2;

public class TagMapper2 : IMapper<TagDal, TagDom>
{
    private readonly IMemoizCash _memoiz;

    public TagMapper2(IMemoizCash memoiz)
    {
        _memoiz = memoiz;
    }

    public TagDal? MapBack(TagDom? item)
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
                tmpPost = new PostDal() { Id = post.Id };

            tag.Posts.Add(tmpPost);
        }

        return tag;
    }

    public TagDom? Map(TagDal? item)
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
                tmpPost = new PostDom() { Id = post.Id };

            tag.Posts.Add(tmpPost);
        }

        return tag;
    }
}