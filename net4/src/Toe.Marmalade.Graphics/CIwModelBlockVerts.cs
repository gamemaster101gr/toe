using System;

using OpenTK;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model block verts.
	/// </summary>
	public class CIwModelBlockVerts : CIwModelBlock
	{
		#region Constants and Fields

		/// <summary>
		/// The verts.
		/// </summary>
		private Vector3[] verts;

		private ushort uniqueValues;

		/// <summary>
		/// The verts.
		/// </summary>
		public Vector3[] Verts
		{
			get
			{
				return this.verts;
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
		/// <exception cref="NotImplementedException">
		/// </exception>
		public override void Serialise(IwSerialise serialise)
		{
			base.Serialise(serialise);
			{
				serialise.UInt16(ref this.uniqueValues);
			}

			short mediane = 0;
			serialise.Int16(ref mediane);

			var len = this.uniqueValues; // this.numItems; //

			this.Resize(this.numItems);

			if (serialise.IsWriting())
			{
				throw new NotImplementedException();
			}

			for (int i = 0; i < len; ++i)
			{
				short x = (short)(this.verts[i].X - mediane);
				serialise.Int16(ref x);
				this.verts[i].X = x + mediane;
			}

			for (int i = 0; i < len; ++i)
			{
				short y = (short)(this.verts[i].Y - mediane);
				serialise.Int16(ref y);
				this.verts[i].Y = y + mediane;
			}

			for (int i = 0; i < len; ++i)
			{
				short z = (short)(this.verts[i].Z - mediane);
				serialise.Int16(ref z);
				this.verts[i].Z = z + mediane;
			}

			ushort[] links = new ushort[this.numItems - this.uniqueValues];
			serialise.Serialise(ref links);
			for (int i = this.uniqueValues; i < this.numItems; ++i)
			{
				this.verts[i] = this.verts[links[i - this.uniqueValues]];
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

		// CIwSVec3*       m_Verts;        //!< vertex list
	}
}