using System;

namespace Toe.Marmalade.Graphics
{
	public class CIwModelBlockIndGroups: CIwModelBlock
	{
		public override void Serialise(Util.IwSerialise serialise)
		{
			base.Serialise(serialise);
			throw new NotImplementedException();
		}
		////uint16*     m_Tuples;       // Ptr to index groups (vertID or not present, normID or not present, colID or not present)
		////int8        m_VertIDOfs;    // Position of vertID within above tuples, or -1 if not present
		////int8        m_NormIDOfs;    // Position of normID within above tuples, or -1 if not present
		////int8        m_ColIDOfs;     // Position of colID within above tuples, or -1 if not present
		////int8        m_UV0IDOfs;     // Position of uvID within above tuples, or -1 if not present
		////uint8       m_TupleSize;    // 0, 1 or 2 (number of uint16 entries in tuple)
		////uint8       m_pad[3];
		
	}
}