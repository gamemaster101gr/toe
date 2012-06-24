using System;
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

		private void ReadGroupResources(IwSerialise iwSerialise)
		{
			uint numResources = 0;
			iwSerialise.UInt32(ref numResources);
			while (numResources>0)
			{
				uint resHash = 0;
				iwSerialise.UInt32(ref resHash);
				uint resCount = 0;
				iwSerialise.UInt32(ref resCount);

				bool unknown0 = false;
				iwSerialise.Bool(ref unknown0);
				bool unknown1 = false;
				iwSerialise.Bool(ref unknown1);

				while (resCount > 0)
				{
					var pos = iwSerialise.Position;
					UInt32 unknown2 = 0;
					iwSerialise.UInt32(ref unknown2);

					CIwManaged res = null;
					iwSerialise.ManagedObject(resHash, ref res);
					Add((CIwResource)res);
					--resCount;

					if (iwSerialise.Position != pos+unknown2)
					{
						iwSerialise.Position = pos + unknown2;
					}
				}
				--numResources;
			}
		}

		private void Add(CIwResource res)
		{
			Debug.WriteLine(string.Format("Adding resource {0}", res.GetType().Name));
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
	}
}