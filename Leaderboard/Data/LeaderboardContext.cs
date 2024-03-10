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
        public DbSet<Workout> Workout { get; set; }
        public DbSet<WorkoutAtletaResult> WorkoutAtletaResult { get; set; }
    }
}
