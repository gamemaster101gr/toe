using System;

using OpenTK;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model block tangents.
	/// </summary>
	public class CIwModelBlockTangents : CIwModelBlock
	{
		#region Constants and Fields

		private Vector3[] tangents;

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="serialise">
		/// The serialise.
		/// </param>
		public override void Serialise(IwSerialise serialise)
		{
			base.Serialise(serialise);
			short mediane = 0;
			serialise.Int16(ref mediane);

			this.Resize(this.numItems);

			for (int i = 0; i < this.numItems; ++i)
			{
				short x = (short)(this.tangents[i].X - mediane);
				serialise.Int16(ref x);
				this.tangents[i].X = x + mediane;
			}

			for (int i = 0; i < this.numItems; ++i)
			{
				short y = (short)(this.tangents[i].Y - mediane);
				serialise.Int16(ref y);
				this.tangents[i].Y = y + mediane;
			}

			for (int i = 0; i < this.numItems; ++i)
			{
				short z = (short)(this.tangents[i].Z - mediane);
				serialise.Int16(ref z);
				this.tangents[i].Z = z + mediane;
			}
		}

		#endregion

		#region Methods

		private void Resize(ushort length)
		{
			if (this.tangents == null)
			{
				this.tangents = new Vector3[length];
			}
			else
			{
				Array.Resize(ref this.tangents, length);
			}
		}

		#endregion
	}
}