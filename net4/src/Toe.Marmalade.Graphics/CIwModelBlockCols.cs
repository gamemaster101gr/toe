using System;
using System.Drawing;

namespace Toe.Marmalade.Graphics
{
	public class CIwModelBlockCols : CIwModelBlock
	{
		private bool serialiseGrayscale;
		public override void Serialise(Util.IwSerialise serialise)
		{
			base.Serialise(serialise);
			serialise.Bool(ref this.serialiseGrayscale);
			if (this.serialiseGrayscale)
			{
				colors = new Color[numItems];
				var buf = new byte[numItems];
				serialise.Serialise(ref buf);
				for (int i = 0; i < numItems; ++i)
				{
					colors[i] = Color.FromArgb(255, buf[i], buf[i], buf[i]);
				}
			}
			else
			{
				colors = new Color[numItems];
				var buf = new byte[numItems * 4];
				serialise.Serialise(ref buf);
				for (int i = 0; i < numItems; ++i)
					colors[i] = Color.FromArgb(buf[i + numItems * 3], buf[i + numItems * 0], buf[i + numItems * 1], buf[i + numItems * 2]);
			}
		}

		private Color[] colors;

		// CIwColour*      m_Cols;     //!< colour list
	}
}