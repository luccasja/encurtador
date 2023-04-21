using Api.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Api.Contratos
{
	public interface IContexto
	{
		public DbSet<UrlEncurtada>? UrlEncurtadas { get; set; }

		void Salvar();
		void AtualizarEntity<T>(T entidade, T novaEntidade);
	}
}
