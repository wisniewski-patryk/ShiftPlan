using ShiftPlan.Api.Models;

namespace ShiftPlan.Api.Repository;

public interface IRepository<T> where T : BaseEntity
{
	Task<T?> Get(int id);
	IQueryable<T> GetAll();
	Task<T> InsertOrUpdate(T entity);
	Task Delete(T entity);
}
