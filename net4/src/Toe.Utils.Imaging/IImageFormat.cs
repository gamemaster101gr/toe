using System;
using System.IO;

namespace Toe.Utils.Imaging
{
	/// <summary>
	/// The i image format.
	/// </summary>
	public interface IImageFormat
	{
		#region Public Events

		/// <summary>
		/// Event on pixel readed.
		/// </summary>
		event EventHandler<OnPixelArgs> OnPixel;

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The read data.
		/// </summary>
		/// <param name="stream">
		/// The stream.
		/// </param>
		void ReadData(Stream stream);

		#endregion
	}
}