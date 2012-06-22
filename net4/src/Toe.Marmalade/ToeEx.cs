namespace Toe.Marmalade
{
	/// <summary>
	/// Extension methods.
	/// </summary>
	public static class ToeEx
	{
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
			return S3E.HashString(s);
		}

		#endregion
	}
}