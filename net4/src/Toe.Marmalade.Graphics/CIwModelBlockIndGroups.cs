using System;
using System.Diagnostics;
using System.Text;

namespace Toe.Marmalade.Graphics
{
	public class CIwModelBlockIndGroups: CIwModelBlock
	{
		sbyte VertIDOfs;
		sbyte NormIDOfs;
		sbyte ColIDOfs;
		sbyte UV0IDOfs;
		sbyte TupleSize;

		public ushort[] Tuples;

		public ushort GetVert(ushort index)
		{
			return Tuples[index * TupleSize + VertIDOfs];
		}
		public bool HasColors
		{
			get
			{
				return ColIDOfs >= 0;
			}
		}
		public ushort GetColor(ushort index)
		{
			return Tuples[index * TupleSize + ColIDOfs];
		}

		public override void Serialise(Util.IwSerialise serialise)
		{
			base.Serialise(serialise);
			serialise.Serialise(ref this.VertIDOfs);
			serialise.Serialise(ref this.NormIDOfs);
			serialise.Serialise(ref this.ColIDOfs);
			serialise.Serialise(ref this.UV0IDOfs);
			TupleSize = 0;
			if (TupleSize < VertIDOfs) TupleSize = VertIDOfs;
			if (TupleSize < NormIDOfs) TupleSize = NormIDOfs;
			if (TupleSize < ColIDOfs) TupleSize = ColIDOfs;
			if (TupleSize < UV0IDOfs) TupleSize = UV0IDOfs;
			if (serialise.IsReading())
			{
				++TupleSize;
				Tuples = new ushort[TupleSize * this.numItems];
			}
			serialise.Serialise(ref Tuples);

			////var buf = new byte[Math.Min(256, serialise.Length-serialise.Position)];
			////serialise.Serialise(ref buf);
			////StringBuilder sb = new StringBuilder();
			////int i = 0;
			////foreach (var b in buf)
			////{
			////    if (b < 16)
			////        sb.Append("0");
			////    sb.Append(string.Format("{0:x} ", b));
			////    ++i;
			////    if (i == 8)
			////        sb.Append("| ");
			////    if (i == 16)
			////    {
			////        sb.Append("\n");
			////        i = 0;
			////    }
			////}
			////Debug.WriteLine(sb.ToString());
			////throw new NotImplementedException();
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