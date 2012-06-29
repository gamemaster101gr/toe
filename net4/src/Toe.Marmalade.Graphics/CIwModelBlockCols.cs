using System.Drawing;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model block cols.
	/// </summary>
	public class CIwModelBlockCols : CIwModelBlock
	{
		#region Constants and Fields

		private Color[] colors;

		private bool serialiseGrayscale;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets Colors.
		/// </summary>
		public Color[] Colors
		{
			get
			{
				return this.colors;
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
			serialise.Bool(ref this.serialiseGrayscale);
			if (this.serialiseGrayscale)
			{
				this.colors = new Color[this.numItems];
				var buf = new byte[this.numItems];
				serialise.Serialise(ref buf);
				for (int i = 0; i < this.numItems; ++i)
				{
					this.Colors[i] = Color.FromArgb(255, buf[i], buf[i], buf[i]);
				}
			}
			else
			{
				this.colors = new Color[this.numItems];
				var buf = new byte[this.numItems * 4];
				serialise.Serialise(ref buf);
				for (int i = 0; i < this.numItems; ++i)
				{
					this.Colors[i] = Color.FromArgb(
						buf[i + this.numItems * 3], buf[i + this.numItems * 0], buf[i + this.numItems * 1], buf[i + this.numItems * 2]);
				}
			}
		}

		#endregion

		// CIwColour*      m_Cols;     //!< colour list
	}
}