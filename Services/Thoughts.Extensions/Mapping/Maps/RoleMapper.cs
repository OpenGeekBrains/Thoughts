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
using Thoughts.DAL.Entities;
using Thoughts.Interfaces.Base.Mapping;
using Thoughts.Interfaces.Mapping;

using RoleDom = Thoughts.Domain.Base.Entities.Role;

namespace Thoughts.Extensions.Mapping.Maps;

public class RoleMapper : IMapper<RoleDom, Role>
{
    private readonly IMemoizCash _memoiz;
    private readonly IMapper<UserDom, UserDal> _userMapper;

    public RoleMapper(IMemoizCash memoiz, IMapper<UserDom, UserDal> userMapper)
    {
        _memoiz = memoiz;
        _userMapper = userMapper;
    }

    public RoleDom? MapBack(Role? item)
    {
        if (item is null) return default;

        var role = new RoleDom
        {
            Id = item.Id,
            Name = item.Name,
        };
        _memoiz.RolesDomain.Cash.Add(role.Id, role);

        foreach (var user in item.Users)
        {
            UserDom tmpUser;
            if (_memoiz.UsersDomain.Cash.ContainsKey(user.Id))
            {
                tmpUser = _memoiz.UsersDomain.Cash[user.Id];
            }
            else
            {
                tmpUser = _userMapper.MapBack(user);
            }
        }

        return role;
    }

    public Role? Map(RoleDom? item)
    {
        if (item is null) return default;

        var role = new Role
        {
            Id = item.Id,
            Name = item.Name,
        };
        _memoiz.RolesDal.Cash.Add(role.Id, role);

        foreach (var user in item.Users)
        {
            UserDal tmpUser;
            if (_memoiz.UsersDal.Cash.ContainsKey(user.Id))
            {
                tmpUser = _memoiz.UsersDal.Cash[user.Id];
            }
            else
            {
                tmpUser = _userMapper.Map(user);
            }
        }

        return role;
    }
}