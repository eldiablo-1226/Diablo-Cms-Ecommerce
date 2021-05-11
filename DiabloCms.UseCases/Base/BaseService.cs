using System.Linq;
using AutoMapper;
using DiabloCms.MsSql;
using Microsoft.EntityFrameworkCore;

namespace DiabloCms.UseCases.Base
{
    public abstract class BaseService<TEntity> where TEntity : class
    {
        protected readonly CmsDbContext Data;
        protected readonly IMapper Mapper;

        protected BaseService(CmsDbContext dbContext, IMapper mapper)
        {
            Data = dbContext;
            Mapper = mapper;
        }

        protected IQueryable<TEntity> All => Data.Set<TEntity>();

        protected IQueryable<TEntity> AllAsNoTracking => Data.Set<TEntity>().AsNoTracking();
    }
}