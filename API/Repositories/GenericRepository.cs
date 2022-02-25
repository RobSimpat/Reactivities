namespace API.Repositories
{
    public class GenericRepository<T> : Interfaces.IGenericRepository<T> where T : class
    {
        private readonly Db.Context.ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(Db.Context.ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public IQueryable<T> GetEntity()
        {
            return _dbSet;
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<EntityEntry<T>> AddAsync(T entity)
        {
            return await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(List<T> entityList)
        {
            await _dbSet.AddRangeAsync(entityList);
        }

        public async Task<EntityEntry<T>> DeleteByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                return _dbSet.Remove(entity);
            }

            return null;
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<int> SaveAllAsync()
        {
            var resultNumber = await _dbContext.SaveChangesAsync();
            return resultNumber;
        }
    }
}
