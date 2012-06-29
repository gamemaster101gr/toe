using System;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model block chunk.
	/// </summary>
	public class CIwModelBlockChunk : CIwModelBlock
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

		////uint16 m_NodeID;           //!< ID of this chunk within Tree's m_Nodes array
		////uint16 m_ChunkID;          //!< there are less chunks than nodes
		////CIwManagedList m_Blocks;           //!< list of smaller prim blocks
	}
}