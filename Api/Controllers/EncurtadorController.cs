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

		[HttpGet]
		public ContentResult Index()
		{
			var conteudoHtml = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "View/index.html"));

			return new ContentResult
			{
				Content = conteudoHtml,
				ContentType = "text/html",
				StatusCode = 200,
			};
		}

		[HttpGet("{parteDaUrl}")]
		public RedirectResult Redirecionar(string parteDaUrl)
		{
			try
			{
				var idUrl = EncurtadorDeUrl.ObterId(parteDaUrl);
				var urlEncurtada = _repositorioDeUrlEncurtadas.Buscar(idUrl);

				if (urlEncurtada != null)
					return Redirect(urlEncurtada.Url ?? string.Empty);
				else
					return Redirect("Index");
			}
			catch (Exception)
			{
				return Redirect("Index");
			}
		}

		[HttpPost("{url}")]
		public IActionResult EncurtarViaPath(string url)
		{
			return Encurtar(url);
		}

		[HttpPost]
		public IActionResult EncurtarViaFormulario([FromForm] string url)
		{
			return Encurtar(url);
		}

		private ActionResult Encurtar(string url)
		{
			var idUrl = _repositorioDeUrlEncurtadas.Adicionar(url);
			var parteDaUrl = EncurtadorDeUrl.ObterParteDaUrl(idUrl ?? 0);
			var uriEncurtada = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{parteDaUrl}";
			return Ok(uriEncurtada);
		}
	}
}
