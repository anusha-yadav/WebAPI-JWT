using LibraryManagement.Data;
using LibraryManagement.Data.Entities;
using LibraryManagement.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Services
{
    public class BaseRepository<T> : IBaseRepository<T> where T:class
    {
        protected readonly EmployeeDBContext EmployeeDBContext;

        public BaseRepository(EmployeeDBContext context)
        {
            EmployeeDBContext = context;
        }

        public IQueryable<T> GetAll()
        {
            return EmployeeDBContext.Set<T>();
        }

        public T GetById(object id)
        {
            return EmployeeDBContext.Set<T>().Find(id);
        }


        public void Insert(T entity)
        {
            EmployeeDBContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            EmployeeDBContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            var entityToDelete = EmployeeDBContext.Set<T>().Find(id);
            if (entityToDelete != null)
            {
                EmployeeDBContext.Set<T>().Remove(entityToDelete);
            }
        }

        public void SaveChanges()
        {
            EmployeeDBContext.SaveChanges();
        }
    }
}
