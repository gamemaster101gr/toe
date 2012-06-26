using System;
using System.Collections.Generic;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.ResManager
{
	/// <summary>
	/// The c iw res group.
	/// </summary>
	public class CIwResGroup : CIwManaged
	{
		private bool isDisposed;
		~CIwResGroup()
		{
			if (!isDisposed)
			{
				isDisposed = true;
				this.Dispose(false);
			}
		}
		#region Constants and Fields

		/// <summary>
		/// Group has an atlas.
		/// </summary>
		public const uint AtlasF = 1 << 2;

		/// <summary>
		/// Atlas is ready for upload on load phase.
		/// </summary>
		public const uint AtlasReadyF = 1 << 3;

		/// <summary>
		/// Group was loaded from binary.
		/// </summary>
		public const uint LoadedF = 1 << 1;

		/// <summary>
		/// Group can be mounted.
		/// </summary>
		public const uint MountableF = 1 << 4;

		/// <summary>
		/// Group has been optimised - cannot be loaded normally.
		/// </summary>
		public const uint OptimisedF = 1 << 5;

		/// <summary>
		/// This group has already been resolved.
		/// </summary>
		public const uint RESOLVED_F = 1 << 6;

		/// <summary>
		/// Group is "shared" (so involved in more searches).
		/// </summary>
		public const uint SharedF = 1 << 0;

		private readonly List<CIwResGroup> childGroups = new List<CIwResGroup>();

		private readonly CIwArray<CIwResList> lists = new CIwArray<CIwResList>();

		private readonly IwResManager resManager;

		private uint flags;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CIwResGroup"/> class.
		/// </summary>
		/// <param name="resManager">
		/// The res manager.
		/// </param>
		public CIwResGroup(IwResManager resManager)
		{
			this.resManager = resManager;
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The get list hashed.
		/// </summary>
		/// <param name="toeHash">
		/// The toe hash.
		/// </param>
		/// <returns>
		/// </returns>
		public CIwResList GetListHashed(uint toeHash)
		{
			for (int i = 0; i < this.lists.Size; ++i)
			{
				if (this.lists[i].Hash == toeHash)
				{
					return this.lists[i];
				}
			}

			return null;
		}

		/// <summary>
		/// The get list named.
		/// </summary>
		/// <param name="name">
		/// The name.
		/// </param>
		/// <returns>
		/// </returns>
		public CIwResList GetListNamed(string name)
		{
			return this.GetListHashed(name.ToeHash());
		}

		/// <summary>
		/// The get res hashed.
		/// </summary>
		/// <param name="name">
		/// The name.
		/// </param>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <returns>
		/// </returns>
		public CIwResource GetResHashed(uint name, string type)
		{
			return this.GetResHashed(name, type.ToeHash());
		}

		/// <summary>
		/// The get res hashed.
		/// </summary>
		/// <param name="name">
		/// The name.
		/// </param>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <returns>
		/// </returns>
		public CIwResource GetResHashed(uint name, uint type)
		{
			var l = this.GetListHashed(type);
			if (l != null)
			{
				return l.GetResHashed(name);
			}

			return null;
		}

		/// <summary>
		/// The get res named.
		/// </summary>
		/// <param name="name">
		/// The name.
		/// </param>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <returns>
		/// </returns>
		public CIwResource GetResNamed(string name, string type)
		{
			return this.GetResHashed(name.ToeHash(), type);
		}

		/// <summary>
		/// The parse attribute.
		/// </summary>
		/// <param name="pParser">
		/// The p parser.
		/// </param>
		/// <param name="pAttrName">
		/// The p attr name.
		/// </param>
		/// <returns>
		/// The parse attribute.
		/// </returns>
		public override bool ParseAttribute(CIwTextParserITX pParser, string pAttrName)
		{
			return base.ParseAttribute(pParser, pAttrName);
		}

		#endregion

		#region Methods

		internal void Read(IwSerialise iwSerialise)
		{
			var b = new IwSerialiseBinaryBlock(iwSerialise);
			b.Block += this.ReadBlock;
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
				string path = string.Empty;
				iwSerialise.String(ref path);
				this.childGroups.Add(this.resManager.LoadGroup(path, false));
				--num;
			}
		}

		private void ReadGroupResources(IwSerialise serialise)
		{
			uint numResources = 0;
			serialise.UInt32(ref numResources);
			while (numResources > 0)
			{
				// uint resHash = 0;
				// serialise.UInt32(ref resHash);
				var item = new CIwResList();
				item.Serialise(serialise);
				this.lists.PushBack(item);
				--numResources;
			}
		}

		private void ReadResGroupMembers(IwSerialise iwSerialise)
		{
			string name = string.Empty;
			iwSerialise.String(ref name);

			iwSerialise.UInt32(ref this.flags);
		}

		private void Write(IwSerialise iwSerialise)
		{
			var b = new IwSerialiseBinaryBlock(iwSerialise);
			b.Block += this.WriteBlock;
			b.Serialise();
		}

		private void WriteBlock(object sender, BinaryBlockEventArgs e)
		{
			throw new NotImplementedException();
		}

		#endregion

		public void Dispose()
		{
			if (!isDisposed)
			{
				isDisposed = true;
				Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		protected  virtual void Dispose(bool disposing)
		{
			if (this.lists != null)
			{
				while (this.lists.Size > 0)
				{
					var p = this.lists.PopBack();
					p.Dispose();
				}
			}
		}
	}
}