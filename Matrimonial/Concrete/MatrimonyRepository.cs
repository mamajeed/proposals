using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matrimonial.Interface;
using Matrimonial.context;
using Microsoft.EntityFrameworkCore;
using Matrimonial.Model;


namespace Matrimonial.Concrete
{
    public class MatrimonyRepository<T>:IMatrimoneyRepository<T> where T: BaseEntity
    {
        private readonly DatabaseContext DbContext;
        private DbSet<T> entities;
        private Boolean Disposed;

        public MatrimonyRepository(DatabaseContext dbContext)
        {
           this.DbContext = dbContext;
           this.entities = DbContext.Set<T>();

        }

        public void Dispose()
        {
            if (!Disposed)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();

                    Disposed = true;
                }
            }
        }


       
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }
        public T Get(long id)
        {
            return entities.SingleOrDefault(s => s.Id== id);
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            DbContext.SaveChanges();
        }
        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbContext.SaveChanges();
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            DbContext.SaveChanges();
        }

      
        //public IQueryable<T> GetProducts(Int32 pageSize, Int32 pageNumber, String name)
        //{
        //    var query = DbContext.Set<T>().Skip((pageNumber - 1) * pageSize).Take(pageSize);

        //    if (!String.IsNullOrEmpty(name))
        //    {
        //        query = query.Where(item => item.Name.ToLower().Contains(name.ToLower()));
        //    }

        //    return query;
        //}

        //public Task<T> GetProductAsync(T entity)
        //{
        //    return DbContext.Set<T>().FirstOrDefaultAsync(item => item.ProductID == entity.ProductID);
        //}

        //public async Task<T> AddProductAsync(T entity)
        //{
        //    entity.MakeFlag = false;
        //    entity.FinishedGoodsFlag = false;
        //    entity.SafetyStockLevel = 1;
        //    entity.ReorderPoint = 1;
        //    entity.StandardCost = 0.0m;
        //    entity.ListPrice = 0.0m;
        //    entity.DaysToManufacture = 0;
        //    entity.SellStartDate = DateTime.Now;
        //    entity.rowguid = Guid.NewGuid();
        //    entity.ModifiedDate = DateTime.Now;

        //    DbContext.Set<T>().Add(entity);

        //    await DbContext.SaveChangesAsync();

        //    return entity;
        //}

        //public async Task<T> UpdateProductAsync(T changes)
        //{
        //    var entity = await GetProductAsync(changes);

        //    if (entity != null)
        //    {
        //        entity.Name = changes.Name;
        //        entity.ProductNumber = changes.ProductNumber;

        //        await DbContext.SaveChangesAsync();
        //    }

        //    return entity;
        //}

        //public async Task<T> DeleteProductAsync(T changes)
        //{
        //    var entity = await GetProductAsync(changes);

        //    if (entity != null)
        //    {
        //        DbContext.Set<T>().Remove(entity);

        //        await DbContext.SaveChangesAsync();
        //    }

        //    return entity;
        //}

    }
}
