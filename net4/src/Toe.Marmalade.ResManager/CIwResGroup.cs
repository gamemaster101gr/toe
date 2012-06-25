using System;
using System.Collections.Generic;
using System.Diagnostics;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.ResManager
{
	public class CIwResGroup:CIwManaged
	{
		private UInt32 flags = 0;
		/// <summary>
		/// group is "shared" (so involved in more searches).
		/// </summary>
		public const uint SharedF = (1 << 0);

		/// <summary>
		/// group was loaded from binary.
		/// </summary>
		public const uint LoadedF = (1 << 1);         

		/// <summary>
		/// group has an atlas.
		/// </summary>
		public const uint AtlasF = (1 << 2);         

		/// <summary>
		/// atlas is ready for upload on load phase.
		/// </summary>
		public const uint AtlasReadyF = (1 << 3);       

		/// <summary>
		/// group can be mounted.
		/// </summary>
		public const uint MountableF = (1 << 4);        

		/// <summary>
		/// group has been optimised - cannot be loaded normally.
		/// </summary>
		public const uint OptimisedF = (1 << 5);         

		/// <summary>
		/// this group has already been resolved.
		/// </summary>
		public const uint RESOLVED_F = (1 << 6);

		private CIwArray<CIwResList> lists = new CIwArray<CIwResList>();
     
		public override bool ParseAttribute(CIwTextParserITX pParser, string pAttrName)
		{
			return base.ParseAttribute(pParser, pAttrName);
		}

		internal void Read(IwSerialise iwSerialise)
		{
			var b = new IwSerialiseBinaryBlock(iwSerialise);
			b.Block += ReadBlock;
			b.Serialise();
		}

		private void ReadBlock(object sender, BinaryBlockEventArgs e)
		{
			switch (e.Hash)
			{
				case 0x8081E087:
					this.ReadResGroupMembers(e.IwSerialise);
					break;
				case 0xDC3C2177:
					this.ReadGroupResources(e.IwSerialise);
					break;
				case 0x3b495dc0:
					this.ReadChildGroups(e.IwSerialise);
					break;
			}
		}

		private void ReadChildGroups(IwSerialise iwSerialise)
		{
			byte num = 0;
			iwSerialise.UInt8(ref num);
			while (num > 0)
			{
				string path = "";
				iwSerialise.String(ref path);
				--num;
			}
		}

		private void ReadGroupResources(IwSerialise serialise)
		{
			uint numResources = 0;
			serialise.UInt32(ref numResources);
			while (numResources>0)
			{
				//uint resHash = 0;
				//serialise.UInt32(ref resHash);
				var item = new CIwResList();
				item.Serialise(serialise);
				lists.PushBack(item);
				--numResources;
			}
		}

		private void ReadResGroupMembers(IwSerialise iwSerialise)
		{
			string name = String.Empty;
			iwSerialise.String(ref name);

			iwSerialise.UInt32(ref flags);
		}

		private void Write(IwSerialise iwSerialise)
		{
			var b = new IwSerialiseBinaryBlock(iwSerialise);
			b.Block += WriteBlock;
			b.Serialise();
		}

		private void WriteBlock(object sender, BinaryBlockEventArgs e)
		{
			throw new NotImplementedException();
		}

		public bool TryResolve(uint type, uint hash, out CIwResource res)
		{
			res = null;
			return false;
		}

		public CIwResList GetListNamed(string name)
		{
			return this.GetListHashed(name.ToeHash());
		}

		private CIwResList GetListHashed(uint toeHash)
		{
			for (int i = 0; i < lists.Size;++i)
			{
				if (this.lists[i].Hash == toeHash) return this.lists[i];
			}
			return null;
		}

		private CIwResource GetResNamed(string name, string type)
		{
			return this.GetResHashed(name.ToeHash(), type);
		}

		private CIwResource GetResHashed(uint name, string type)
		{
			return this.GetResHashed(name, type.ToeHash());
		}

		private CIwResource GetResHashed(uint name, uint type)
		{
			var l = this.GetListHashed(type);
			if (l != null)
			{
				return l.GetResHashed(name);
			}
			return null;
		}
	}
}