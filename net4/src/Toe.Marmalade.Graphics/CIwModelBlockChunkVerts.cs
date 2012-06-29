using System;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model block chunk verts.
	/// </summary>
	public class CIwModelBlockChunkVerts : CIwModelBlock
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

		////uint16* m_VertIDs;      //!< IDs into standard vertex list
	}
}