using System;

using OpenTK;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model block glu vs.
	/// </summary>
	public class CIwModelBlockGLUVs : CIwModelBlock
	{
		#region Constants and Fields

		private Vector2[] verts;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets UVs.
		/// </summary>
		/// <exception cref="NotImplementedException">
		/// </exception>
		public Vector2[] UVs
		{
			get
			{
				return this.verts;
			}

			set
			{
				throw new NotImplementedException();
			}
		}

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

			// serialise.Int16(ref mediane);
			this.Resize(this.numItems);

			for (int i = 0; i < this.numItems; ++i)
			{
				short x = (short)((this.verts[i].X - mediane) * S3E.IwGeomOne);
				serialise.Int16(ref x);
				this.verts[i].X = (x + mediane) / (float)S3E.IwGeomOne;

				short y = (short)((this.verts[i].Y - mediane) * S3E.IwGeomOne);
				serialise.Int16(ref y);
				this.verts[i].Y = (y + mediane) / (float)S3E.IwGeomOne;
			}
		}

		#endregion

		#region Methods

		private void Resize(ushort length)
		{
			if (this.verts == null)
			{
				this.verts = new Vector2[length];
			}
			else
			{
				Array.Resize(ref this.verts, length);
			}
		}

		#endregion
	}
}