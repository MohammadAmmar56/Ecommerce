using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { }
        public DbSet<adminlogin> adminlogin { get; set; }
        public DbSet<signup> signup { get; set; }
        public DbSet<slider> slider { get; set; }
        public DbSet<collection> collection { get; set; }
        public DbSet<topsales> topsales { get; set; }
        public DbSet<newarrival> newarrival { get; set; }
        public DbSet<shoescollection> shoescollection { get; set; }
        public DbSet<menscollection> menscollection { get; set; }  
        public DbSet<category> category { get; set; }
        public DbSet<product> product { get; set; }
        public DbSet<order> order { get; set; }
    }
}
