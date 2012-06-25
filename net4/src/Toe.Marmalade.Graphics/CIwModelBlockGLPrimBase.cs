namespace Toe.Marmalade.Graphics
{
	public class CIwModelBlockGLPrimBase : CIwModelBlock
	{
		private uint materialId;

		public override void Serialise(Util.IwSerialise serialise)
		{
			base.Serialise(serialise);

			serialise.UInt32(ref materialId);

			inds = new ushort[numItems];
			serialise.Serialise(ref inds);

			{
				serialise.UInt16(ref streamIdBegin);
			}
			{
				serialise.UInt16(ref streamIdEnd);
			}
		}

		private ushort[] inds;

		private ushort streamIdBegin;

		private ushort streamIdEnd;
	}
}