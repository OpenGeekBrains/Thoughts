﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thoughts.Domain.Base.Entities;

public class Post : EntityModel
{
    /// <summary>Статус записи</summary>
    [Required]
    public Status Status { get; set; } = null!;

    /// <summary>Дата записи</summary>
    public DateTimeOffset Date { get; set; }

    /// <summary>Автор</summary>
    [Required]
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    /// <summary> Внешний ключ для актора </summary>
    public string UserId { get; set; } //собственно, хитрость

    /// <summary>Заголовок записи</summary>
    [Required]
    public string Title { get; set; } = null!;

    /// <summary>Текст (тело) записи</summary>
    [Required, MinLength(20)]
    public string Body { get; set; } = null!;

    /// <summary>Категория к которой относится запись</summary>
    [Required]
    public Category Category { get; set; } = null!;

    /// <summary>Список тегов относящихся к записи</summary>
    public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();

    /// <summary>Список комментариев относящихся к записи</summary>
    public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

    /// <summary>Дата публикации</summary>
    public DateTime PublicationsDate { get; set; }

    /// <summary>Приложенные файлы</summary>
    public ICollection<FileModel> Files { get; set; } = new HashSet<FileModel>();

    public Post() { }

    public Post(
        Status status,
        DateTime date, 
        User user,
        string title,
        string body,
        Category category, 
        DateTime PublicationsDate,
        ICollection<Tag> tags, 
        ICollection<Comment> comments,
        ICollection<FileModel> files,
        string email)
    {
        Status = status;
        Date = date;
        User = user;
        Title = title;
        Body = body;
        Category = category;
        Tags = tags;
        Comments = comments;
        this.PublicationsDate = PublicationsDate;
        Files = files;
    }

    public override string ToString() => $"{Date}, {User.NickName}: {Title}";

}
