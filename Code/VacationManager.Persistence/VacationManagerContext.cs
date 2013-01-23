using System.Data.Entity;
using VacationManager.Persistence.Configurations;
using VacationManager.Persistence.Model;

namespace VacationManager.Persistence
{
    public class VacationManagerContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<VacationRequest> Requests { get; set; }
        public DbSet<VacationStatus> VacationStatus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new VacationRequestConfiguration());
            modelBuilder.Configurations.Add(new VacationStatusConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}