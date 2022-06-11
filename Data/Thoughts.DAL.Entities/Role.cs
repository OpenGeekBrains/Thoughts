﻿using Microsoft.EntityFrameworkCore;

using Thoughts.DAL.Entities.Base;

namespace Thoughts.DAL.Entities;

[Index(nameof(Name), IsUnique = true, Name = "NameIndex")]
public class Role : NamedEntity
{
    public ICollection<User> Users { get; set; } = new HashSet<User>();

    public Role() { }

    public Role(string name) => Name = name;

    public override string ToString() => Name;
}
