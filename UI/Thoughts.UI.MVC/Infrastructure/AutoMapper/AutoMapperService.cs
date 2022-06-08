using AutoMapper;

using Thoughts.Interfaces;

namespace Thoughts.UI.MVC.Infrastructure.AutoMapper;

public class AutoMapperService<T> : IMapper<T>
{
    private readonly IMapper _BaseMapper;

    public AutoMapperService(IMapper BaseMapper) => _BaseMapper = BaseMapper;

    public T Map(object source) => _BaseMapper.Map<T>(source);
}

public class AutoMapperService<TIn, TOut> : IMapper<TIn, TOut>
{
    private readonly IMapper _BaseMapper;

    public AutoMapperService(IMapper BaseMapper) => _BaseMapper = BaseMapper;

    public TIn Map(TOut source) => _BaseMapper.Map<TIn>(source);

    public TOut Map(TIn source) => _BaseMapper.Map<TOut>(source);
}
