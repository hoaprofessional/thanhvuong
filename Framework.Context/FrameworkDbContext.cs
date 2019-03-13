using Framework.Context.Configurations;
using Framework.Models.UserManagement;
using Framework.Models.QoutationManagement;
using Framework.Models.TaskManagement;
using Framework.Models.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Security.Claims;
using Framework.Models.Configuration;
using Framework.Models.NotificationManagement;
using Framework.Models;

namespace Framework.Context
{
    public class FrameworkDbContext : IdentityDbContext<ApplicationUser>
    {
        private IHttpContextAccessor contextAccessor;
        public FrameworkDbContext(DbContextOptions<FrameworkDbContext> options, IHttpContextAccessor contextAccessor)
            : base(options)
        {
            this.contextAccessor = contextAccessor;
          //  this..LazyLoadingEnabled = false;
        }

        public string GetLoginedUserName()
        {
            return contextAccessor.HttpContext.User?.Identity?.Name;
        }

        #region SaleData

        public DbSet<Qoutation> Qoutations { get; set; }
        public DbSet<QoutationDetail> QoutationDetails { get; set; }
        public DbSet<QoutationStatus> QoutationStatuss { get; set; }
        public DbSet<QoutationEvent> QoutationEvent { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatus> OrderStatuss { get; set; }
        public DbSet<OrderEvent> OrderEvents { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<QuotesProcessedInterested> QuotesProcessedInteresteds { get; set; }
        public DbSet<QuotesStatusWaitingApprovalInterested> QuotesStatusWaitingApprovalInteresteds { get; set; }
        public DbSet<QuotesStatusWaitingProcessInterested> QuotesStatusWaitingProcessInteresteds { get; set; }
        public DbSet<OrderProcessedInterested> OrderProcessedInteresteds { get; set; }
        public DbSet<OrderStatusWaitingApprovalInterested> OrderStatusWaitingApprovalInteresteds { get; set; }
        public DbSet<OrderStatusWaitingProcessInterested> OrderStatusWaitingProcessInteresteds { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Logger> Loggers { get; set; }
        public DbSet<CacheData> CacheDatas { get; set; }
        #endregion

        #region UserManager
        public DbSet<Permission> Permissions { get; set; }
        #endregion

        #region TaskManagement
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskStatus> TaskStatuss { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<WorkStatus> WorkStatuss { get; set; }
        public DbSet<Priority> Priority { get; set; }
        public DbSet<AssignWorkUser> AssignWorkUsers { get; set; }

        public DbSet<QoutationSendNotificationConfig> QoutationSendNotificationConfigs { get; set; }

        #endregion

        #region Notification

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationPermissionConfiguration> NotificationPermissionConfigurations { get; set; }

        #endregion

        #region Common Configuration
        public DbSet<CommonConfiguration> CommonConfigurations { get; set; }
        public DbSet<IdConfiguration> IdConfigurations { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new QoutationDetailConfiguration());
            builder.ApplyConfiguration(new OrderDetailConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new TaskConfiguration());
            builder.ApplyConfiguration(new TaskStatusConfiguration());
            builder.ApplyConfiguration(new WorkConfiguration());
            builder.ApplyConfiguration(new WorkStatusConfiguration());
            builder.ApplyConfiguration(new PriorityConfiguration());

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
