using System;

using OpenTK;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model block norms.
	/// </summary>
	public class CIwModelBlockNorms : CIwModelBlock
	{
		#region Constants and Fields

		private Vector3[] verts;

		#endregion

		// CIwSVec3*       m_Norms;    //!< vertex normal list
		#region Public Properties

		/// <summary>
		/// Gets or sets Normal.
		/// </summary>
		/// <exception cref="NotImplementedException">
		/// </exception>
		public Vector3[] Normal
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
			serialise.Int16(ref mediane);

			this.Resize(this.numItems);

			for (int i = 0; i < this.numItems; ++i)
			{
				short x = (short)(this.verts[i].X - mediane);
				serialise.Int16(ref x);
				this.verts[i].X = x + mediane;
			}

			for (int i = 0; i < this.numItems; ++i)
			{
				short y = (short)(this.verts[i].Y - mediane);
				serialise.Int16(ref y);
				this.verts[i].Y = y + mediane;
			}

			for (int i = 0; i < this.numItems; ++i)
			{
				short z = (short)(this.verts[i].Z - mediane);
				serialise.Int16(ref z);
				this.verts[i].Z = z + mediane;
			}
		}

		#endregion

		#region Methods

		private void Resize(ushort length)
		{
			if (this.verts == null)
			{
				this.verts = new Vector3[length];
			}
			else
			{
				Array.Resize(ref this.verts, length);
			}
		}

		#endregion
	}
}