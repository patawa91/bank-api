using System.Linq.Expressions;

namespace Bank.Domain.Repositories;

/// <summary>
/// Standard repository interface for CRUD operations.
/// </summary>
/// <typeparam name="T">The domain object.</typeparam>
public interface IRepository<T, S> where T : class
{
    /// <summary>
    /// Get all instances.
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Get an instance by id.
    /// </summary>
    /// <param name="id">Id of entity.</param>
    Task<T> GetByIdAsync(int id);

    /// <summary>
    /// Add an instance.
    /// </summary>
    /// <param name="entity">Entity to add.</param>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Update an instance.
    /// </summary>
    /// <param name="entity">Instance to update.</param>
    Task<T> UpdateAsync(T entity);

    /// <summary>
    /// Remove an instance by id.
    /// </summary>
    /// <param name="id">Id to remove.</param>
    Task<T> RemoveAsync(int id);

    /// <summary>
    /// Find instances by criteria.
    /// </summary>
    /// <param name="searchCriteria">Criteria to search for.</param>
    Task<IEnumerable<T>> Find(S searchCriteria);
}
