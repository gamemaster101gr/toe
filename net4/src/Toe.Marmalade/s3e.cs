using System;
using System.Text;

using OpenTK.Graphics.OpenGL;

namespace Toe.Marmalade
{
	/// <summary>
	/// S3e namespace functions.
	/// </summary>
	public class S3E
	{
		#region Constants and Fields

		/// <summary>
		/// The iw geom one.
		/// </summary>
		public const int IwGeomOne = 1 << 12;

		/// <summary>
		/// Initial hash value.
		/// </summary>
		public const uint S3EHashInitial = 5381;

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The check open gl status.
		/// </summary>
		/// <exception cref="Exception">
		/// </exception>
		public static void CheckOpenGLStatus()
		{
			var code = GL.GetError();
			if (code != ErrorCode.NoError)
			{
				throw new Exception(string.Format("OpenGL Error: {0}", code));
			}
		}

		/// <summary>
		/// Case insensetive hash.
		/// </summary>
		/// <param name="text">
		/// String to calculate hash for.
		/// </param>
		/// <returns>
		/// Calculated hash.
		/// </returns>
		public static uint HashString(string text)
		{
			return HashString(text, S3EHashInitial);
		}

		/// <summary>
		/// Case insensetive hash.
		/// </summary>
		/// <param name="text">
		/// String to calculate hash for.
		/// </param>
		/// <param name="hash">
		/// Initial hash value.
		/// </param>
		/// <returns>
		/// Calculated hash.
		/// </returns>
		public static uint HashString(string text, uint hash)
		{
			if (string.IsNullOrEmpty(text))
			{
				return hash;
			}

			foreach (var c in Encoding.UTF8.GetBytes(text))
			{
				var cc = (c < 'A' || c > 'Z') ? c : (c - 'A' + 'a'); // Ignore case!
				hash = ((hash << 5) + hash) + (uint)cc;
			}

			return hash;
		}

		#endregion
	}
}