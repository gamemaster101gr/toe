using System;

using OpenTK;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	public class CIwModelBlockNorms : CIwModelBlock
	{
		public override void Serialise(IwSerialise serialise)
		{
			base.Serialise(serialise);
			short mediane = 0;
			serialise.Int16(ref mediane);

			Resize(numItems);

			for (int i = 0; i < numItems; ++i)
			{
				short x = (short)(verts[i].X - mediane);
				serialise.Int16(ref x);
				verts[i].X = x + mediane;
			}
			for (int i = 0; i < numItems; ++i)
			{
				short y = (short)(verts[i].Y - mediane);
				serialise.Int16(ref y);
				verts[i].Y = y + mediane;
			}
			for (int i = 0; i < numItems; ++i)
			{
				short z = (short)(verts[i].Z - mediane);
				serialise.Int16(ref z);
				verts[i].Z = z + mediane;
			}
		}

		private void Resize(ushort length)
		{
			if (verts == null)
				verts = new Vector3[length];
			else
				Array.Resize(ref verts, length);
		}

		Vector3[] verts;
		// CIwSVec3*       m_Norms;    //!< vertex normal list
		public Vector3[] Normal
		{
			get
			{
				return verts;
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}