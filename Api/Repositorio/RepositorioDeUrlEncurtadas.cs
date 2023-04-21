using Api.Contratos;
using Api.Entidades;
using System.Web;

namespace Api.Repositorio
{
	public class RepositorioDeUrlEncurtadas
	{
		private readonly IContexto _contexto;

		public RepositorioDeUrlEncurtadas(IContexto contexto)
		{
			_contexto = contexto;
		}

		public int? Adicionar(string urlCompleta)
		{
			var urlExistente = Buscar(urlCompleta);
			if (urlExistente != null)
				return urlExistente.Id;

			var novaUrl = new UrlEncurtada
			{
				Url = HttpUtility.UrlDecode(urlCompleta),
			};

			_contexto.UrlEncurtadas?.Add(novaUrl);
			_contexto.Salvar();

			return _contexto.UrlEncurtadas?
				.OrderBy(url => url.Id)
				.LastOrDefault()?.Id;
		}

		public UrlEncurtada? Buscar(string? urlCompleta)
		{
			var urlDecodificada = HttpUtility.UrlDecode(urlCompleta);
			var urlExistente = _contexto.UrlEncurtadas?
				.FirstOrDefault(url => !string.IsNullOrEmpty(url.Url) && url.Url.Equals(urlDecodificada));

			return urlExistente;
		}

		public UrlEncurtada? Buscar(int? id)
		{
			return _contexto.UrlEncurtadas?
				.FirstOrDefault(url => url.Id.Equals(id));
		}

		public IEnumerable<UrlEncurtada>? BuscarTodos(int pagina = 1)
		{
			return _contexto.UrlEncurtadas?
				.OrderByDescending(url => url.Id)
				.Skip(pagina)
				.Take(50)
				.ToList();
		}

		public IEnumerable<UrlEncurtada>? BuscarTodosAposId(int id)
		{
			return _contexto.UrlEncurtadas?
				.OrderByDescending(url => url.Id)
				.Where(url => url.Id > id)
				.Take(50)
				.ToList();
		}

		public void Atualizar(int id, UrlEncurtada urlEncurtada)
		{
			var urlExistente = Buscar(id);
			if (urlExistente != null)
			{
				_contexto.AtualizarEntity(urlExistente, urlEncurtada);
				_contexto.Salvar();
			}
		}

		public void Excluir(int id)
		{
			var urlExistente = Buscar(id);
			if (urlExistente != null)
			{
				_contexto.UrlEncurtadas?.Remove(urlExistente);
				_contexto.Salvar();
			}
		}
	}
}
