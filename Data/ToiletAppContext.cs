using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToiletApp.Models;

namespace ToiletApp.Data
{
    public class ToiletAppContext : DbContext
    {
        public ToiletAppContext (DbContextOptions<ToiletAppContext> options)
            : base(options)
        {
        }

        public DbSet<ToiletApp.Models.Areas> Areas { get; set; } = default!;
        public DbSet<ToiletApp.Models.Toilets> Toilets { get; set; } = default!;
        public DbSet<ToiletApp.Models.Comments> Comments { get; set; } = default!;
    }
}
