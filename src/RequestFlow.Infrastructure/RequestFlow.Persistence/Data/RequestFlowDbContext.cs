using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RequestFlow.Domain.Entities;
using RequestFlow.Persistence.Data;

namespace RequestFlow.Persistence;

public class RequestFlowDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public RequestFlowDbContext(DbContextOptions<RequestFlowDbContext> options)
        : base(options)
    {
    }

    public DbSet<Request> Requests => Set<Request>();
    public DbSet<RequestType> RequestTypes => Set<RequestType>();
    public DbSet<RequestStatusHistory> RequestStatusHistories => Set<RequestStatusHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RequestFlowDbContext).Assembly);
    }
}
