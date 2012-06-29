using System;

using OpenTK;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model block bi tangents.
	/// </summary>
	public class CIwModelBlockBiTangents : CIwModelBlock
	{
		#region Constants and Fields

		private Vector3[] biTangents;

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
				short x = (short)(this.biTangents[i].X - mediane);
				serialise.Int16(ref x);
				this.biTangents[i].X = x + mediane;
			}

			for (int i = 0; i < this.numItems; ++i)
			{
				short y = (short)(this.biTangents[i].Y - mediane);
				serialise.Int16(ref y);
				this.biTangents[i].Y = y + mediane;
			}

			for (int i = 0; i < this.numItems; ++i)
			{
				short z = (short)(this.biTangents[i].Z - mediane);
				serialise.Int16(ref z);
				this.biTangents[i].Z = z + mediane;
			}
		}

		#endregion

		#region Methods

		private void Resize(ushort length)
		{
			if (this.biTangents == null)
			{
				this.biTangents = new Vector3[length];
			}
			else
			{
				Array.Resize(ref this.biTangents, length);
			}
		}

		#endregion
	}
}