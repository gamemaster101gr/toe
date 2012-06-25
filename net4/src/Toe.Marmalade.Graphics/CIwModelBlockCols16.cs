using System;

namespace Toe.Marmalade.Graphics
{
	public class CIwModelBlockCols16 : CIwModelBlock
	{
		public override void Serialise(Util.IwSerialise serialise)
		{
			base.Serialise(serialise);
			throw new NotImplementedException();
		}
		// uint16*      m_Cols;     //!< colour list
	}
}