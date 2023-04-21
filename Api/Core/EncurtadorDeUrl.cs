using Microsoft.AspNetCore.WebUtilities;

namespace Api.Core
{
	public static class EncurtadorDeUrl
	{
		public static string ObterParteDaUrl(int id)
		{
			return WebEncoders.Base64UrlEncode(BitConverter.GetBytes(id));
		}

		public static int ObterId(string parteDaUrl)
		{
			return BitConverter.ToInt32(WebEncoders.Base64UrlDecode(parteDaUrl));
		}
	}
}