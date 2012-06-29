using System;

namespace Toe.Marmalade.Util
{
	/// <summary>
	/// The iw serialise binary block.
	/// </summary>
	public class IwSerialiseBinaryBlock
	{
		#region Constants and Fields

		private readonly IwSerialise serialise;

		private uint length;

		private byte magic = 0x3D;

		private byte major = 3;

		private byte minor = 6;

		private long position;

		private byte rev = 6;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="IwSerialiseBinaryBlock"/> class.
		/// </summary>
		/// <param name="serialise">
		/// The serialise.
		/// </param>
		public IwSerialiseBinaryBlock(IwSerialise serialise)
		{
			this.serialise = serialise;
		}

		#endregion

		#region Public Events

		/// <summary>
		/// The block.
		/// </summary>
		public event EventHandler<BinaryBlockEventArgs> Block;

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The close block.
		/// </summary>
		public void CloseBlock()
		{
			if (this.serialise.IsWriting())
			{
				var p = this.serialise.Position;
				this.serialise.Position = this.position;
				this.length = (uint)(p - this.position);
				this.serialise.UInt32(ref this.length);
				this.serialise.Position = p;
			}
		}

		/// <summary>
		/// The open block.
		/// </summary>
		/// <param name="hash">
		/// The hash.
		/// </param>
		public void OpenBlock(ref uint hash)
		{
			this.serialise.UInt32(ref hash);
			this.position = this.serialise.Position;
			if (hash != 0)
			{
				this.serialise.UInt32(ref this.length);
			}
			else
			{
				this.length = 0;
			}
		}

		/// <summary>
		/// The serialise.
		/// </summary>
		public void Serialise()
		{
			this.Open();
			if (this.serialise.IsReading())
			{
				this.Read();
			}
			else
			{
				this.Write();
			}

			this.Close();
		}

		#endregion

		#region Methods

		/// <summary>
		/// The close.
		/// </summary>
		protected void Close()
		{
		}

		/// <summary>
		/// The open.
		/// </summary>
		/// <exception cref="ArgumentException">
		/// </exception>
		protected void Open()
		{
			this.serialise.UInt8(ref this.magic);
			this.serialise.UInt8(ref this.major);
			this.serialise.UInt8(ref this.minor);
			this.serialise.UInt8(ref this.rev);
			this.serialise.SetVersion(this.major, this.minor, this.rev);

			if (this.serialise.IsReading() && this.magic != 0x3D)
			{
				throw new ArgumentException();
			}

			ushort unknown = 0;
			this.serialise.UInt16(ref unknown);
		}

		/// <summary>
		/// The write.
		/// </summary>
		protected virtual void Write()
		{
			// this.WriteBlocks();
		}

		private void Read()
		{
			uint hash = 0;
			for (;;)
			{
				this.OpenBlock(ref hash);
				if (hash == 0 && this.length == 0)
				{
					return;
				}

				if (this.Block != null)
				{
					this.Block(this, new BinaryBlockEventArgs(hash, this.serialise, this.length - 4));
				}

				if (this.serialise.Position != this.position + this.length)
				{
					throw new FormatException(
						string.Format(
							"{0}: Binary block position difference is {1} at 0x{2:x8}", 
							this.serialise, 
							this.serialise.Position - (this.position + this.length), 
							this.serialise.Position));
					this.serialise.Position = this.position + (long)this.length;
				}
			}
		}

		#endregion
	}

	/// <summary>
	/// The binary block event args.
	/// </summary>
	public class BinaryBlockEventArgs : EventArgs
	{
		#region Constants and Fields

		private readonly uint hash;

		private readonly IwSerialise iwSerialise;

		private readonly uint len;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="BinaryBlockEventArgs"/> class.
		/// </summary>
		/// <param name="hash">
		/// The hash.
		/// </param>
		/// <param name="iwSerialise">
		/// The iw serialise.
		/// </param>
		/// <param name="len">
		/// The len.
		/// </param>
		public BinaryBlockEventArgs(uint hash, IwSerialise iwSerialise, uint len)
		{
			this.hash = hash;
			this.iwSerialise = iwSerialise;
			this.len = len;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets Hash.
		/// </summary>
		public uint Hash
		{
			get
			{
				return this.hash;
			}
		}

		/// <summary>
		/// Gets IwSerialise.
		/// </summary>
		public IwSerialise IwSerialise
		{
			get
			{
				return this.iwSerialise;
			}
		}

		#endregion
	}
}