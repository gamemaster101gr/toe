using System;

using OpenTK;

namespace Toe.Marmalade.Graphics
{
	public class CIwModelBlockGLUVs : CIwModelBlock
	{
		public override void Serialise(Util.IwSerialise serialise)
		{
			base.Serialise(serialise);
			short mediane = 0;
			//serialise.Int16(ref mediane);

			Resize(numItems);

			for (int i = 0; i < numItems; ++i)
			{
				short x = (short)((verts[i].X - mediane) * (float)S3E.IwGeomOne);
				serialise.Int16(ref x);
				verts[i].X = (x + mediane)/(float)S3E.IwGeomOne;

				short y = (short)((verts[i].Y - mediane) * (float)S3E.IwGeomOne);
				serialise.Int16(ref y);
				verts[i].Y = (y + mediane) / (float)S3E.IwGeomOne;
			}
		}

		private void Resize(ushort length)
		{
			if (verts == null)
				verts = new Vector2[length];
			else
				Array.Resize(ref verts, length);
		}

		Vector2[] verts;

		public Vector2[] UVs
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