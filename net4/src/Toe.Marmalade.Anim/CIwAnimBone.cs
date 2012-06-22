using OpenTK;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Anim
{
	/// <summary>
	/// The c iw anim bone.
	/// </summary>
	public class CIwAnimBone: CIwResource
	{
		private Quaternion rot;
		private Vector3 pos;
		public override void Serialise(IwSerialise serialise)
		{
			base.Serialise(serialise);

			uint unknown0 = 0;
			serialise.UInt32(ref unknown0);

			serialise.SQuat(ref rot);
			serialise.SVec3(ref pos);

			ushort unknown1 = 0;
			serialise.UInt16(ref unknown1);
			ushort unknown2 = 0;
			serialise.UInt16(ref unknown2);
		}
	}
}