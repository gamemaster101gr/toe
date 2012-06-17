namespace Toe.Core
{
	/// <summary>
	/// Extension methods.
	/// </summary>
	public static class ToeEx
	{
		#region Constants and Fields

		/// <summary>
		/// Initial hash value.
		/// </summary>
		private static uint hashInitialValue = 5381;

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Case insensetive hash.
		/// </summary>
		/// <param name="s">
		/// String to calculate hash for.
		/// </param>
		/// <returns>
		/// Calculated hash.
		/// </returns>
		public static uint ToeHash(this string s)
		{
			uint res = hashInitialValue;
			foreach (var c in s)
			{
				var caseInsensitiveChar = (c < 'A' || c > 'Z') ? c : (c - 'A' + 'a');
				res = ((res << 5) + res) + (byte)caseInsensitiveChar;
			}

			return res;
		}

		#endregion
	}
}