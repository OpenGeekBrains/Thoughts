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

namespace Thoughts.Extensions.Mapping.Maps;

public class TagMapper : IMapper<TagDom, TagDal>
{
    private readonly IMapper<PostDom, PostDal> _mapper;
    private readonly ICash<TagDal, TagDom, int> cash;
    private readonly Dictionary<int, TagDal> _dalCash = new();
    private readonly Dictionary<int, TagDom> _domCash = new();

    public TagMapper(IMapper<PostDom, PostDal> mapper, ICash<TagDal, TagDom, int> cash)
    {
        _mapper = mapper;
        this.cash = cash;
        cash._domCash.
    }

    public void AddToCash(object? obj)
    {
        if (obj is null) return;

        if (obj is TagDal)
        {
            var tmp = obj as TagDal;
            _dalCash.Add(tmp.Id, tmp);
        }
        else if (obj is TagDom)
        {
            var tmp = obj as TagDom;
            _domCash.Add(tmp.Id, tmp);
        }
        throw new ArgumentException($"{nameof(TagMapper)} cant add this object in cash");
    }

    public object GetDalObject(object key)
    {
        if (key is null) throw new ArgumentException($"Key is null");
        if (key is not int) throw new ArgumentException($"{nameof(TagMapper)} cash doesn't use this type of key");

        var tmpKey = (int)key;
        return _dalCash[tmpKey];

    }

    public object GetDomainObject(object key)
    {
        if (key is null) throw new ArgumentException($"Key is null");
        if (key is not int) throw new ArgumentException($"{nameof(TagMapper)} cash doesn't use this type of key");

        var tmpKey = (int)key;
        return _domCash[tmpKey];
    }

    public TagDal? Map(TagDom? item)
    {
        if (item is null) return default;

        var tag = new TagDal
        {
            Id = item.Id,
            Name = item.Name,
        };
        AddToCash(tag);

        foreach (var post in item.Posts)
        {
            var tmpObj = _mapper.GetDalObject(post.Id);
            if (tmpObj is not null && tmpObj is PostDal)
            {
                var tmpPost = tmpObj as PostDal;
                tag.Posts.Add(tmpPost!);
            }
            else
                tag.Posts.Add(_mapper.Map(post));
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
        AddToCash(tag);

        foreach (var post in item.Posts)
        {
            var tmpObj = _mapper.GetDomainObject(post.Id);
            if (tmpObj is not null && tmpObj is PostDom)
            {
                var tmpPost = tmpObj as PostDom;
                tag.Posts.Add(tmpPost!);
            }
            else
                tag.Posts.Add(_mapper.MapBack(post));
        }

        return tag;
    }
}