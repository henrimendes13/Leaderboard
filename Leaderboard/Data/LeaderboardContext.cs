using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Leaderboard.Models;

namespace Leaderboard.Data
{
    public class LeaderboardContext : DbContext
    {
        public LeaderboardContext (DbContextOptions<LeaderboardContext> options)
            : base(options)
        {
        }

        public DbSet<Atleta> Atleta { get; set; }
    }
}
