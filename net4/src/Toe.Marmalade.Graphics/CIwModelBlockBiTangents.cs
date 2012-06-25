using System;

using OpenTK;

namespace Toe.Marmalade.Graphics
{
	public class CIwModelBlockBiTangents : CIwModelBlock
	{
		public override void Serialise(Util.IwSerialise serialise)
		{
			base.Serialise(serialise);
			short mediane = 0;
			serialise.Int16(ref mediane);

			Resize(numItems);

			for (int i = 0; i < numItems; ++i)
			{
				short x = (short)(this.biTangents[i].X - mediane);
				serialise.Int16(ref x);
				this.biTangents[i].X = x + mediane;
			}
			for (int i = 0; i < numItems; ++i)
			{
				short y = (short)(this.biTangents[i].Y - mediane);
				serialise.Int16(ref y);
				this.biTangents[i].Y = y + mediane;
			}
			for (int i = 0; i < numItems; ++i)
			{
				short z = (short)(this.biTangents[i].Z - mediane);
				serialise.Int16(ref z);
				this.biTangents[i].Z = z + mediane;
			}
		}
		private void Resize(ushort length)
		{
			if (this.biTangents == null)
				this.biTangents = new Vector3[length];
			else
				Array.Resize(ref this.biTangents, length);
		}

		Vector3[] biTangents;
	}
}