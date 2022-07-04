using Thoughts.DAL.Entities;
using Thoughts.Interfaces.Base.Mapping;
using Thoughts.Interfaces.Mapping;
using CommentDom = Thoughts.Domain.Base.Entities.Comment;
using PostDal = Thoughts.DAL.Entities.Post;
using UserDal = Thoughts.DAL.Entities.User;
using PostDom = Thoughts.Domain.Base.Entities.Post;
using UserDom = Thoughts.Domain.Base.Entities.User;

namespace Thoughts.Extensions.Mapping.Maps;

public class CommentMapper : IMapper<CommentDom, Comment>
{
    private readonly IMemoizCash _memoiz;
    private readonly IMapper<PostDom, PostDal> _postMapper;
    private readonly IMapper<UserDom, UserDal> _userMapper;

    public CommentMapper(IMemoizCash memoiz, IMapper<PostDom, PostDal> postMapper, IMapper<UserDom, UserDal> userMapper)
    {
        _memoiz = memoiz;
        _postMapper = postMapper;
        _userMapper = userMapper;
    }

    public Comment? Map(CommentDom? item)
    {
        if (item is null) return default;

        var com = new Comment
        {
            Id = item.Id,
            Body = item.Body,
            IsDeleted = item.IsDeleted,
            Date = item.Date,
        };
        _memoiz.CommentsDal.Cash.Add(com.Id, com);

        if (_memoiz.PostsDal.Cash.ContainsKey(item.Post.Id))
        {
            com.Post = _memoiz.PostsDal.Cash[item.Post.Id];
        }
        else
        {
            com.Post = _postMapper.Map(item.Post);
        }

        if (item.ParentComment is not null && _memoiz.CommentsDal.Cash.ContainsKey(item.ParentComment.Id))
        {
            com.ParentComment = _memoiz.CommentsDal.Cash[item.ParentComment.Id];
        }
        else
        {
            com.ParentComment = Map(item.ParentComment);
        }


        if (_memoiz.UsersDal.Cash.ContainsKey(item.User.Id))
        {
            com.User = _memoiz.UsersDal.Cash[item.User.Id];
        }
        else
        {
            com.User = _userMapper.Map(item.User);
        }

        foreach (var comment in item.ChildrenComment)
        {
            Comment tmpCom;
            if (_memoiz.CommentsDal.Cash.ContainsKey(comment.Id))
            {
                tmpCom = _memoiz.CommentsDal.Cash[comment.Id];
            }
            else
            {
                tmpCom = Map(comment);
            }
            com.ChildrenComment.Add(tmpCom);
        }

        return com;
    }

    public CommentDom? MapBack(Comment? item)
    {
        if (item is null) return default;

        var com = new CommentDom
        {
            Id = item.Id,
            Body = item.Body,
            IsDeleted = item.IsDeleted,
            Date = item.Date,
        };
        _memoiz.CommentsDomain.Cash.Add(com.Id, com);

        if (_memoiz.PostsDomain.Cash.ContainsKey(item.Post.Id))
        {
            com.Post = _memoiz.PostsDomain.Cash[item.Post.Id];
        }
        else
        {
            com.Post = _postMapper.MapBack(item.Post);
        }

        if (item.ParentComment is not null && _memoiz.CommentsDomain.Cash.ContainsKey(item.ParentComment.Id))
        {
            com.ParentComment = _memoiz.CommentsDomain.Cash[item.ParentComment.Id];
        }
        else
        {
            com.ParentComment = MapBack(item.ParentComment);
        }

        if (_memoiz.UsersDomain.Cash.ContainsKey(item.User.Id))
        {
            com.User = _memoiz.UsersDomain.Cash[item.User.Id];
        }
        else
        {
            com.User = _userMapper.MapBack(item.User);
        }

        foreach (var comment in item.ChildrenComment)
        {
            CommentDom tmpCom;
            if (_memoiz.CommentsDomain.Cash.ContainsKey(comment.Id))
            {
                tmpCom = _memoiz.CommentsDomain.Cash[comment.Id];
            }
            else
            {
                tmpCom = MapBack(comment);
            }
            com.ChildrenComment.Add(tmpCom);
        }

        return com;
    }
}