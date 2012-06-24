using Toe.Marmalade.Util;

namespace Toe.Marmalade.ResManager
{
	public class CIwResList: CIwManaged
	{}
	public class CIwGroupDirectoryEntry
	{
		void Serialise(IwSerialise serialise)
		{
			serialise.UInt32(ref Hash);
			serialise.UInt32(ref Offset);
		}

		public uint Hash;
		public uint Offset;
	}
}