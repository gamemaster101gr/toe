using System;

namespace Toe.Marmalade.Util
{
	public class IwSerialiseBinaryBlock
	{
		private readonly IwSerialise serialise;
		byte magic = 0x3D;
		byte major = 3;
		byte minor = 6;
		byte rev = 6;

		private long position;
		private uint length;

		public	IwSerialiseBinaryBlock(IwSerialise serialise)
		{
			this.serialise = serialise;
		}

		protected void Open()
		{
			serialise.UInt8(ref magic);
			serialise.UInt8(ref major);
			serialise.UInt8(ref minor);
			serialise.UInt8(ref rev);
			serialise.SetVersion(major, minor, rev);

			if (serialise.IsReading() && magic != 0x3D)
				throw new ArgumentException();

			ushort unknown = 0;
			serialise.UInt16(ref unknown);
		}

		public void OpenBlock(ref uint hash)
		{
			serialise.UInt32(ref hash);
			position = serialise.Position;
			if (hash != 0)
			{
				serialise.UInt32(ref length);
			}
			else
			{
				length = 0;
			}
		}
		public void CloseBlock()
		{
			if (serialise.IsWriting())
			{
				var p = serialise.Position;
				serialise.Position = position;
				length = (uint)(p - position);
				serialise.UInt32(ref length);
				serialise.Position = p;
			}
		}

		protected void Close()
		{
			
		}
		public void Serialise()
		{
			this.Open();
			if (serialise.IsReading())
				this.Read();
			else
				this.Write();
			this.Close();
		}

		protected virtual void Write()
		{
			//this.WriteBlocks();
		}

		private void Read()
		{
			uint hash = 0;
			for (; ; )
			{
				this.OpenBlock(ref hash);
				if (hash == 0 && this.length == 0) return;
				if (this.Block != null)
				{
					this.Block(this, new BinaryBlockEventArgs(hash, this.serialise, this.length - 4));
				}
				if (this.serialise.Position != this.position + this.length)
				{
					throw new FormatException(string.Format("Binary block position difference is {0}", this.serialise.Position - (this.position + this.length)));
					this.serialise.Position = this.position + (long)this.length;
				}
			}
		}

		public event EventHandler<BinaryBlockEventArgs> Block;
	}
	public class BinaryBlockEventArgs : EventArgs
	{
		private readonly uint hash;

		private readonly IwSerialise iwSerialise;

		private readonly uint len;

		public BinaryBlockEventArgs(uint hash, IwSerialise iwSerialise, uint len)
		{
			this.hash = hash;
			this.iwSerialise = iwSerialise;
			this.len = len;
		}

		public IwSerialise IwSerialise
		{
			get
			{
				return this.iwSerialise;
			}
		}

		public uint Hash
		{
			get
			{
				return this.hash;
			}
		}
	}
}