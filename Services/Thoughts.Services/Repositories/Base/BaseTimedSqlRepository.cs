using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Thoughts.DAL.Sqlite;
using Thoughts.Interfaces.Base.Entities;
using Thoughts.Interfaces.Base.Repositories;

namespace Thoughts.Services.Repositories.Base;

public abstract class BaseTimedSqlRepository<T> : BaseSqlRepository<T>, ITimedRepository<T> where T: class, ITimedEntity
{
    private readonly ContextSqlite _dbContextSql;
    private readonly ILogger<BaseTimedSqlRepository<T>> _logger;
    private readonly DbSet<T> _table;

    public BaseTimedSqlRepository(ContextSqlite dbContextSql, ILogger<BaseTimedSqlRepository<T>> logger) : base(dbContextSql, logger)
    {
        _dbContextSql = dbContextSql;
        _logger = logger;
        _table = dbContextSql.Set<T>();
    }

    #region Async

    public async Task<bool> ExistGreaterThenTimeAsync(DateTimeOffset ReferenceTime, CancellationToken Cancel = default) => 
        await _table.AnyAsync(entity => entity.Time > ReferenceTime,Cancel).ConfigureAwait(false);

    public async Task<int> GetCountGreaterThenTimeAsync(DateTimeOffset ReferenceTime, CancellationToken Cancel = default) => 
        await _table.Where(entity => entity.Time > ReferenceTime).CountAsync(Cancel).ConfigureAwait(false);

    public async Task<IEnumerable<T>> GetAllGreaterThenTimeAsync(DateTimeOffset ReferenceTime, CancellationToken Cancel = default) => 
        await _table.Where(entity => entity.Time > ReferenceTime).ToArrayAsync(Cancel).ConfigureAwait(false);

    public async Task<IEnumerable<T>> GetGreaterThenTimeAsync(DateTimeOffset ReferenceTime, int Skip, int Count, CancellationToken Cancel = default) => 
        await _table.Where(entity => entity.Time > ReferenceTime).Skip(Skip).Take(Count).ToArrayAsync(Cancel).ConfigureAwait(false);

    public async Task<bool> ExistLessThenTimeAsync(DateTimeOffset ReferenceTime, CancellationToken Cancel = default) => 
        await _table.AnyAsync(entity => entity.Time < ReferenceTime,Cancel).ConfigureAwait(false);

    public async Task<int> GetCountLessThenTimeAsync(DateTimeOffset ReferenceTime, CancellationToken Cancel = default) => 
        await _table.Where(entity => entity.Time < ReferenceTime).CountAsync(Cancel).ConfigureAwait(false);

    public async Task<IEnumerable<T>> GetAllLessThenTimeAsync(DateTimeOffset ReferenceTime, CancellationToken Cancel = default) => 
        await _table.Where(entity => entity.Time < ReferenceTime).ToArrayAsync(Cancel).ConfigureAwait(false);

    public async Task<IEnumerable<T>> GetLessThenTimeAsync(DateTimeOffset ReferenceTime, int Skip, int Count, CancellationToken Cancel = default) => 
        await _table.Where(entity => entity.Time < ReferenceTime).Skip(Skip).Take(Count).ToArrayAsync(Cancel).ConfigureAwait(false);

    public async Task<IEnumerable<T>> GetAllInTimeIntervalAsync(DateTimeOffset StartTime, DateTimeOffset EndTime, CancellationToken Cancel = default) => 
        await _table.Where(entity => entity.Time >= StartTime && entity.Time <= EndTime).ToArrayAsync(Cancel).ConfigureAwait(false);

    #endregion

    #region Sync

    public bool ExistGreaterThenTime(DateTimeOffset ReferenceTime) => ExistGreaterThenTimeAsync(ReferenceTime).Result;

    public int GetCountGreaterThenTime(DateTimeOffset ReferenceTime) =>
        GetCountGreaterThenTimeAsync(ReferenceTime).Result;

    public IEnumerable<T> GetAllGreaterThenTime(DateTimeOffset ReferenceTime) =>
        GetAllGreaterThenTimeAsync(ReferenceTime).Result;

    public IEnumerable<T> GetGreaterThenTime(DateTimeOffset ReferenceTime, int Skip, int Count) =>
        GetGreaterThenTimeAsync(ReferenceTime, Skip, Count).Result;

    public bool ExistLessThenTime(DateTimeOffset ReferenceTime) => ExistLessThenTimeAsync(ReferenceTime).Result;

    public int GetCountLessThenTime(DateTimeOffset ReferenceTime) => GetCountLessThenTimeAsync(ReferenceTime).Result;

    public IEnumerable<T> GetAllLessThenTime(DateTimeOffset ReferenceTime) => GetAllLessThenTimeAsync(ReferenceTime).Result;

    public IEnumerable<T> GetLessThenTime(DateTimeOffset ReferenceTime, int Skip, int Count) => GetLessThenTimeAsync(ReferenceTime, Skip, Count).Result;

    public IEnumerable<T> GetAllInTimeInterval(DateTimeOffset StartTime, DateTimeOffset EndTime) => GetAllInTimeIntervalAsync(StartTime, EndTime).Result;

    #endregion

    public Task<IPage<T>> GetInTimeIntervalAsync(DateTimeOffset StartTime, DateTimeOffset EndTime, int PageIndex, int PageSize,
        CancellationToken Cancel = default) =>
        throw new NotImplementedException();
    
    public Task<IPage<T>> GetPageGreaterThenTimeAsync(DateTimeOffset ReferenceTime, int PageIndex, int PageSize,
        CancellationToken Cancel = default) =>
        throw new NotImplementedException();
    
    public Task<IPage<T>> GetPageLessThenTimeAsync(DateTimeOffset ReferenceTime, int PageIndex, int PageSize, CancellationToken Cancel = default) => throw new NotImplementedException();
}