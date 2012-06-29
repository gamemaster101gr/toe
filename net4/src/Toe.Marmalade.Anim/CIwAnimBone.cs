using OpenTK;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Anim
{
	/// <summary>
	/// The c iw anim bone.
	/// </summary>
	public class CIwAnimBone : CIwResource
	{
		#region Constants and Fields

		private Vector3 pos;

		private Quaternion rot;
		uint parentBone = 0;
		ushort m_SkelID;       // ID of this bone within skeleton
		ushort m_Flags;        // e.g. UPDATE_WORLD_MAT_F
		float m_TransformPrecision = 1; // Amount this is scaled up by.


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
			
			serialise.UInt32(ref parentBone);

			serialise.SQuat(ref this.rot);
			serialise.SVec3(ref this.pos);

			serialise.UInt16(ref m_SkelID);
			serialise.UInt16(ref m_Flags);
			serialise.Fixed(ref m_TransformPrecision);
		}

		#endregion
	}
}