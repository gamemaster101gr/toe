using System;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model block face flags.
	/// </summary>
	public class CIwModelBlockFaceFlags : CIwModelBlock
	{
		#region Public Methods and Operators

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="serialise">
		/// The serialise.
		/// </param>
		/// <exception cref="NotImplementedException">
		/// </exception>
		public override void Serialise(IwSerialise serialise)
		{
			base.Serialise(serialise);
			throw new NotImplementedException();
		}

		#endregion

		////uint8* m_FaceFlags; //!< per face flag list
		////bool m_WorldSet; //!< True if block contains world file variations to sets
	}
}