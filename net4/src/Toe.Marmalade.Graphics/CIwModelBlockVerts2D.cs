using System;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	public class CIwModelBlockVerts2D : CIwModelBlock
	{
		public override void Serialise(Util.IwSerialise serialise)
		{
			base.Serialise(serialise);
			throw new NotImplementedException();
		}

		// CIwSVec2*       m_Verts;        //!< vertex list
	}
}