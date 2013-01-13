using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using VacationManager.Services.Model;

namespace VacationManager.Services
{
    public class VacationManagerContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<VacationRequest> Requests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new VacationRequestConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }

    public class VacationRequestConfiguration : EntityTypeConfiguration<VacationRequest>
    {
        public VacationRequestConfiguration()
        {
            HasRequired(b => b.Employee);
        }
    }
}