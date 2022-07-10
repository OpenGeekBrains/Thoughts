using Thoughts.DAL.Entities;
using Thoughts.Interfaces.Base;

using RoleDAL = Thoughts.DAL.Entities.Role;
using RoleDOM = Thoughts.Domain.Base.Entities.Role;


namespace Thoughts.Extensions.Maps;

public class RoleMapper : IMapper<RoleDOM, Role>
{
    public RoleDOM? Map(RoleDAL? role_dal)
    {
        if (role_dal is null) 
            return default;

        var role_dom = new RoleDOM
        {
            Id = role_dal.Id,
            Name = role_dal.Name,
        };

        return role_dom;
    }

    public RoleDAL? Map(RoleDOM? role_dom)
    {
        if (role_dom is null) return default;

        var role = new Role
        {
            Id = role_dom.Id,
            Name = role_dom.Name,
        };

        return role;
    }
}