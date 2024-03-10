using System.ComponentModel.DataAnnotations;

namespace Leaderboard.Models
{
    public class Workout
    {
        [Key] 
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
