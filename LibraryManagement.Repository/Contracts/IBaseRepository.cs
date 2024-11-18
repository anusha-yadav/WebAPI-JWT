using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(object id);

        void Insert(T obj);

        void Update(T obj);

        void Delete(object id);

        void SaveChanges();
    }
}
