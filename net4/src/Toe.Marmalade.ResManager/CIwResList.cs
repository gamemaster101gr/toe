using System;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.ResManager
{
	/// <summary>
	/// The c iw res list.
	/// </summary>
	public class CIwResList : CIwManaged
	{
		#region Constants and Fields

		/// <summary>
		/// All the resources in the list.
		/// </summary>
		private readonly CIwManagedList resources = new CIwManagedList();

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets Size.
		/// </summary>
		public uint Size
		{
			get
			{
				return this.resources.Size;
			}
		}

		#endregion

		#region Public Indexers

		/// <summary>
		/// The this.
		/// </summary>
		/// <param name="i">
		/// The i.
		/// </param>
		public CIwResource this[int i]
		{
			get
			{
				return (CIwResource)this.resources[i];
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The get res hashed.
		/// </summary>
		/// <param name="name">
		/// The name.
		/// </param>
		/// <returns>
		/// </returns>
		public CIwResource GetResHashed(uint name)
		{
			for (int i = 0; i < this.resources.Size; ++i)
			{
				if (this.resources[i].Hash == name)
				{
					return (CIwResource)this.resources[i];
				}
			}

			return null;
		}

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="serialise">
		/// The serialise.
		/// </param>
		/// <exception cref="Exception">
		/// </exception>
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
				uint length = 0;
				serialise.UInt32(ref length);

				CIwManaged res = null;
				serialise.ManagedObject(this.Hash, ref res);
				this.resources.Add((CIwResource)res, false);
				--resCount;

				if (serialise.Position != pos + length)
				{
					throw new Exception(
						string.Format(
							"Parse of {0} failed: wrong position by {1} bytes", res.GetType().Name, serialise.Position - (pos + length)));
					serialise.Position = pos + length;
				}
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// The dispose.
		/// </summary>
		/// <param name="disposing">
		/// The disposing.
		/// </param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			while (this.resources.Size > 0)
			{
				var r = this.resources.PopBack();
				r.Dispose();
			}
		}

		#endregion
	}

	/// <summary>
	/// The c iw group directory entry.
	/// </summary>
	public class CIwGroupDirectoryEntry
	{
		#region Constants and Fields

		/// <summary>
		/// The hash.
		/// </summary>
		public uint Hash;

		/// <summary>
		/// The offset.
		/// </summary>
		public uint Offset;

		#endregion

		#region Methods

		private void Serialise(IwSerialise serialise)
		{
			serialise.UInt32(ref this.Hash);
			serialise.UInt32(ref this.Offset);
		}

		#endregion
	}
}