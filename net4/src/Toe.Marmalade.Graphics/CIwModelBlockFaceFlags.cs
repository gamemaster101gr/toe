using System;

namespace Toe.Marmalade.Graphics
{
	public class CIwModelBlockFaceFlags : CIwModelBlock
	{
		public override void Serialise(Util.IwSerialise serialise)
		{
			base.Serialise(serialise);
			throw new NotImplementedException();
		}
		////uint8* m_FaceFlags; //!< per face flag list
		////bool m_WorldSet; //!< True if block contains world file variations to sets
	}
}