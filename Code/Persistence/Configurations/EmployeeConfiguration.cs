using System.Data.Entity.ModelConfiguration;
using Persistence.Model;

namespace Persistence.Configurations
{
    public class EmployeeConfiguration : 
        EntityTypeConfiguration<EmployeeEntity>
    {
        public EmployeeConfiguration()
        {
            HasRequired(x => x.Situation)
                .WithRequiredPrincipal();
        }
    }
}