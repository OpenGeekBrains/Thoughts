using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Thoughts.DAL.Sqlite;
using Thoughts.Interfaces.Base.Entities;
using Thoughts.Interfaces.Base.Repositories;

namespace Thoughts.Services.Repositories.Base;

public abstract class BaseNamedSqlRepository<T> : BaseSqlRepository<T>, INamedRepository<T> where T: class, INamedEntity
{
    private readonly ContextSqlite _dbContextSql;
    private readonly ILogger<BaseNamedSqlRepository<T>> _logger;
    private readonly DbSet<T> _table;

    protected BaseNamedSqlRepository(ContextSqlite dbContextSql, ILogger<BaseNamedSqlRepository<T>> logger) : base(dbContextSql, logger)
    {
        _dbContextSql = dbContextSql;
        _logger = logger;
        _table = dbContextSql.Set<T>();
    }

    #region Async

    public async Task<bool> ExistNameAsync(string Name, CancellationToken Cancel = default) =>
        await _table.AnyAsync(entity => entity.Name == Name, Cancel).ConfigureAwait(false);

    public async Task<T> GetByNameAsync(string Name, CancellationToken Cancel = default) => 
        await _table.FindAsync(Name,Cancel).ConfigureAwait(false) 
        ?? throw new InvalidOperationException("Объект с указанным именем не был найден");

    public virtual async Task<T> DeleteByNameAsync(string Name, CancellationToken Cancel = default)
    {
        var entity = await GetByNameAsync(Name, Cancel).ConfigureAwait(false);
        _dbContextSql.Entry(entity).State = EntityState.Deleted;
        await SaveChangesAsync(Cancel);
        return entity;
    }

    #endregion

    #region Sync

    public bool ExistName(string Name) => ExistNameAsync(Name).Result;


    public T GetByName(string Name) => GetByNameAsync(Name).Result;


    public T DeleteByName(string Name) => DeleteByNameAsync(Name).Result;

    #endregion

}