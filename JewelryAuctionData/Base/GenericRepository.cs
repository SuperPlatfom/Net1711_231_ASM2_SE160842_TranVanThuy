using JewelryAuctionData;
using JewelryAuctionData.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonReservationData.Base
{
    public class GenericRepository<T> where T : BaseEntity
    {
        private const string ErrorMessage = "Haven't any transaction";
        internal readonly DbSet<T> _dbSet;
        private readonly UnitOfWork _unitOfWork;

        public GenericRepository(UnitOfWork unitOfWork)
        {
            _dbSet = unitOfWork.Context.Set<T>();
            this._unitOfWork = unitOfWork;
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public void Create(T entity)
        {
            _dbSet.Add(entity);
            this._unitOfWork.Context.SaveChanges();
        }

        public async Task CreateAsync(T entity)
        {
            if (!this._unitOfWork.IsTransaction)
            {
                throw new InvalidOperationException(ErrorMessage);
            }

            await this._dbSet.AddAsync(entity).ConfigureAwait(false);
        }

        public async Task CreateRangeAsync(IEnumerable<T> entities)
        {
            if (!this._unitOfWork.IsTransaction)
            {
                throw new InvalidOperationException(ErrorMessage);
            }

            await this._dbSet.AddRangeAsync(entities).ConfigureAwait(false);
        }

        public T Update(T entity)
        {
            if (!this._unitOfWork.IsTransaction)
            {
                throw new InvalidOperationException(ErrorMessage);
            }

            this._dbSet.Attach(entity);
            return entity;
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            if (!this._unitOfWork.IsTransaction)
            {
                throw new InvalidOperationException(ErrorMessage);
            }

            this._dbSet.AttachRange(entities);
            return entities;
        }

        public bool Remove(T entity)
        {
            if (!this._unitOfWork.IsTransaction)
            {
                throw new InvalidOperationException(ErrorMessage);
            }

            this._dbSet.Remove(entity);
            return true;
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            if (!this._unitOfWork.IsTransaction)
            {
                throw new InvalidOperationException(ErrorMessage);
            }

            this._dbSet.RemoveRange(entities);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public T GetById(string code)
        {
            return _dbSet.Find(code);
        }

        public async Task<T> GetByIdAsync(string code)
        {
            return await _dbSet.FindAsync(code);
        }

        #region Separating asign entity and save operators

        public void PrepareCreate(T entity)
        {
            _dbSet.Add(entity);
        }

        public void PrepareUpdate(T entity)
        {
            var tracker = _dbSet.Attach(entity);
            tracker.State = EntityState.Modified;
        }

        public void PrepareRemove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public int Save()
        {
            return this._unitOfWork.Context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await this._unitOfWork.Context.SaveChangesAsync();
        }

        #endregion Separating asign entity and save operators
    }
}
