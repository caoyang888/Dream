using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Dream.Model
{
    public class DbBase : DbContext
    {
        public DbBase()
            : base("Dream")
        {
            //Database.SetInitializer<DbBase>(null);
        }

        public DbSet<TB_Admin> TB_Admin { get; set; }
        public DbSet<TB_Category> TB_Category { get; set; }
        public DbSet<TB_ErrorCode> TB_ErrorCode { get; set; }
        public DbSet<TB_Lecturer> TB_Lecturer { get; set; }
        public DbSet<TB_SignInLog> TB_SignInLog { get; set; }
        public DbSet<TB_SystemConfig> TB_SystemConfig { get; set; }
        public DbSet<TB_SystemLog> TB_SystemLog { get; set; }
        public DbSet<TB_User> TB_User { get; set; }
        public DbSet<TB_UserHistory> TB_UserHistory { get; set; }
        public DbSet<TB_Video> TB_Video { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
