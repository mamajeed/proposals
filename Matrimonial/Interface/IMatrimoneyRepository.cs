using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Matrimonial.Interface
{
   public interface IMatrimoneyRepository<T>: IDisposable
    {

        IEnumerable<T> GetAll();
        T Get(long id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        


        //IQueryable<T> GetProducts(Int32 pageSize, Int32 pageNumber, String name);

        //Task<T> GetProductAsync(T entity);

        //Task<T> AddProductAsync(T entity);

        //Task<T> UpdateProductAsync(T changes);

        //Task<T> DeleteProductAsync(T changes);
    }
}
