using Api.Contratos;
using Api.Core;
using Api.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
	[ApiController]
	[Route("")]
	public class EncurtadorController : ControllerBase
	{
		private IContexto _contexto;
		private RepositorioDeUrlEncurtadas _repositorioDeUrlEncurtadas;

		public EncurtadorController(DbContextOptions dbContext)
		{
			_contexto = new Contexto(dbContext);
			_repositorioDeUrlEncurtadas = new RepositorioDeUrlEncurtadas(_contexto);
		}

		[HttpGet("{parteDaUrl}")]
		public RedirectResult Redirecionar(string parteDaUrl)
		{
			var idUrl = EncurtadorDeUrl.ObterId(parteDaUrl);
			var urlEncurtada = _repositorioDeUrlEncurtadas.Buscar(idUrl);
			return Redirect(urlEncurtada?.Url ?? string.Empty);
		}

		[HttpPost("{url}")]
		public IActionResult Encurtar(string url)
		{
			var idUrl = _repositorioDeUrlEncurtadas.Adicionar(url);
			var parteDaUrl = EncurtadorDeUrl.ObterParteDaUrl(idUrl ?? 0);
			var uriEncurtada = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{parteDaUrl}";
			return Ok(uriEncurtada);
		}

	}
}
