using System;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model block verts 2 d.
	/// </summary>
	public class CIwModelBlockVerts2D : CIwModelBlock
	{
		#region Public Methods and Operators

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="serialise">
		/// The serialise.
		/// </param>
		/// <exception cref="NotImplementedException">
		/// </exception>
		public override void Serialise(IwSerialise serialise)
		{
			base.Serialise(serialise);
			throw new NotImplementedException();
		}

		#endregion

		// CIwSVec2*       m_Verts;        //!< vertex list
	}
}