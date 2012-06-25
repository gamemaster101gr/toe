using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model block.
	/// </summary>
	public class CIwModelBlock: CIwManaged
	{
		private ushort size;

		protected ushort numItems;

		private ushort flags;

		public override void Serialise(IwSerialise serialise)
		{

			base.Serialise(serialise);
			{
				serialise.UInt16(ref size);
			}
			{
				serialise.UInt16(ref numItems);
			}
			{
				serialise.UInt16(ref flags);
			}

		}

		public virtual uint Render(CIwModel model, uint flags)
		{
			return 0;
		}
	}
}