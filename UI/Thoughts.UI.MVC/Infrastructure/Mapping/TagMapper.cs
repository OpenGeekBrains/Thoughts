using Thoughts.Domain.Base.Entities;
using Thoughts.Interfaces;

namespace Thoughts.UI.MVC.Infrastructure.Mapping;

public class TagMapper : IMapper<Tag>, IMapper<Tag, DAL.Entities.Tag>
{
    public Tag? Map(object? source) => source switch
    {
        null => null,
        DAL.Entities.Tag tag => new Tag(tag.Name),
        _ => throw new NotSupportedException($"Тип объектов {source.GetType()} не поддерживается для проекции в тип {typeof(Tag)}")
    };

    public DAL.Entities.Tag? Map(Tag? source) => throw new NotImplementedException();

    public Tag? Map(DAL.Entities.Tag? source) => throw new NotImplementedException();
}
