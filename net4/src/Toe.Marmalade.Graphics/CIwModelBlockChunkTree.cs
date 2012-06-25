using System;

namespace Toe.Marmalade.Graphics
{
	public class CIwModelBlockChunkTree : CIwModelBlock
	{
		public override void Serialise(Util.IwSerialise serialise)
		{
			base.Serialise(serialise);
			throw new NotImplementedException();
		}
		////uint16 m_NumNodes;     //!< number of nodes in tree
		////uint16 m_NumChunks;    //!< number of chunks (leaf nodes)
		////Node* m_Nodes;        //!< all nodes in tree

	}
}