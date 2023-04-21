using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entidades
{
	[Table("url_encurtadas")]
	public class UrlEncurtada
	{
		[Key]
		[Required]
		[Column("id")]
		public int Id { get; set; }

		[Required]
		[StringLength(2000)]
		[Column("url")]
		public string? Url { get; set; }

		[Required]
		[Column("data_criacao")]
		public DateTime DataCriacao { get; set; }
	}
}
