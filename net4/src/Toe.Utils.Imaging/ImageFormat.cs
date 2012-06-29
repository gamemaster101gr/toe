using System;
using System.Drawing;
using System.IO;

namespace Toe.Utils.Imaging
{
	/// <summary>
	/// The image format.
	/// </summary>
	public abstract class ImageFormat
	{
		#region Constants and Fields

		/// <summary>
		/// The height.
		/// </summary>
		protected readonly int height;

		/// <summary>
		/// The pitch.
		/// </summary>
		protected readonly int pitch;

		/// <summary>
		/// The width.
		/// </summary>
		protected readonly int width;

		/// <summary>
		/// The palette.
		/// </summary>
		protected Color[] palette;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageFormat"/> class.
		/// </summary>
		public ImageFormat()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageFormat"/> class.
		/// </summary>
		/// <param name="width">
		/// The width.
		/// </param>
		/// <param name="height">
		/// The height.
		/// </param>
		public ImageFormat(int width, int height)
		{
			this.width = width;
			this.height = height;
			this.pitch = width;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageFormat"/> class.
		/// </summary>
		/// <param name="width">
		/// The width.
		/// </param>
		/// <param name="height">
		/// The height.
		/// </param>
		/// <param name="pitch">
		/// The pitch.
		/// </param>
		public ImageFormat(int width, int height, int pitch)
		{
			this.width = width;
			this.height = height;
			this.pitch = pitch;
		}

		#endregion

		#region Public Events

		/// <summary>
		/// Event on pixel readed.
		/// </summary>
		public event EventHandler<OnPixelArgs> OnPixel;

		#endregion

		#region Methods

		/// <summary>
		/// The raise on pixel.
		/// </summary>
		/// <param name="args">
		/// The args.
		/// </param>
		protected void RaiseOnPixel(OnPixelArgs args)
		{
			if (this.OnPixel != null)
			{
				this.OnPixel(this, args);
			}
		}

		/// <summary>
		/// The read palette arg b 1555.
		/// </summary>
		/// <param name="stream">
		/// The stream.
		/// </param>
		/// <param name="len">
		/// The len.
		/// </param>
		protected void ReadPaletteARGB1555(Stream stream, int len)
		{
			this.palette = new Color[len];
			for (int i = 0; i < this.palette.Length; ++i)
			{
				byte a = (byte)stream.ReadByte();
				byte b = (byte)stream.ReadByte();
				ushort r = (ushort)(a | (ushort)(b << 8));

				this.palette[i] = Color.FromArgb(
					(0 != (r >> 15)) ? 255 : 0, 
					(byte)(((r >> 10) & 0x1F) << 3), 
					(byte)(((r >> 5) & 0x1F) << 3), 
					(byte)(((r >> 0) & 0x1F) << 3));
			}
		}

		/// <summary>
		/// The read palette rg b 888.
		/// </summary>
		/// <param name="stream">
		/// The stream.
		/// </param>
		/// <param name="len">
		/// The len.
		/// </param>
		protected void ReadPaletteRGB888(Stream stream, int len)
		{
			this.palette = new Color[len];
			for (int i = 0; i < this.palette.Length; ++i)
			{
				byte r = (byte)stream.ReadByte();
				byte g = (byte)stream.ReadByte();
				byte b = (byte)stream.ReadByte();

				this.palette[i] = Color.FromArgb(255, r, g, b);
			}
		}

		#endregion
	}
}