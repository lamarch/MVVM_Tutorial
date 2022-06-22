
namespace MVVM_Tutorial.Contexts
{
    using Microsoft.EntityFrameworkCore;

    using MVVM_Tutorial.DTOs;
    using MVVM_Tutorial.Models;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class ReservoomDbContext : DbContext
    {
        public ReservoomDbContext(DbContextOptions options) : base(options)
        {
        }

#nullable disable
        public DbSet<ReservationDTO> Reservations { get; set; }
#nullable restore
    }
}
