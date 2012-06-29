using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model block.
	/// </summary>
	public class CIwModelBlock : CIwManaged
	{
		#region Constants and Fields

		/// <summary>
		/// The num items.
		/// </summary>
		protected ushort numItems;

		private ushort flags;

		private ushort size;

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The render.
		/// </summary>
		/// <param name="model">
		/// The model.
		/// </param>
		/// <param name="flags">
		/// The flags.
		/// </param>
		/// <returns>
		/// The render.
		/// </returns>
		public virtual uint Render(CIwModel model, uint flags)
		{
			return 0;
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
			{
				serialise.UInt16(ref this.size);
			}
			{
				serialise.UInt16(ref this.numItems);
			}
			{
				serialise.UInt16(ref this.flags);
			}
		}

		#endregion
	}
}