﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CmsEntity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CmsEntities : DbContext
    {
        public CmsEntities()
            : base("name=CmsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TB_Authority> TB_Authority { get; set; }
        public virtual DbSet<TB_BasicContent> TB_BasicContent { get; set; }
        public virtual DbSet<TB_BasicUser> TB_BasicUser { get; set; }
        public virtual DbSet<TB_ContentType> TB_ContentType { get; set; }
        public virtual DbSet<TB_Dictionary> TB_Dictionary { get; set; }
        public virtual DbSet<TB_DicType> TB_DicType { get; set; }
        public virtual DbSet<TB_Organization> TB_Organization { get; set; }
        public virtual DbSet<TB_Position> TB_Position { get; set; }
        public virtual DbSet<TB_PositionCity> TB_PositionCity { get; set; }
        public virtual DbSet<TB_PositionCounty> TB_PositionCounty { get; set; }
        public virtual DbSet<TB_PositionProvice> TB_PositionProvice { get; set; }
        public virtual DbSet<TB_PositionTown> TB_PositionTown { get; set; }
        public virtual DbSet<TB_PositionVillage> TB_PositionVillage { get; set; }
        public virtual DbSet<TB_Role> TB_Role { get; set; }
        public virtual DbSet<TB_RoleAuthority> TB_RoleAuthority { get; set; }
        public virtual DbSet<TB_SysParams> TB_SysParams { get; set; }
        public virtual DbSet<TB_UserOrganization> TB_UserOrganization { get; set; }
        public virtual DbSet<TB_UserRole> TB_UserRole { get; set; }
    }
}
