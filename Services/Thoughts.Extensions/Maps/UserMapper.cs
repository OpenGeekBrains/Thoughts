using Thoughts.DAL.Entities;
using Thoughts.Interfaces.Base;
using UserDom = Thoughts.Domain.Base.Entities.User;
using RoleDAL = Thoughts.DAL.Entities.Role;
using RoleDOM = Thoughts.Domain.Base.Entities.Role;
using StatusDAL = Thoughts.DAL.Entities.Status;
using StatusDOM = Thoughts.Domain.Base.Entities.Status;

namespace Thoughts.Extensions.Maps;

public class UserMapper : IMapper<UserDom, User>, IMapper<User, UserDom>
{
    private static StatusDOM ToDOM(StatusDAL status_dal) => status_dal switch
    {
        StatusDAL.Blocked => StatusDOM.Blocked,
        StatusDAL.Private => StatusDOM.Private,
        StatusDAL.Protected => StatusDOM.Protected,
        StatusDAL.Public => StatusDOM.Public,
        _ => (StatusDOM)(int)status_dal
    };

    private static StatusDAL ToDAL(StatusDOM status_dal) => status_dal switch
    {
        StatusDOM.Blocked => StatusDAL.Blocked,
        StatusDOM.Private => StatusDAL.Private,
        StatusDOM.Protected => StatusDAL.Protected,
        StatusDOM.Public => StatusDAL.Public,
        _ => (StatusDAL)(int)status_dal
    };
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
            Status = ToDAL(item.Status),
            Roles = item.Roles.Select(role => new RoleDAL
            {
                Id = role.Id,
                Name = role.Name
            }).ToArray(),
        };

        return user;
    }

    public UserDom? Map(User? item)
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
            Status = ToDOM(item.Status),
            Roles = item.Roles.Select(role => new RoleDOM
            {
                Id = role.Id,
                Name = role.Name
            }).ToArray(),
        };

        return user;
    }
}