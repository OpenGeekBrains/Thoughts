using UserDal = Thoughts.DAL.Entities.User;
using RoleDom = Thoughts.Domain.Base.Entities.Role;
using UserDom = Thoughts.Domain.Base.Entities.User;
using Thoughts.DAL.Entities;
using Thoughts.Interfaces.Base.Mapping;
using Thoughts.Interfaces.Mapping;

namespace Thoughts.Extensions.Mapping.Maps2;

public class RoleMapper2 : IMapper<RoleDom, Role>
{
    private readonly IMemoizCash _memoiz;

    public RoleMapper2(IMemoizCash memoiz)
    {
        _memoiz = memoiz;
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
                tmpUser = new UserDom() { Id = user.Id };
            }
            role.Users.Add(tmpUser);
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
                tmpUser = new UserDal() { Id = user.Id };
            }
            role.Users.Add(tmpUser);
        }

        return role;
    }
}