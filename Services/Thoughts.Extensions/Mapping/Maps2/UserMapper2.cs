using RoleDal = Thoughts.DAL.Entities.Role;
using StatusDom = Thoughts.Domain.Base.Entities.Status;
using RoleDom = Thoughts.Domain.Base.Entities.Role;
using UserDom = Thoughts.Domain.Base.Entities.User;
using Thoughts.DAL.Entities;
using Thoughts.Interfaces.Base.Mapping;
using Thoughts.Interfaces.Mapping;

namespace Thoughts.Extensions.Mapping.Maps2;

public class UserMapper2 : IMapper<UserDom, User>
{
    private readonly IMemoizCash _memoiz;

    public UserMapper2(IMemoizCash memoiz)
    {
        _memoiz = memoiz;
    }

    public User? Map(UserDom? item)
    {
        if (item is null) return default;

        var user = new User
        {
            Id = item.Id,
            NickName = item.NickName,
            LastName = item.LastName,
            FirstName = item.FirstName,
            Patronymic = item.Patronymic,
            Birthday = item.Birthday,
            Status = (Status)item.Status,
        };
        _memoiz.UsersDal.Cash.Add(user.Id, user);

        foreach (var role in item.Roles)
        {
            RoleDal tmpRole;
            if (_memoiz.RolesDal.Cash.ContainsKey(role.Id))
            {
                tmpRole = _memoiz.RolesDal.Cash[role.Id];
            }
            else
            {
                tmpRole = new RoleDal() { Id = role.Id };
            }
            user.Roles.Add(tmpRole);
        }

        return user;
    }

    public UserDom? MapBack(User? item)
    {
        if (item is null) return default;

        var user = new UserDom
        {
            Id = item.Id,
            NickName = item.NickName,
            LastName = item.LastName,
            FirstName = item.FirstName,
            Patronymic = item.Patronymic,
            Birthday = item.Birthday,
            Status = (StatusDom)item.Status,
        };
        _memoiz.UsersDomain.Cash.Add(user.Id, user);

        foreach (var role in item.Roles)
        {
            RoleDom tmpRole;
            if (_memoiz.RolesDomain.Cash.ContainsKey(role.Id))
            {
                tmpRole = _memoiz.RolesDomain.Cash[role.Id];
            }
            else
            {
                tmpRole = new RoleDom() { Id = role.Id };
            }
            user.Roles.Add(tmpRole);
        }

        return user;
    }
}