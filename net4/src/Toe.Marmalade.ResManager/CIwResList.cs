using System;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.ResManager
{
	public class CIwResList: CIwManaged
	{
		/// <summary>
		/// All the resources in the list.
		/// </summary>
		CIwManagedList resources = new CIwManagedList();

		public uint Size
		{
			get
			{
				return resources.Size;
			}
		}

		public override void Serialise(IwSerialise serialise)
		{
			base.Serialise(serialise);

			uint resCount = 0;
			serialise.UInt32(ref resCount);

			bool unknown0 = false;
			serialise.Bool(ref unknown0);
			bool unknown1 = true;
			serialise.Bool(ref unknown1);

			while (resCount > 0)
			{
				var pos = serialise.Position;
				UInt32 length = 0;
				serialise.UInt32(ref length);

				CIwManaged res = null;
				serialise.ManagedObject(Hash, ref res);
				resources.Add((CIwResource)res,false);
				--resCount;

				if (serialise.Position != pos + length)
				{
					serialise.Position = pos + length;
					throw new Exception(string.Format("Parse of {0} failed", res.GetType().Name));
				}
			}
		}

		public CIwResource GetResHashed(uint name)
		{
			for (int i = 0; i < resources.Size; ++i)
				if (resources[i].Hash == name)
					return (CIwResource)resources[i];
			return null;
		}

		public CIwResource this[int i]
		{
			get
			{
				return (CIwResource)resources[i];
			}
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			while (resources.Size > 0)
			{
				var r = resources.PopBack();
				r.Dispose();
			}
		}
	}
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