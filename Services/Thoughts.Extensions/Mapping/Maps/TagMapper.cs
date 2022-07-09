using PostDal = Thoughts.DAL.Entities.Post;
using TagDal = Thoughts.DAL.Entities.Tag;
using PostDom = Thoughts.Domain.Base.Entities.Post;
using TagDom = Thoughts.Domain.Base.Entities.Tag;
using Thoughts.Interfaces.Base.Mapping;
using Thoughts.Interfaces.Mapping;

namespace Thoughts.Extensions.Mapping.Maps;

public class TagMapper : IMapper<TagDom, TagDal>
{
    private readonly IMapper<PostDom, PostDal> _postMapper;
    private readonly IMemoizCash _memoiz;

    //public TagMapper(IMemoizCash memoiz, IMapper<PostDom, PostDal> postMapper)
    //{
    //    _memoiz = memoiz;
    //    _postMapper = postMapper;
    //}

    public TagMapper(IMemoizCash memoiz, IMapperCollector collector)
    {
        _memoiz = memoiz;
        _postMapper = collector.PostMapper!;
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
                tmpPost = _postMapper.Map(post);

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
                tmpPost = _postMapper.MapBack(post);

            tag.Posts.Add(tmpPost);
        }

        return tag;
    }
}