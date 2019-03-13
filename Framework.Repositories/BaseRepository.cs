using Framework.Context;
using Framework.Models;
using Framework.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Framework.Repositories
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Thêm đối tượng vào bảng
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Đối tượng sau khi được thêm vào</returns>
        T Add(T entity);
        /// <summary>
        /// Thêm 1 danh sách đối tượng vào bảng
        /// </summary>
        /// <param name="list">danh sách đối tượng</param>
        void AddRange(T[] entity);
        /// <summary>
        /// Thêm 1 danh sách đối tượng vào bảng
        /// </summary>
        /// <param name="list">danh sách đối tượng</param>
        void AddRange(List<T> entity);
        /// <summary>
        /// Cập nhật bảng
        /// </summary>
        /// <param name="entity">đối tượng cần cập nhật</param>
        void Update(T entity);
        /// <summary>
        /// Xóa đối tượng
        /// </summary>
        /// <param name="entity">đối tượng cần xóa</param>
        T Delete(T entity);
        /// <summary>
        /// Xóa bằng mã đối tượng
        /// </summary>
        /// <param name="id">Mã đối tượng cần xóa</param>
        T Delete(string id);
        /// <summary>
        /// Xóa theo điều kiện
        /// </summary>
        /// <param name="where">điều kiện cần xóa</param>
        void DeleteMulti(Expression<Func<T, bool>> where);
        /// <summary>
        /// Lấy đối tượng bằng mã
        /// </summary>
        /// <param name="id">Mã đối tượng</param>
        /// <returns>Đối tượng với mã là id</returns>
        T GetSingleById(string id);
        /// <summary>
        /// Đếm số lượng đối tượng dựa vào điều kiện
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <returns>số lượng đối tượng thỏa mãn</returns>
        int Count(Expression<Func<T, bool>> where);
        /// <summary>
        /// Lấy ra danh sách tất cả đối tượng
        /// </summary>
        /// <returns>Danh sách tất cả đối tượng</returns>
        IQueryable<T> GetAll();
        /// <summary>
        /// Lấy 1 đối tượng dựa vào điều kiện
        /// </summary>
        /// <param name="expression">Điều kiện</param>
        /// <returns>1 đối tượng đầu bảng thỏa mãn điều kiện</returns>
        T GetSingleByCondition(Expression<Func<T, bool>> expression);
        /// <summary>
        /// Lọc ra danh sách đối tượng thỏa mãn điều kiện lọc
        /// </summary>
        /// <param name="predicate">điều kiện lọc</param>
        /// <returns>Danh sách đối tượng thỏa mãn điều kiện lọc</returns>
        IQueryable<T> GetMulti(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Kiểm tra sự tồn tại của đối tượng thỏa mãn điều kiện
        /// </summary>
        /// <param name="predicate">điều kiện</param>
        /// <returns>true nếu có đối tượng thỏa mãn, false nếu không</returns>
        bool CheckContains(Expression<Func<T, bool>> predicate);
        DbSet<T> DbSet { get; }
        /// <summary>
        /// Lọc ra danh sách đối tượng thỏa mãn điều kiện lọc và tự động chọn thuộc tính cần lấy
        /// </summary>
        /// <typeparam name="TResult">Đối tượng khai báo thuộc tính cần lấy(DTO)</typeparam>
        /// <param name="predicate">điều kiện lọc</param>
        /// <returns>Lọc ra tất cả đối tượng thỏa mãn điều kiện lọc và chỉ chọn các thuộc tính có trong lớp TResult</returns>
        IQueryable<TResult> GetMultiBySelectClassType<TResult>(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Thêm đối tượng vào bảng bất đồng bộ
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Đối tượng sau khi được thêm vào</returns>
        Task<EntityEntry<T>> AddAsync(T entity);
        /// <summary>
        /// Lấy đối tượng bằng mã bất đồng bộ
        /// </summary>
        /// <param name="id">Mã đối tượng</param>
        /// <returns>Đối tượng với mã là id</returns>
        Task<T> GetSingleByIdAsync(string id);
        /// <summary>
        /// Lấy 1 đối tượng dựa vào điều kiện bất đồng bộ
        /// </summary>
        /// <param name="expression">Điều kiện</param>
        /// <returns>1 đối tượng đầu bảng thỏa mãn điều kiện</returns>
        Task<T> GetSingleByConditionAsync(Expression<Func<T, bool>> expression);
        /// <summary>
        /// Đếm số lượng đối tượng dựa vào điều kiện bất đồng bộ
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <returns>số lượng đối tượng thỏa mãn</returns>
        Task<int> CountAsync(Expression<Func<T, bool>> where);
        /// <summary>
        /// Kiểm tra sự tồn tại của đối tượng thỏa mãn điều kiện bất đồng bộ
        /// </summary>
        /// <param name="predicate">điều kiện</param>
        /// <returns>true nếu có đối tượng thỏa mãn, false nếu không</returns>
        Task<bool> CheckContainsAsync(Expression<Func<T, bool>> predicate);
    }
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected BaseRepository(FrameworkDbContext dataContext)
        {
            this.dataContext = dataContext;
            dbSet = DbContext.Set<T>();
        }


        #region Properties

        protected FrameworkDbContext dataContext;
        protected readonly DbSet<T> dbSet;


        public FrameworkDbContext DbContext { get { return dataContext; } }
        #endregion
        /// <summary>
        /// Phương thức tạo mã đối tượng
        /// Mặc định là các con số ngẫu nhiên
        /// </summary>
        /// <returns>1 chuỗi số ngẫu nhiên</returns>
        protected string GenerateUniqueId()
        {
            //Các con số ngẫu nhiên lấy theo ngày
            return Guid.NewGuid().ToString();
            //var ticks = new DateTime(2016, 1, 1).Ticks;
            //var ans = DateTime.Now.Ticks - ticks;
            //return ans.ToString("x");
        }
        /// <summary>
        /// Lấy tên user đang login trong hệ thống
        /// </summary>
        /// <returns>Tên user đang đăng nhập</returns>
        public string GetLoginedUserName()
        {
            return ((FrameworkDbContext)dataContext).GetLoginedUserName();
        }

        #region Implementation
        /// <summary>
        /// Thêm đối tượng vào bảng
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Đối tượng sau khi được thêm vào</returns>
        public virtual T Add(T entity)
        {
            return dbSet.Add(entity).Entity;
        }
        /// <summary>
        /// Thêm 1 danh sách đối tượng vào bảng
        /// </summary>
        /// <param name="list">danh sách đối tượng</param>
        public void AddRange(T[] list)
        {
            foreach (var entity in list)
            {
                Add(entity);
            }
        }
        /// <summary>
        /// Thêm 1 danh sách đối tượng vào bảng
        /// </summary>
        /// <param name="list">danh sách đối tượng</param>
        public void AddRange(List<T> list)
        {
            foreach (var entity in list)
            {
                Add(entity);
            }
        }
        /// <summary>
        /// Cập nhật bảng
        /// </summary>
        /// <param name="entity">đối tượng cần cập nhật</param>
        public virtual void Update(T entity)
        {
             dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }
        /// <summary>
        /// Xóa đối tượng
        /// </summary>
        /// <param name="entity">đối tượng cần xóa</param>
        public virtual T Delete(T entity)
        {
            return dbSet.Remove(entity).Entity;
        }
        /// <summary>
        /// Xóa bằng mã đối tượng
        /// </summary>
        /// <param name="id">Mã đối tượng cần xóa</param>
        public virtual T Delete(string id)
        {
            return Delete(GetSingleById(id));
        }
        /// <summary>
        /// Xóa theo điều kiện
        /// </summary>
        /// <param name="where">điều kiện cần xóa</param>
        public virtual void DeleteMulti(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
            {
                Delete(obj);
            }
        }
        /// <summary>
        /// Lấy đối tượng bằng mã
        /// </summary>
        /// <param name="id">Mã đối tượng</param>
        /// <returns>Đối tượng với mã là id</returns>
        public virtual T GetSingleById(string id)
        {
            if(String.IsNullOrEmpty(id))
            {
                return null;
            }
            return dbSet.Find(id);
        }
        /// <summary>
        /// Đếm số lượng đối tượng dựa vào điều kiện
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <returns>số lượng đối tượng thỏa mãn</returns>
        public virtual int Count(Expression<Func<T, bool>> where)
        {
            return dbSet.Count(where);
        }
        /// <summary>
        /// Lấy ra danh sách tất cả đối tượng
        /// </summary>
        /// <returns>Danh sách tất cả đối tượng</returns>
        public IQueryable<T> GetAll()
        {
            return dataContext.Set<T>();
        }
        /// <summary>
        /// Lấy 1 đối tượng dựa vào điều kiện
        /// </summary>
        /// <param name="expression">Điều kiện</param>
        /// <returns>1 đối tượng đầu bảng thỏa mãn điều kiện</returns>
        public T GetSingleByCondition(Expression<Func<T, bool>> expression)
        {
            return dataContext.Set<T>().FirstOrDefault(expression);
        }
        /// <summary>
        /// Lọc ra danh sách đối tượng thỏa mãn điều kiện lọc
        /// </summary>
        /// <param name="predicate">điều kiện lọc</param>
        /// <returns>Danh sách đối tượng thỏa mãn điều kiện lọc</returns>
        public virtual IQueryable<T> GetMulti(Expression<Func<T, bool>> predicate)
        {
            return dataContext.Set<T>().Where(predicate);
        }
        /// <summary>
        /// Kiểm tra sự tồn tại của đối tượng thỏa mãn điều kiện
        /// </summary>
        /// <param name="predicate">điều kiện</param>
        /// <returns>true nếu có đối tượng thỏa mãn, false nếu không</returns>
        public bool CheckContains(Expression<Func<T, bool>> predicate)
        {
            return dataContext.Set<T>().Count<T>(predicate) > 0;
        }

        public DbSet<T> DbSet
        {
            get
            {
                return dbSet;
            }
        }
        /// <summary>
        /// Lọc ra danh sách đối tượng thỏa mãn điều kiện lọc và tự động chọn thuộc tính cần lấy
        /// </summary>
        /// <typeparam name="TResult">Đối tượng khai báo thuộc tính cần lấy(DTO)</typeparam>
        /// <param name="predicate">điều kiện lọc</param>
        /// <returns>Lọc ra tất cả đối tượng thỏa mãn điều kiện lọc và chỉ chọn các thuộc tính có trong lớp TResult</returns>
        public IQueryable<TResult> GetMultiBySelectClassType<TResult>(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).Select(ExpressionHelper.SelectExpression<T, TResult>()).AsQueryable();
        }
        /// <summary>
        /// Thêm đối tượng vào bảng bất đồng bộ
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Đối tượng sau khi được thêm vào</returns>
        public Task<EntityEntry<T>> AddAsync(T entity)
        {
            return DbSet.AddAsync(entity);
        }
        /// <summary>
        /// Lấy đối tượng bằng mã bất đồng bộ
        /// </summary>
        /// <param name="id">Mã đối tượng</param>
        /// <returns>Đối tượng với mã là id</returns>
        public Task<T> GetSingleByIdAsync(string id)
        {
            return dbSet.FindAsync(id);
        }
        /// <summary>
        /// Lấy 1 đối tượng dựa vào điều kiện bất đồng bộ
        /// </summary>
        /// <param name="expression">Điều kiện</param>
        /// <returns>1 đối tượng đầu bảng thỏa mãn điều kiện</returns>
        public Task<T> GetSingleByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return dataContext.Set<T>().FirstOrDefaultAsync(expression);
        }
        /// <summary>
        /// Đếm số lượng đối tượng dựa vào điều kiện bất đồng bộ
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <returns>số lượng đối tượng thỏa mãn</returns>
        public Task<int> CountAsync(Expression<Func<T, bool>> where)
        {
            return dataContext.Set<T>().Where(where).CountAsync();
        }
        /// <summary>
        /// Kiểm tra sự tồn tại của đối tượng thỏa mãn điều kiện bất đồng bộ
        /// </summary>
        /// <param name="predicate">điều kiện</param>
        /// <returns>true nếu có đối tượng thỏa mãn, false nếu không</returns>
        public async Task<bool> CheckContainsAsync(Expression<Func<T, bool>> predicate)
        {
            int value = await CountAsync(predicate);
            return value > 0;
        }

         #endregion
    }
}
