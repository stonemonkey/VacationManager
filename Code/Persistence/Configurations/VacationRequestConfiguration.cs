using System.Data.Entity.ModelConfiguration;
using Persistence.Model;

namespace Persistence.Configurations
{
    public class VacationRequestConfiguration : 
        EntityTypeConfiguration<VacationRequestEntity>
    {
        public VacationRequestConfiguration()
        {
            HasRequired(x => x.Employee);
        }
    }
}