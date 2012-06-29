using System;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model block chunk tree.
	/// </summary>
	public class CIwModelBlockChunkTree : CIwModelBlock
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

		////uint16 m_NumNodes;     //!< number of nodes in tree
		////uint16 m_NumChunks;    //!< number of chunks (leaf nodes)
		////Node* m_Nodes;        //!< all nodes in tree
	}
}