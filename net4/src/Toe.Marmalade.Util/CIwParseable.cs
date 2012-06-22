namespace Toe.Marmalade.Util
{
	/// <summary>
	/// The c iw parseable.
	/// </summary>
	public class CIwParseable
	{
		#region Public Methods and Operators

		/// <summary>
		/// The parse attribute.
		/// </summary>
		/// <param name="pParser">
		/// The p parser.
		/// </param>
		/// <param name="pAttrName">
		/// The p attr name.
		/// </param>
		/// <returns>
		/// The parse attribute.
		/// </returns>
		public virtual bool ParseAttribute(CIwTextParserITX pParser, string pAttrName)
		{
			return false;
		}

		/// <summary>
		/// The parse close.
		/// </summary>
		/// <param name="pParser">
		/// The p parser.
		/// </param>
		public virtual void ParseClose(CIwTextParserITX pParser)
		{
		}

		/// <summary>
		/// The parse open.
		/// </summary>
		/// <param name="pParser">
		/// The p parser.
		/// </param>
		public virtual void ParseOpen(CIwTextParserITX pParser)
		{
		}

		#endregion
	}
}