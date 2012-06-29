using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model block ind groups.
	/// </summary>
	public class CIwModelBlockIndGroups : CIwModelBlock
	{
		#region Constants and Fields

		/// <summary>
		/// The tuples.
		/// </summary>
		public ushort[] Tuples;

		private sbyte ColIDOfs;

		private sbyte NormIDOfs;

		private sbyte TupleSize;

		private sbyte UV0IDOfs;

		private sbyte VertIDOfs;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets a value indicating whether HasColors.
		/// </summary>
		public bool HasColors
		{
			get
			{
				return this.ColIDOfs >= 0;
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The get color.
		/// </summary>
		/// <param name="index">
		/// The index.
		/// </param>
		/// <returns>
		/// The get color.
		/// </returns>
		public ushort GetColor(ushort index)
		{
			return this.Tuples[index * this.TupleSize + this.ColIDOfs];
		}

		/// <summary>
		/// The get vert.
		/// </summary>
		/// <param name="index">
		/// The index.
		/// </param>
		/// <returns>
		/// The get vert.
		/// </returns>
		public ushort GetVert(ushort index)
		{
			return this.Tuples[index * this.TupleSize + this.VertIDOfs];
		}

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="serialise">
		/// The serialise.
		/// </param>
		public override void Serialise(IwSerialise serialise)
		{
			base.Serialise(serialise);
			serialise.Serialise(ref this.VertIDOfs);
			serialise.Serialise(ref this.NormIDOfs);
			serialise.Serialise(ref this.ColIDOfs);
			serialise.Serialise(ref this.UV0IDOfs);
			this.TupleSize = 0;
			if (this.TupleSize < this.VertIDOfs)
			{
				this.TupleSize = this.VertIDOfs;
			}

			if (this.TupleSize < this.NormIDOfs)
			{
				this.TupleSize = this.NormIDOfs;
			}

			if (this.TupleSize < this.ColIDOfs)
			{
				this.TupleSize = this.ColIDOfs;
			}

			if (this.TupleSize < this.UV0IDOfs)
			{
				this.TupleSize = this.UV0IDOfs;
			}

			if (serialise.IsReading())
			{
				++this.TupleSize;
				this.Tuples = new ushort[this.TupleSize * this.numItems];
			}

			serialise.Serialise(ref this.Tuples);

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

		#endregion

		////uint16*     m_Tuples;       // Ptr to index groups (vertID or not present, normID or not present, colID or not present)
		////int8        m_VertIDOfs;    // Position of vertID within above tuples, or -1 if not present
		////int8        m_NormIDOfs;    // Position of normID within above tuples, or -1 if not present
		////int8        m_ColIDOfs;     // Position of colID within above tuples, or -1 if not present
		////int8        m_UV0IDOfs;     // Position of uvID within above tuples, or -1 if not present
		////uint8       m_TupleSize;    // 0, 1 or 2 (number of uint16 entries in tuple)
		////uint8       m_pad[3];
	}
}