#define SQL
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class Context : DbContext
{
    string connString = $"Server=185.60.40.210\\SQLEXPRESS,58015;Database=F1BDMaider;User Id=sa;Password=Pa88word;MultipleActiveResultSets=true";
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(connString);

    // public CryptoContext(DbContextOptions<CryptoContext> options) : base(options) { }
    // public CryptoContext() : base() { }
    public Context()
    {}

    public DbSet<Misil> misil { get; set; }

    public DbSet<Frente> frente { get; set; }

    public DbSet<Contrato> contrato { get; set; }

}