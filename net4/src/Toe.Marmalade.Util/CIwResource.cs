using System;

namespace Toe.Marmalade.Util
{
	/// <summary>
	/// The c iw resource.
	/// </summary>
	public class CIwResource : CIwManaged, IDisposable
	{
		#region Public Methods and Operators

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="serialise">
		/// The serialise.
		/// </param>
		public override void Serialise(IwSerialise serialise)
		{
			base.Serialise(serialise);
		}

		#endregion
	}
}