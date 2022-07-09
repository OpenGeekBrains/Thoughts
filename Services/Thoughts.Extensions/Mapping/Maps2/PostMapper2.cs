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

namespace Thoughts.Extensions.Mapping.Maps2;

public class PostMapper2 : IMapper<PostDom, PostDal>
{
    private readonly IMemoizCash _memoiz;
    public PostMapper2(IMemoizCash memoiz)
    {
        _memoiz = memoiz;
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

        if (_memoiz.CategorysDomain.Cash.ContainsKey(item.Category.Id))
        {
            post.Category = _memoiz.CategorysDomain.Cash[item.Category.Id];
        }
        else
        {
            post.Category = new CategoryDom() { Id = item.Category.Id };
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
                tmpTag = new TagDom() { Id = tag.Id };
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
                tmpComment = new CommentDom() { Id = comment.Id };
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

        if (_memoiz.UsersDal.Cash.ContainsKey(item.User.Id))
        {
            post.User = _memoiz.UsersDal.Cash[item.User.Id];
        }
        else
        {
            post.User = new UserDal() { Id = item.User.Id };
        }

        if (_memoiz.CategorysDal.Cash.ContainsKey(item.Category.Id))
        {
            post.Category = _memoiz.CategorysDal.Cash[item.Category.Id];
        }
        else
        {
            post.Category = new CategoryDal() { Id = item.Category.Id };
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
                tmpTag = new TagDal() { Id = tag.Id };
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
                tmpComment = new CommentDal() { Id = comment.Id };
            }
            post.Comments.Add(tmpComment);
        }

        return post;
    }
}