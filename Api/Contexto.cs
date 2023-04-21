using Api.Contratos;
using Api.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Api
{
	public class Contexto : DbContext, IContexto
	{
		public DbSet<UrlEncurtada>? UrlEncurtadas { get; set; }

		public Contexto(DbContextOptions options) : base(options)
		{
			ChangeTracker.LazyLoadingEnabled = true;
		}

		public void Salvar() => SaveChanges();

		public void AtualizarEntity<T>(T entidade, T novaEntidade)
		{
			if ((entidade != null) && (novaEntidade != null))
				Entry(entidade).CurrentValues.SetValues(novaEntidade);
		}
	}
}
