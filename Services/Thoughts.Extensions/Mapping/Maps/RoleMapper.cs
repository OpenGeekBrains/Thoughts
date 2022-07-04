﻿using Thoughts.DAL.Entities;
using Thoughts.Interfaces.Base.Mapping;

using RoleDom = Thoughts.Domain.Base.Entities.Role;

namespace Thoughts.Extensions.Mapping.Maps;

public class RoleMapper : IMapper<RoleDom, Role>, IMapper<Role, RoleDom>
{
    public RoleDom? Map(Role? item)
    {
        if (item is null) return default;

        var role = new RoleDom
        {
            Id = item.Id,
            Name = item.Name,
        };
        MapsCash.RoleDomCash.Add(role);

        foreach (var user in item.Users)
            role.Users.Add(MapsHelper.FindUserOrMapNew(user));

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
        MapsCash.RoleDalCash.Add(role);

        foreach (var user in item.Users)
            role.Users.Add(MapsHelper.FindUserOrMapNew(user));

        return role;
    }
}