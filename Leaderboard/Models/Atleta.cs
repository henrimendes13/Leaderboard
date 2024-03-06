using Leaderboard.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Schema;

namespace Leaderboard.Models
{
	public class Atleta
	{
		[Key] public int Id { get; set; }
		public string Nome { get; set; }

		public Categoria Categoria { get; set; }

		public int Workout1 { get; set; }

		public int Workout2 { get; set; }

		public int Workout3 { get; set; }



		public Atleta() { }

		public Atleta(string nome, Categoria categoria, int workout1, int workout2, int workout3)
		{
			Nome = nome;
			Categoria = categoria;
			Workout1 = workout1;
			Workout2 = workout2;
			Workout3 = workout3;
		}


	}
}
