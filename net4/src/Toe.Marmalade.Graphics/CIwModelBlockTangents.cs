using System;

using OpenTK;

namespace Toe.Marmalade.Graphics
{
	public class CIwModelBlockTangents: CIwModelBlock
	{
		public override void Serialise(Util.IwSerialise serialise)
		{
			base.Serialise(serialise);
			short mediane = 0;
			serialise.Int16(ref mediane);

			Resize(numItems);

			for (int i = 0; i < numItems; ++i)
			{
				short x = (short)(this.tangents[i].X - mediane);
				serialise.Int16(ref x);
				this.tangents[i].X = x + mediane;
			}
			for (int i = 0; i < numItems; ++i)
			{
				short y = (short)(this.tangents[i].Y - mediane);
				serialise.Int16(ref y);
				this.tangents[i].Y = y + mediane;
			}
			for (int i = 0; i < numItems; ++i)
			{
				short z = (short)(this.tangents[i].Z - mediane);
				serialise.Int16(ref z);
				this.tangents[i].Z = z + mediane;
			}
		}
		private void Resize(ushort length)
		{
			if (this.tangents == null)
				this.tangents = new Vector3[length];
			else
				Array.Resize(ref this.tangents, length);
		}

		Vector3[] tangents;
	}
}