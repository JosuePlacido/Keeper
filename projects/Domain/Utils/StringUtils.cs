using System.Globalization;
using System.Text;

namespace Keeper.Domain.Utils
{
	public static class StringUtils
	{
		public static string NormalizeLower(string text)
		{
			if (string.IsNullOrEmpty(text))
				return "";
			StringBuilder sbReturn = new StringBuilder();
			var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
			foreach (char letter in arrayText)
			{
				if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
					sbReturn.Append(letter);
			}
			return sbReturn.ToString().ToLower();
		}
	}

}
