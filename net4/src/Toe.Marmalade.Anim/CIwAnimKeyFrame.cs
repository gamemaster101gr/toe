using OpenTK;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Anim
{
	public class CIwAnimKeyFrame: CIwManaged
	{

		public override void Serialise(IwSerialise serialise)
		{
			base.Serialise(serialise);
			{
				int value = 0;
				serialise.Int32(ref value);
			}
			{
				byte value = 2;  //1b=3
				serialise.UInt8(ref value);
			}
			{
				Vector3 value = Vector3.Zero;
				serialise.SVec3(ref value);
			}
			{
				byte value = 1;
				serialise.UInt8(ref value);
			}
			{
				uint value = 3; //1b=1
				serialise.UInt32(ref value);
			}
			{
				uint value = 2; //1b=1 num of bones?
				serialise.UInt32(ref value);
			}
			//1b=1 one more uint32?
			{
				bool value = true;
				serialise.Bool(ref value);
			}

			SerialiseMappedData(serialise); //1b-none

			// by number of bones
			{
				// 0x1d38b3fd
				Quaternion value = Quaternion.Identity;
				serialise.SQuat(ref value);
			}
			{
				// 0xa272adcf
				Quaternion value = Quaternion.Identity;
				serialise.SQuat(ref value);
			}
		}

		private void SerialiseMappedData(IwSerialise serialise)
		{
			ushort median = 8;
			serialise.Serialise(ref median);
			ushort[] data = new ushort[8];
			serialise.Serialise(ref data);
		}
	}
}