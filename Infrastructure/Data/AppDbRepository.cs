using Application.Data;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Entities.Common;

namespace Infrastructure.Data;

public class AppDbRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
	public AppDbRepository(AppDbContext appDbContext) : base(appDbContext)
	{
	}
}
