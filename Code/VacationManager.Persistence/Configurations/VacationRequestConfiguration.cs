using System.Data.Entity.ModelConfiguration;
using VacationManager.Persistence.Model;

namespace VacationManager.Persistence.Configurations
{
    public class VacationRequestConfiguration : EntityTypeConfiguration<VacationRequest>
    {
        public VacationRequestConfiguration()
        {
            HasRequired(x => x.Employee);
        }
    }
}