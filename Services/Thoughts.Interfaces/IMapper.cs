using System.Diagnostics.CodeAnalysis;

namespace Thoughts.Interfaces;

public interface IMapper<out T>
{
    [return: NotNullIfNotNull("source")]
    T? Map(object? source);
}

public interface IMapper<TIn, TOut>
{
    [return: NotNullIfNotNull("source")]
    TOut? Map(TIn? source);

    [return: NotNullIfNotNull("source")]
    TIn? Map(TOut? source);
}
