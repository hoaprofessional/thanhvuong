using Framework.Models;
using Framework.Repositories;
using Framework.Repositories.Infrastructor;
using Framework.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services.ManageService
{
    /// <summary>
    /// Xử lý nghiệp vụ dạng quản lý
    /// </summary>
    /// <typeparam name="T">Kiểu của model cần quản lý</typeparam>
    public interface IManageServiceBase<T>
         where T : class, IAuditable
    {
        /// <summary>
        /// Thêm 1 model mới
        /// </summary>
        /// <param name="entity">model cần thêm</param>
        /// <returns>Model sau khi được thêm có sẵn id mới</returns>
        T Add(T entity);

        /// <summary>
        /// Thêm danh sách model
        /// </summary>
        /// <param name="collection">Danh sách model cần thêm</param>
        void AddRange(T[] collection);

        /// <summary>
        /// Thêm danh sách model
        /// </summary>
        /// <param name="collection">Danh sách model cần thêm</param>
        void AddRange(List<T> entity);

        /// <summary>
        /// Cập nhật model
        /// </summary>
        /// <param name="entity">model cần cập nhật</param>
        void Update(T entity);

        /// <summary>
        /// Xóa model
        /// </summary>
        /// <param name="entity">Model cần xóa</param>
        void Delete(T entity);
        /// <summary>
        /// Lay item boi id
        /// </summary>
        /// <param name="id">id cua item</param>
        /// <returns></returns>
        T GetById(string id);
    }
    public class ManageServiceBase<T> : IManageServiceBase<T>
        where T : class, IAuditable
    {
        protected readonly IRepository<T> repository;

        public ManageServiceBase(IRepository<T> repository)
        {
            this.repository = repository;
        }

        public virtual T Add(T entity)
        {
            return repository.Add(entity);
        }
        public virtual void Update(T entity)
        {
            repository.Update(entity);
        }
        public virtual void Delete(T entity)
        {
            entity.Active = false;
            repository.Update(entity);
        }

        public void AddRange(T[] collection)
        {
            repository.AddRange(collection);
        }

        public void AddRange(List<T> collection)
        {
            repository.AddRange(collection);
        }

        public T GetById(string id)
        {
            return repository.GetSingleById(id);
        }
    }
}
