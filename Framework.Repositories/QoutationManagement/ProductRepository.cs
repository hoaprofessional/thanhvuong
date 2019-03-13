using Framework.Context;
using Framework.Models.QoutationManagement;
using System;
namespace Framework.Repositories.QoutationManagement
{
    public interface IProductRepository : IRepository<Product>
    {
        
    }
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override Product Add(Product entity)
        {
            //entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(Product entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}