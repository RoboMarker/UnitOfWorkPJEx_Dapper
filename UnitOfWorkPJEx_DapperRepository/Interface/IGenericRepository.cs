﻿namespace UnitOfWorkPJEx_DapperRepository.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity? GetById(int id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
