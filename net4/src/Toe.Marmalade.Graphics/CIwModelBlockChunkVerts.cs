using System;

namespace Toe.Marmalade.Graphics
{
	public class CIwModelBlockChunkVerts : CIwModelBlock
	{
		public override void Serialise(Util.IwSerialise serialise)
		{
			base.Serialise(serialise);
			throw new NotImplementedException();
		}
		////uint16* m_VertIDs;      //!< IDs into standard vertex list
	}
}