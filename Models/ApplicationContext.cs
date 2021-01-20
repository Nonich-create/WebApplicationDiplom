using Microsoft.EntityFrameworkCore;


namespace WebApplicationDiplom.Models
{
    public class ApplicationContext : DbContext 
    {
        public DbSet<EducationalInstitutions> EducationalInstitutions { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<TableAddress> PersTableAddresson { get; set; }
        public DbSet<TableArea> TableArea { get; set; }
        public DbSet<TableEducational> TableEducational { get; set; }
        public DbSet<TableHistoryOfAppointments> TableHistoryOfAppointments { get; set; }
        public DbSet<Tablelocality> Tablelocality { get; set; }
        public DbSet<TableOrganizations> TableOrganizations { get; set; }
        public DbSet<TablePosition> TablePosition { get; set; }
        public DbSet<TableQualification> TableQualification { get; set; }
        public DbSet<TableSpecialty> TableSpecialty { get; set; }
        public DbSet<TableVerificationOfEducation> TableVerificationOfEducation { get; set; }
        public DbSet<Worker> Worker { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
         : base(options)
        {
               //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
