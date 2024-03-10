namespace Leaderboard.Models.DTO
{
    public class AtletaWorkoutDTO
    {
        public string AtletaName { get; set; }
        public List<string> AtletaResults { get; set;}=new List<string>();

        public int Pontos { get; set; }


    }
}
