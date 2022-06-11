﻿namespace Thoughts.DAL.Entities.DefaultData;

public static class GetDefaultData
{
    public static Status[] DefaultStatus() => new Status[]
    {
        new () { Id = 1, Name = "Черновик" },
        new () { Id = 2, Name = "Опубликовано" },
        new () { Id = 3, Name = "На модерации" },
        new () { Id = 4, Name = "Заблокировано"},
    };

    public static Role[] DefaultRole() => new Role[]
    {
        new () { Id = 1, Name = "Администратор" },
        new () { Id = 2, Name = "Модератор" },
        new () { Id = 3, Name = "Автор" },
        new () { Id = 4, Name = "Гость" },
    };
}
