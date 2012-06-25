using System;

namespace Toe.Marmalade.Graphics
{
	public class CIwModelBlockChunk : CIwModelBlock
	{
		public override void Serialise(Util.IwSerialise serialise)
		{
			base.Serialise(serialise);
			throw new NotImplementedException();
		}
		////uint16 m_NodeID;           //!< ID of this chunk within Tree's m_Nodes array
		////uint16 m_ChunkID;          //!< there are less chunks than nodes
		////CIwManagedList m_Blocks;           //!< list of smaller prim blocks
	}
}