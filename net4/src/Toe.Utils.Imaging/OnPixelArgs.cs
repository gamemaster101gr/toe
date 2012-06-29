using System;
using System.Drawing;

namespace Toe.Utils.Imaging
{
	/// <summary>
	/// The on pixel args.
	/// </summary>
	public class OnPixelArgs : EventArgs
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets Color.
		/// </summary>
		public Color Color { get; set; }

		/// <summary>
		/// Gets or sets X.
		/// </summary>
		public int X { get; set; }

		/// <summary>
		/// Gets or sets Y.
		/// </summary>
		public int Y { get; set; }

		#endregion
	}
}