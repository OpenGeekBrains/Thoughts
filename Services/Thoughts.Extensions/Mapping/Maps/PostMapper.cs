using PostDal = Thoughts.DAL.Entities.Post;
using StatusDal = Thoughts.DAL.Entities.Status;
using CategoryDal = Thoughts.DAL.Entities.Category;
using CommentDal = Thoughts.DAL.Entities.Comment;
using TagDal = Thoughts.DAL.Entities.Tag;
using UserDal = Thoughts.DAL.Entities.User;
using PostDom = Thoughts.Domain.Base.Entities.Post;
using StatusDom = Thoughts.Domain.Base.Entities.Status;
using CategoryDom = Thoughts.Domain.Base.Entities.Category;
using CommentDom = Thoughts.Domain.Base.Entities.Comment;
using TagDom = Thoughts.Domain.Base.Entities.Tag;
using UserDom = Thoughts.Domain.Base.Entities.User;
using Thoughts.Interfaces.Base.Mapping;
using Thoughts.Interfaces.Mapping;

namespace Thoughts.Extensions.Mapping.Maps;

public class PostMapper : IMapper<PostDom, PostDal>
{
    private readonly IMemoizCash _memoiz;
    private readonly IMapper<UserDom, UserDal> _userMapper;
    private readonly IMapper<CategoryDom, CategoryDal> _categoryMapper;
    private readonly IMapper<TagDom, TagDal> _tagMapper;
    private readonly IMapper<CommentDom, CommentDal> _commentMapper;

    public PostMapper(IMemoizCash memoiz, IMapper<UserDom, UserDal> userMapper, IMapper<CategoryDom, CategoryDal> categoryMapper, IMapper<TagDom, TagDal> tagMapper, IMapper<CommentDom, CommentDal> commentMapper)
    {
        _memoiz = memoiz;
        _userMapper = userMapper;
        _categoryMapper = categoryMapper;
        _tagMapper = tagMapper;
        _commentMapper = commentMapper;
    }
    public PostDom? MapBack(PostDal? item)
    {
        if (item is null) return default;

        var post = new PostDom
        {
            Id = item.Id,
            Status = (StatusDom)item.Status,
            Date = item.Date,
            User = new UserDom
            {
                Id = item.User.Id,
            },
            Title = item.Title,
            Body = item.Body,
            PublicationsDate = item.PublicationDate,
        };
        _memoiz.PostsDomain.Cash.Add(post.Id, post);

        if (_memoiz.UsersDomain.Cash.ContainsKey(post.User.Id))
        {
            post.User = _memoiz.UsersDomain.Cash[post.User.Id];
        }
        else
        {
            post.User = _userMapper.MapBack(item.User);
        }

        if (_memoiz.CategorysDomain.Cash.ContainsKey(item.Category.Id))
        {
            post.Category = _memoiz.CategorysDomain.Cash[item.Category.Id];
        }
        else
        {
            post.Category = _categoryMapper.MapBack(item.Category);
        }

        foreach (var tag in item.Tags)
        {
            TagDom tmpTag;
            if (_memoiz.TagsDomain.Cash.ContainsKey(tag.Id))
            {
                tmpTag = _memoiz.TagsDomain.Cash[tag.Id];
            }
            else
            {
                tmpTag = _tagMapper.MapBack(tag);
            }
            post.Tags.Add(tmpTag);
        }

        foreach (var comment in item.Comments)
        {
            CommentDom tmpComment;
            if (_memoiz.CommentsDomain.Cash.ContainsKey(comment.Id))
            {
                tmpComment = _memoiz.CommentsDomain.Cash[comment.Id];
            }
            else
            {
                tmpComment = _commentMapper.MapBack(comment);
            }
            post.Comments.Add(tmpComment);
        }

        return post;
    }


    public PostDal? Map(PostDom? item)
    {
        if (item is null) return default;

        var post = new PostDal
        {
            Id = item.Id,
            Status = (StatusDal)item.Status,
            Date = item.Date,
            Title = item.Title,
            Body = item.Body,
            PublicationDate = item.PublicationsDate,
        };
        _memoiz.PostsDal.Cash.Add(post.Id, post);

        if (_memoiz.UsersDal.Cash.ContainsKey(post.User.Id))
        {
            post.User = _memoiz.UsersDal.Cash[post.User.Id];
        }
        else
        {
            post.User = _userMapper.Map(item.User);
        }

        if (_memoiz.CategorysDal.Cash.ContainsKey(item.Category.Id))
        {
            post.Category = _memoiz.CategorysDal.Cash[item.Category.Id];
        }
        else
        {
            post.Category = _categoryMapper.Map(item.Category);
        }

        foreach (var tag in item.Tags)
        {
            TagDal tmpTag;
            if (_memoiz.TagsDal.Cash.ContainsKey(tag.Id))
            {
                tmpTag = _memoiz.TagsDal.Cash[tag.Id];
            }
            else
            {
                tmpTag = _tagMapper.Map(tag);
            }
            post.Tags.Add(tmpTag);
        }

        foreach (var comment in item.Comments)
        {
            CommentDal tmpComment;
            if (_memoiz.CommentsDal.Cash.ContainsKey(comment.Id))
            {
                tmpComment = _memoiz.CommentsDal.Cash[comment.Id];
            }
            else
            {
                tmpComment = _commentMapper.Map(comment);
            }
            post.Comments.Add(tmpComment);
        }

        return post;
    }

}