using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model ext sel set face.
	/// </summary>
	public class CIwModelExtSelSetFace : CIwModelExtSelSet
	{
		#region Constants and Fields

		private readonly CIwArray<ushort> m_FaceIDs = new CIwArray<ushort>(); // !< IDs into model builder face array.

		private byte m_Flags; // !< CIwFace flags - general.

		private byte m_FlagsHW; // !< CIwFace flags - HW.

		private byte m_FlagsSW; // !< CIwFace flags - SW.

		private uint m_NumFaces; // !< Number of faces in model this set applies to

		private sbyte m_OTZOfsSW; // !< OTZ offset for SW.

		private bool m_WorldSet; // !< True if this set is a world file only set

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

			////serialise.DebugWrite(256);

			// serialise.UInt8(ref m_Flags);
			serialise.Bool(ref this.m_WorldSet);
			serialise.UInt8(ref this.m_FlagsSW);
			serialise.UInt8(ref this.m_FlagsHW);
			serialise.UInt32(ref this.m_NumFaces);
			serialise.Int8(ref this.m_OTZOfsSW);
			this.m_FaceIDs.SerialiseHeader(serialise);
			this.m_FaceIDs.ForEach(
				(ref ushort v) =>
					{
						serialise.Serialise(ref v);
						return true;
					});

			////serialise.DebugWrite(256);
		}

		#endregion
	}
}