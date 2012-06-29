namespace Toe.Marmalade.Util
{
	/// <summary>
	/// The i resource resolver.
	/// </summary>
	public interface IResourceResolver
	{
		#region Public Methods and Operators

		/// <summary>
		/// The resolve.
		/// </summary>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <param name="hash">
		/// The hash.
		/// </param>
		/// <returns>
		/// </returns>
		CIwManaged Resolve(uint type, uint hash);

		/// <summary>
		/// The resolve file path.
		/// </summary>
		/// <param name="filePath">
		/// The file path.
		/// </param>
		/// <param name="ram">
		/// The ram.
		/// </param>
		/// <returns>
		/// The resolve file path.
		/// </returns>
		string ResolveFilePath(string filePath, bool ram);

		#endregion
	}
}