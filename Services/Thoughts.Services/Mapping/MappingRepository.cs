using Thoughts.Domain;
using Thoughts.Interfaces.Base;
using Thoughts.Interfaces.Base.Entities;
using Thoughts.Interfaces.Base.Repositories;

namespace Thoughts.Services.Mapping;

public class MappingRepository<TSource, TDestination> : IRepository<TDestination>
    where TDestination : class, IEntity<int> 
    where TSource : class, IEntity<int>
{
    private readonly IRepository<TSource> _SourceRepository;
    private readonly IMapper<TSource, TDestination> _Mapper;

    public MappingRepository(
        IRepository<TSource> SourceRepository, 
        IMapper<TSource, TDestination> Mapper)
    {
        _SourceRepository = SourceRepository;
        _Mapper = Mapper;
    }

    public async Task<bool> ExistId(int Id, CancellationToken Cancel = default) => await _SourceRepository.ExistId(Id, Cancel);

    public async Task<bool> Exist(TDestination item, CancellationToken Cancel = default)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        var dest_item = _Mapper.Map(item);
        return await _SourceRepository.Exist(dest_item, Cancel);
    }

    public async Task<int> GetCount(CancellationToken Cancel = default) => await _SourceRepository.GetCount(Cancel);

    public async Task<IEnumerable<TDestination>> GetAll(CancellationToken Cancel = default)
    {
        var source_items = await _SourceRepository.GetAll(Cancel);

        var result_items = source_items.Select(i => _Mapper.Map(i));
        return result_items;
    }

    public async Task<IEnumerable<TDestination>> Get(int Skip, int Count, CancellationToken Cancel = default)
    {
        var source_items = await _SourceRepository.Get(Skip, Count, Cancel);

        var result_items = source_items.Select(i => _Mapper.Map(i));
        return result_items;
    }

    public async Task<IPage<TDestination>> GetPage(int PageNumber, int PageSize, CancellationToken Cancel = default)
    {
        if (PageNumber < 0) throw new ArgumentOutOfRangeException(nameof(PageNumber), PageNumber, "Номер страницы должен быть больше, либо равен 0");
        if (PageSize <= 0) throw new ArgumentOutOfRangeException(nameof(PageSize), PageSize, "Размер страницы должен быть больше 0");

        var source_page = await _SourceRepository.GetPage(PageNumber, PageSize, Cancel);

        var dest_items = source_page.Items.Select(i => _Mapper.Map(i));

        var result_page = new Page<TDestination>(dest_items, PageNumber, PageSize, source_page.TotalCount);

        return result_page;
    }

    public async Task<TDestination> GetById(int Id, CancellationToken Cancel = default)
    {
        var source_item = await _SourceRepository.GetById(Id, Cancel).ConfigureAwait(false);
        if (source_item is null) return null;

        var result_items = _Mapper.Map(source_item);
        return result_items;
    }

    public async Task<TDestination> Add(TDestination item, CancellationToken Cancel = default)
    {
        var destination_item = _Mapper.Map(item);

        var result = await _SourceRepository.Add(destination_item, Cancel).ConfigureAwait(false);

        var result_item = _Mapper.Map(result);
        return result_item;
    }

    public async Task AddRange(IEnumerable<TDestination> items, CancellationToken Cancel = default)
    {
        if (items is null) throw new ArgumentNullException(nameof(items));

        var sourse_items = items.Select(i => _Mapper.Map(i));

        await _SourceRepository.AddRange(sourse_items, Cancel).ConfigureAwait(false);
    }

    public async Task<TDestination> Update(TDestination item, CancellationToken Cancel = default)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        var source_item = _Mapper.Map(item);
        await _SourceRepository.Update(source_item, Cancel).ConfigureAwait(false);
        return item;
    }

    public async Task UpdateRange(IEnumerable<TDestination> items, CancellationToken Cancel = default)
    {
        if (items is null) throw new ArgumentNullException(nameof(items));

        var sourse_items = items.Select(i => _Mapper.Map(i));

        await _SourceRepository.UpdateRange(sourse_items, Cancel);
    }

    public async Task<TDestination> Delete(TDestination item, CancellationToken Cancel = default)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));

        var source_item = _Mapper.Map(item);

        await _SourceRepository.Delete(source_item, Cancel);

        return item;
    }

    public async Task DeleteRange(IEnumerable<TDestination> items, CancellationToken Cancel = default)
    {
        if (items is null) throw new ArgumentNullException(nameof(items));

        var sourse_items = items.Select(i => _Mapper.Map(i));

        await _SourceRepository.DeleteRange(sourse_items, Cancel);
    }

    public async Task<TDestination> DeleteById(int id, CancellationToken Cancel = default)
    {
        var result_item = await _SourceRepository.DeleteById(id, Cancel).ConfigureAwait(false);
        var result = _Mapper.Map(result_item);
        return result;
    }
}
