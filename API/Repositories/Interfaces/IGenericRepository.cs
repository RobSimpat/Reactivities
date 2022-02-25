﻿namespace API.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        Task<EntityEntry<T>> AddAsync(T entity);

        Task<EntityEntry<T>> DeleteByIdAsync(int id);

        void Update(T entity);

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);

        Task<int> SaveAllAsync();

        public IQueryable<T> GetEntity();

        Task AddRangeAsync(List<T> entityList);
    }

}
