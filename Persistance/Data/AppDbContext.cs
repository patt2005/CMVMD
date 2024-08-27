using Microsoft.EntityFrameworkCore;
using Persistance.Entities;

namespace Persistance.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
    {
    }

    public DbSet<Article> Articles { get; set; }
    public DbSet<LegislationDocument> LegislationDocuments { get; set; }
    public DbSet<ExecutiveOfficeMember> ExecutiveOfficeMembers { get; set; }
    public DbSet<DentistryComisionMember> DentistryComisionMembers { get; set; }
    public DbSet<ComisionComponentMember> ComisionComponentMembers { get; set; }
    public DbSet<TrainingDocument> TrainingDocuments { get; set; }
    public DbSet<Entities.File> Files { get; set; }
    public DbSet<Veterinarian> Veterinarians { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<User> Users { get; set; }
}
