using System.Data.Entity.ModelConfiguration;
using VacationManager.Persistence.Model;

namespace VacationManager.Persistence.Configurations
{
    public class VacationStatusConfiguration : EntityTypeConfiguration<VacationStatus>
    {
        public VacationStatusConfiguration()
        {
            HasRequired(x => x.Employee);
        }
    }
}