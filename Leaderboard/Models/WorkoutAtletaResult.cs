using System.ComponentModel.DataAnnotations;

namespace Leaderboard.Models
{
    public class WorkoutAtletaResult
    {
        [Key]
        public int Id { get; set; }
        public int AtletaId { get; set; }
        public int WorkoutId { get; set; }
        public string Resultado { get; set; }
    }
}
