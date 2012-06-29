using System.IO;

namespace Toe.Utils.Imaging
{
	/// <summary>
	/// The palette 4 rgb 888.
	/// </summary>
	public class Palette4Rgb888 : ImageFormat, IImageFormat
	{
		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Palette4Rgb888"/> class.
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
		public Palette4Rgb888(int width, int height, int pitch)
			: base(width, height, pitch)
		{
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Read and decode data block.
		/// </summary>
		/// <param name="stream">
		/// The source stream.
		/// </param>
		public void ReadData(Stream stream)
		{
			// Read palette first.
			int dataLen = this.pitch * this.height;
			long pos = stream.Position;
			stream.Position += dataLen;
			this.ReadPaletteRGB888(stream, 16);
			long end = stream.Position;
			stream.Position = pos;

			// Decode pixels.
			var onPixelArgs = new OnPixelArgs();
			for (int y = 0; y < this.height; ++y)
			{
				for (int x = 0; x < this.width; ++x)
				{
					var index = stream.ReadByte();
					byte i0 = (byte)(index & 0x0F);
					byte i1 = (byte)((index >> 4) & 0x0F);

					onPixelArgs.X = x;
					onPixelArgs.Y = y;
					onPixelArgs.Color = this.palette[i0];
					this.RaiseOnPixel(onPixelArgs);

					++x;
					onPixelArgs.X = x;
					onPixelArgs.Y = y;
					onPixelArgs.Color = this.palette[i1];
					this.RaiseOnPixel(onPixelArgs);
				}
			}

			// Set position after palette.
			stream.Position = end;
		}

		#endregion
	}
}