using System.IO;

namespace Toe.Utils.Imaging
{
	/// <summary>
	/// The palette 8 abgr 1555.
	/// </summary>
	public class Palette8Abgr1555 : ImageFormat, IImageFormat
	{
		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Palette8Abgr1555"/> class.
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
		public Palette8Abgr1555(int width, int height, int pitch)
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
			this.ReadPaletteARGB1555(stream, 256);
			long end = stream.Position;
			stream.Position = pos;

			// Decode pixels.
			var onPixelArgs = new OnPixelArgs();
			for (int y = 0; y < this.height; ++y)
			{
				for (int x = 0; x < this.width; ++x)
				{
					var index = stream.ReadByte();

					onPixelArgs.X = x;
					onPixelArgs.Y = y;
					onPixelArgs.Color = this.palette[index];
					this.RaiseOnPixel(onPixelArgs);
				}
			}

			// Set position after palette.
			stream.Position = end;
		}

		#endregion
	}
}