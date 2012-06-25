using System;
using System.Collections.Generic;

using OpenTK;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	public class CIwModelBlockVerts : CIwModelBlock
	{
		private ushort uniqueValues;

		public override void Serialise(IwSerialise serialise)
		{
			base.Serialise(serialise);
			{
				serialise.UInt16(ref this.uniqueValues);
			}
			short mediane = 0;
			serialise.Int16(ref mediane);

			var len = this.uniqueValues;// this.numItems; //

			Resize(this.numItems);

			if (serialise.IsWriting())
				throw new NotImplementedException();

			for (int i = 0; i < len; ++i)
			{
				short x = (short)(verts[i].X-mediane);
				serialise.Int16(ref x);
				verts[i].X = x + mediane;
			}
			for (int i = 0; i < len; ++i)
			{
				short y = (short)(verts[i].Y - mediane);
				serialise.Int16(ref y);
				verts[i].Y = y + mediane;
			}
			for (int i = 0; i < len; ++i)
			{
				short z = (short)(verts[i].Z - mediane);
				serialise.Int16(ref z);
				verts[i].Z = z+mediane;
			}

			ushort[] links = new ushort[this.numItems-this.uniqueValues];
			serialise.Serialise(ref links);
			for (int i=this.uniqueValues;i<numItems;++i)
			{
				verts[i] = verts[links[i - this.uniqueValues]];
			}
		}

		private void Resize(ushort length)
		{
			if (verts == null)
				verts = new Vector3[length];
			else
				Array.Resize(ref verts,length);
		}

		Vector3[] verts;

		// CIwSVec3*       m_Verts;        //!< vertex list
	}
}