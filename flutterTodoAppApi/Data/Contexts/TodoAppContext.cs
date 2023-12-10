using flutterTodoAppApi.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace flutterTodoAppApi.Data.Contexts
{
    public class TodoAppContext(DbContextOptions<TodoAppContext> options) : DbContext(options)
    {
        public DbSet<UserEO> Users { get; set; }
        public DbSet<UserSettingEO> UsersSettings { get; set; }
        public DbSet<ThemeEO> Themes { get; set; }
        public DbSet<TodoEO> Todos { get; set; }
        public DbSet<CheckListEO> CheckLists { get; set; }
        public DbSet<CheckListItemEO> CheckListsItems { get; set; }
        public DbSet<ConnectionEO> Connections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSettingEO>()
                .HasOne(setting => setting.User)
                .WithOne(user => user.Setting)
                .HasForeignKey<UserSettingEO>(setting => setting.UserId);

            modelBuilder.Entity<UserSettingEO>()
                .HasOne(setting => setting.Theme)
                .WithOne(theme => theme.Setting)
                .HasForeignKey<UserSettingEO>(setting => setting.ThemeId);

            modelBuilder.Entity<TodoEO>()
                .HasOne(todo => todo.User)
                .WithMany(user => user.Todos)
                .HasForeignKey(todo => todo.UserId);

            modelBuilder.Entity<CheckListEO>()
                .HasOne(checkList => checkList.User)
                .WithMany(user => user.CheckLists)
                .HasForeignKey(checkList => checkList.UserId);

            modelBuilder.Entity<CheckListItemEO>()
                .HasOne(checkListItem => checkListItem.CheckList)
                .WithMany(checkList => checkList.CheckListItems)
                .HasForeignKey(checkListItem => checkListItem.CheckListId);

            modelBuilder.Entity<ConnectionEO>()
                .HasOne(connection => connection.User)
                .WithMany(user => user.Connections)
                .HasForeignKey(connection => connection.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
