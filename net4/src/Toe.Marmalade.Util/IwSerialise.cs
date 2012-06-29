using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security;
using System.Text;

using OpenTK;

namespace Toe.Marmalade.Util
{
	/// <summary>
	/// The iw serialise.
	/// </summary>
	public class IwSerialise : IDisposable
	{
		#region Constants and Fields

		private readonly byte[] buffer = new byte[4];

		private readonly ClassRegistry classRegistry;

		private readonly IwSerialiseMode mode;

		private readonly IResourceResolver resourceResolver;

		private readonly Stream stream;

		private byte Major = 3;

		private byte Minor = 6;

		private byte Revision = 6;

		private bool isDisposed;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="IwSerialise"/> class.
		/// </summary>
		/// <param name="stream">
		/// The stream.
		/// </param>
		/// <param name="mode">
		/// The mode.
		/// </param>
		/// <param name="classRegistry">
		/// The class Registry.
		/// </param>
		/// <param name="resourceResolver">
		/// The resource Resolver.
		/// </param>
		public IwSerialise(
			Stream stream, IwSerialiseMode mode, ClassRegistry classRegistry, IResourceResolver resourceResolver)
		{
			this.stream = stream;
			this.mode = mode;
			this.classRegistry = classRegistry;
			this.resourceResolver = resourceResolver;
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="IwSerialise"/> class. 
		/// </summary>
		~IwSerialise()
		{
			this.Dispose(false);
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets ClassRegistry.
		/// </summary>
		public ClassRegistry ClassRegistry
		{
			get
			{
				return this.classRegistry;
			}
		}

		/// <summary>
		/// Gets Length.
		/// </summary>
		public long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		/// <summary>
		/// Gets or sets Position.
		/// </summary>
		public long Position
		{
			get
			{
				return this.stream.Position;
			}

			set
			{
				this.stream.Position = value;
			}
		}

		public Stream Stream
		{
			get
			{
				return stream;
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The open.
		/// </summary>
		/// <param name="filePath">
		/// The file path.
		/// </param>
		/// <param name="read">
		/// The read.
		/// </param>
		/// <param name="classRegistry">
		/// The class Registry.
		/// </param>
		/// <param name="resourceResolver">
		/// The resource Resolver.
		/// </param>
		/// <param name="ram">
		/// The ram.
		/// </param>
		/// <returns>
		/// </returns>
		public static IwSerialise Open(
			string filePath, bool read, ClassRegistry classRegistry, IResourceResolver resourceResolver, bool ram = false)
		{
			return Open(filePath, read ? IwSerialiseMode.Read : IwSerialiseMode.Write, classRegistry, resourceResolver, ram);
		}

		/// <summary>
		/// The open.
		/// </summary>
		/// <param name="filePath">
		/// The file path.
		/// </param>
		/// <param name="mode">
		/// The mode.
		/// </param>
		/// <param name="classRegistry">
		/// The class Registry.
		/// </param>
		/// <param name="resourceResolver">
		/// The resource Resolver.
		/// </param>
		/// <param name="ram">
		/// The ram.
		/// </param>
		/// <returns>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// </exception>
		public static IwSerialise Open(
			string filePath, 
			IwSerialiseMode mode, 
			ClassRegistry classRegistry, 
			IResourceResolver resourceResolver, 
			bool ram = false)
		{
			switch (mode)
			{
				case IwSerialiseMode.Read:
					return new IwSerialise(
						File.OpenRead(resourceResolver.ResolveFilePath(filePath, ram)), mode, classRegistry, resourceResolver);
				case IwSerialiseMode.Write:
					return new IwSerialise(
						File.OpenWrite(resourceResolver.ResolveFilePath(filePath, ram)), mode, classRegistry, resourceResolver);
				default:
					throw new ArgumentOutOfRangeException("mode");
			}
		}

		/// <summary>
		/// The bool.
		/// </summary>
		/// <param name="val">
		/// The val.
		/// </param>
		public void Bool(ref bool val)
		{
			if (this.mode == IwSerialiseMode.Read)
			{
				this.ReadInfoBuffer(1);
				val = this.buffer[0] != 0;
			}
			else
			{
				this.buffer[0] = (byte)(val ? 1 : 0);
				this.stream.Write(this.buffer, 0, 1);
			}
		}

		/// <summary>
		/// The char.
		/// </summary>
		/// <param name="val">
		/// The val.
		/// </param>
		public void Char(ref char val)
		{
			if (this.IsReading())
			{
				this.ReadInfoBuffer(1);
				val = (char)this.buffer[0];
			}
			else
			{
				var l = Encoding.UTF8.GetBytes(new[] { val }, 0, 1, this.buffer, 0);
				this.stream.Write(this.buffer, 0, l);
			}
		}

		/// <summary>
		/// The close.
		/// </summary>
		public void Close()
		{
			this.stream.Close();
		}

		/// <summary>
		/// The colour.
		/// </summary>
		/// <param name="color">
		/// The color.
		/// </param>
		public void Colour(ref Color color)
		{
			if (this.IsReading())
			{
				byte a = 0, r = 0, g = 0, b = 0;
				this.UInt8(ref r);
				this.UInt8(ref g);
				this.UInt8(ref b);
				this.UInt8(ref a);
				color = Color.FromArgb(a, r, g, b);
			}
			else
			{
				byte a = color.A, r = color.R, g = color.G, b = color.B;
				this.UInt8(ref r);
				this.UInt8(ref g);
				this.UInt8(ref b);
				this.UInt8(ref a);
			}
		}

		/// <summary>
		/// The debug write.
		/// </summary>
		/// <param name="len">
		/// The len.
		/// </param>
		public void DebugWrite(long len)
		{
			len = Math.Min(256, this.Length - this.Position);
			var p = this.Position;
			byte[] buf = new byte[len];
			this.Serialise(ref buf);
			int i = 0;
			foreach (var b in buf)
			{
				if (0 == (i % 16))
				{
					Debug.Write(string.Format("{0:x4}: ", i));
				}

				Debug.Write(string.Format("{0:x2} ", b));

				++i;
				if (0 == (i % 16))
				{
					Debug.WriteLine(string.Empty);
				}
				else if (0 == (i % 8))
				{
					Debug.Write("| ");
				}
			}

			Debug.WriteLine(string.Empty);
			this.Position = p;
		}

		/// <summary>
		/// The dispose.
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// The fixed.
		/// </summary>
		/// <param name="radius">
		/// The radius.
		/// </param>
		public void Fixed(ref float radius)
		{
			if (this.IsReading())
			{
				int r = 0;
				this.Int32(ref r);
				radius = (float)r / S3E.IwGeomOne;
			}
			else
			{
				int r = (int)radius * S3E.IwGeomOne;
				this.Int32(ref r);
			}
		}

		/// <summary>
		/// The float.
		/// </summary>
		/// <param name="value">
		/// The value.
		/// </param>
		[SecuritySafeCritical]
		public void Float(float value)
		{
			if (this.IsReading())
			{
				unsafe
				{
					this.ReadInfoBuffer(4);
					uint tmpBuffer = (uint)(this.buffer[0] | this.buffer[1] << 8 | this.buffer[2] << 16 | this.buffer[3] << 24);
					value = *((float*)&tmpBuffer);
				}
			}
			else
			{
				unsafe
				{
					uint TmpValue = *(uint*)&value;
					this.buffer[0] = (byte)TmpValue;
					this.buffer[1] = (byte)(TmpValue >> 8);
					this.buffer[2] = (byte)(TmpValue >> 16);
					this.buffer[3] = (byte)(TmpValue >> 24);
					this.stream.Write(this.buffer, 0, 4);
				}
			}
		}

		/// <summary>
		/// The int 16.
		/// </summary>
		/// <param name="val">
		/// The val.
		/// </param>
		public void Int16(ref short val)
		{
			if (this.mode == IwSerialiseMode.Read)
			{
				this.ReadInfoBuffer(2);
				val = (short)(this.buffer[0] | this.buffer[1] << 8);
			}
			else
			{
				this.buffer[0] = (byte)val;
				this.buffer[1] = (byte)(val >> 8);
				this.stream.Write(this.buffer, 0, 2);
			}
		}

		/// <summary>
		/// The int 32.
		/// </summary>
		/// <param name="val">
		/// The val.
		/// </param>
		public void Int32(ref int val)
		{
			if (this.mode == IwSerialiseMode.Read)
			{
				this.ReadInfoBuffer(4);
				val = this.buffer[0] | this.buffer[1] << 8 | this.buffer[2] << 16 | this.buffer[3] << 24;
			}
			else
			{
				this.buffer[0] = (byte)val;
				this.buffer[1] = (byte)(val >> 8);
				this.buffer[2] = (byte)(val >> 16);
				this.buffer[3] = (byte)(val >> 24);
				this.stream.Write(this.buffer, 0, 4);
			}
		}

		/// <summary>
		/// The int 8.
		/// </summary>
		/// <param name="val">
		/// The val.
		/// </param>
		public void Int8(ref sbyte val)
		{
			if (this.mode == IwSerialiseMode.Read)
			{
				this.ReadInfoBuffer(1);
				val = (sbyte)this.buffer[0];
			}
			else
			{
				this.buffer[0] = (byte)val;
				this.stream.Write(this.buffer, 0, 1);
			}
		}

		/// <summary>
		/// The is open.
		/// </summary>
		/// <returns>
		/// The is open.
		/// </returns>
		public bool IsOpen()
		{
			return this.stream != null;
		}

		/// <summary>
		/// The is reading.
		/// </summary>
		/// <returns>
		/// The is reading.
		/// </returns>
		public bool IsReading()
		{
			return this.mode == IwSerialiseMode.Read;
		}

		/// <summary>
		/// The is version older then.
		/// </summary>
		/// <param name="aI">
		/// The a i.
		/// </param>
		/// <param name="bI">
		/// The b i.
		/// </param>
		/// <param name="cI">
		/// The c i.
		/// </param>
		/// <returns>
		/// The is version older then.
		/// </returns>
		public bool IsVersionOlderThen(int aI, int bI, int cI)
		{
			// TODO: fix this
			return true;
		}

		/// <summary>
		/// The is writing.
		/// </summary>
		/// <returns>
		/// The is writing.
		/// </returns>
		public bool IsWriting()
		{
			return this.mode == IwSerialiseMode.Write;
		}

		/// <summary>
		/// The managed hash.
		/// </summary>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <param name="val">
		/// The val.
		/// </param>
		public void ManagedHash(uint type, ref CIwManaged val)
		{
			uint hash = 0;
			if (this.IsReading())
			{
				this.UInt32(ref hash);
				if (this.resourceResolver != null)
				{
					val = this.resourceResolver.Resolve(type, hash);
				}
			}
			else
			{
				if (val == null)
				{
					hash = 0;
				}
				else
				{
					hash = val.Hash;
				}

				this.UInt32(ref hash);
			}
		}
		public void ManagedHash<T>(ref T pObj) where T : CIwManaged
		{
			CIwManaged m = pObj;
			ManagedHash(typeof(T).Name.ToeHash(), ref m);
			pObj = (T)m;
		}
		/// <summary>
		/// The managed object.
		/// </summary>
		/// <param name="pObj">
		/// The p obj.
		/// </param>
		/// <exception cref="FormatException">
		/// </exception>
		public void ManagedObject(ref CIwManaged pObj)
		{
			if (this.mode == IwSerialiseMode.Read)
			{
				uint hash = 0;
				this.UInt32(ref hash);
				IwClassFactory factory = this.ClassRegistry.Get(hash);
				if (factory == null)
				{
					throw new FormatException(string.Format("Can't Serialise unknown type 0x{0:x}", hash));
				}

				pObj = factory.Create();
			}
			else
			{
				uint hash = pObj.Hash;
				this.UInt32(ref hash);
			}

			Debug.WriteLine(string.Format("Serialise {0}", pObj.GetType().Name));
			pObj.Serialise(this);
		}

		/// <summary>
		/// The managed object.
		/// </summary>
		/// <param name="hash">
		/// The hash.
		/// </param>
		/// <param name="pObj">
		/// The p obj.
		/// </param>
		/// <exception cref="FormatException">
		/// </exception>
		/// <exception cref="ArgumentException">
		/// </exception>
		/// <exception cref="FormatException">
		/// </exception>
		public void ManagedObject(uint hash, ref CIwManaged pObj)
		{
			if (this.mode == IwSerialiseMode.Read)
			{
				IwClassFactory factory = this.ClassRegistry.Get(hash);
				if (factory == null)
				{
					throw new FormatException(string.Format("Can't Serialise unknown type 0x{0:x}", hash));
				}

				pObj = factory.Create();
			}
			else
			{
				if (hash != pObj.Hash)
				{
					throw new ArgumentException();
				}
			}

			try
			{
				pObj.Serialise(this);
			}
			catch (Exception ex)
			{
				throw new FormatException(
					string.Format("Can't Serialise GetResHashed(0x{0:x}, \"0x{1}\")", pObj.Hash, pObj.GetType().Name), ex);
			}
		}

		/// <summary>
		/// The quat.
		/// </summary>
		/// <param name="rot">
		/// The rot.
		/// </param>
		public void Quat(ref Quaternion rot)
		{
			int w = 0;
			int x = 0;
			int y = 0;
			int z = 0;
			if (this.IsWriting())
			{
				w = (int)(rot.W * S3E.IwGeomOne);
				x = (int)(rot.X * S3E.IwGeomOne);
				y = (int)(rot.Y * S3E.IwGeomOne);
				z = (int)(rot.Z * S3E.IwGeomOne);
			}

			this.Int32(ref w);
			this.Int32(ref x);
			this.Int32(ref y);
			this.Int32(ref z);
			if (this.IsReading())
			{
				rot.W = (float)w / S3E.IwGeomOne;
				rot.X = (float)x / S3E.IwGeomOne;
				rot.Y = (float)y / S3E.IwGeomOne;
				rot.Z = (float)z / S3E.IwGeomOne;
			}
		}

		/// <summary>
		/// The s quat.
		/// </summary>
		/// <param name="rot">
		/// The rot.
		/// </param>
		public void SQuat(ref Quaternion rot)
		{
			uint unknown = 0;
			this.UInt32(ref unknown);
		}

		/// <summary>
		/// The s vec 3.
		/// </summary>
		/// <param name="pos">
		/// The pos.
		/// </param>
		public void SVec3(ref Vector3 pos)
		{
			short x = 0;
			short y = 0;
			short z = 0;
			if (this.IsWriting())
			{
				x = (short)(pos.X * S3E.IwGeomOne);
				y = (short)(pos.Y * S3E.IwGeomOne);
				z = (short)(pos.Z * S3E.IwGeomOne);
			}

			this.Int16(ref x);
			this.Int16(ref y);
			this.Int16(ref z);
			if (this.IsReading())
			{
				pos.X = (float)x / S3E.IwGeomOne;
				pos.Y = (float)y / S3E.IwGeomOne;
				pos.Z = (float)z / S3E.IwGeomOne;
			}
		}

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="data">
		/// The data.
		/// </param>
		public void Serialise(ref ushort[] data)
		{
			this.Serialise(ref data, data.Length);
		}

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="data">
		/// The data.
		/// </param>
		/// <param name="numVerts">
		/// The num verts.
		/// </param>
		public void Serialise(ref ushort[] data, int numVerts)
		{
			for (int i = 0; i < numVerts; ++i)
			{
				this.Serialise(ref data[i]);
			}
		}

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="data">
		/// The data.
		/// </param>
		public void Serialise(ref byte[] data)
		{
			for (int i = 0; i < data.Length; ++i)
			{
				this.Serialise(ref data[i]);
			}
		}

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="val">
		/// The val.
		/// </param>
		public void Serialise(ref byte val)
		{
			this.UInt8(ref val);
		}

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="val">
		/// The val.
		/// </param>
		public void Serialise(ref ushort val)
		{
			this.UInt16(ref val);
		}

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="val">
		/// The val.
		/// </param>
		public void Serialise(ref uint val)
		{
			this.UInt32(ref val);
		}

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="val">
		/// The val.
		/// </param>
		public void Serialise(ref sbyte val)
		{
			this.Int8(ref val);
		}

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="val">
		/// The val.
		/// </param>
		public void Serialise(ref short val)
		{
			this.Int16(ref val);
		}

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="val">
		/// The val.
		/// </param>
		public void Serialise(ref int val)
		{
			this.Int32(ref val);
		}

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="data">
		/// The data.
		/// </param>
		public void Serialise(ref float[] data)
		{
			for (int i = 0; i < data.Length; ++i)
			{
				this.Serialise(ref data[i]);
			}
		}

		/// <summary>
		/// The set version.
		/// </summary>
		/// <param name="major">
		/// The major.
		/// </param>
		/// <param name="minor">
		/// The minor.
		/// </param>
		/// <param name="rev">
		/// The rev.
		/// </param>
		public void SetVersion(byte major, byte minor, byte rev)
		{
			this.Major = major;
			this.Minor = minor;
			this.Revision = rev;
		}

		/// <summary>
		/// The string.
		/// </summary>
		/// <param name="s">
		/// The s.
		/// </param>
		public void String(ref string s)
		{
			if (this.IsReading())
			{
				char c = ' ';
				StringBuilder sb = new StringBuilder();
				for (;;)
				{
					this.Char(ref c);
					if (c == '\0')
					{
						s = sb.ToString();
						return;
					}

					sb.Append(c);
				}
			}
			else
			{
				var buf = Encoding.UTF8.GetBytes(s);
				this.stream.Write(buf, 0, buf.Length);
				byte zero = 0;
				this.UInt8(ref zero);
			}
		}

		/// <summary>
		/// The to string.
		/// </summary>
		/// <returns>
		/// The to string.
		/// </returns>
		public override string ToString()
		{
			if (this.stream is FileStream)
			{
				return ((FileStream)this.stream).Name;
			}

			return base.ToString();
		}

		/// <summary>
		/// The u int 16.
		/// </summary>
		/// <param name="val">
		/// The val.
		/// </param>
		public void UInt16(ref ushort val)
		{
			if (this.mode == IwSerialiseMode.Read)
			{
				this.ReadInfoBuffer(2);
				val = (ushort)(this.buffer[0] | this.buffer[1] << 8);
			}
			else
			{
				this.buffer[0] = (byte)val;
				this.buffer[1] = (byte)(val >> 8);
				this.stream.Write(this.buffer, 0, 2);
			}
		}

		/// <summary>
		/// The u int 24.
		/// </summary>
		/// <param name="val">
		/// The val.
		/// </param>
		public void UInt24(ref uint val)
		{
			if (this.mode == IwSerialiseMode.Read)
			{
				this.ReadInfoBuffer(3);
				val = (uint)(this.buffer[0] | this.buffer[1] << 8 | this.buffer[2] << 16);
			}
			else
			{
				this.buffer[0] = (byte)val;
				this.buffer[1] = (byte)(val >> 8);
				this.buffer[2] = (byte)(val >> 16);
				this.stream.Write(this.buffer, 0, 3);
			}
		}

		/// <summary>
		/// The u int 32.
		/// </summary>
		/// <param name="val">
		/// The val.
		/// </param>
		public void UInt32(ref uint val)
		{
			if (this.mode == IwSerialiseMode.Read)
			{
				this.ReadInfoBuffer(4);
				val = (uint)(this.buffer[0] | this.buffer[1] << 8 | this.buffer[2] << 16 | this.buffer[3] << 24);
			}
			else
			{
				this.buffer[0] = (byte)val;
				this.buffer[1] = (byte)(val >> 8);
				this.buffer[2] = (byte)(val >> 16);
				this.buffer[3] = (byte)(val >> 24);
				this.stream.Write(this.buffer, 0, 4);
			}
		}

		/// <summary>
		/// The u int 8.
		/// </summary>
		/// <param name="val">
		/// The val.
		/// </param>
		public void UInt8(ref byte val)
		{
			if (this.mode == IwSerialiseMode.Read)
			{
				this.ReadInfoBuffer(1);
				val = this.buffer[0];
			}
			else
			{
				this.buffer[0] = val;
				this.stream.Write(this.buffer, 0, 1);
			}
		}

		/// <summary>
		/// The vec 3.
		/// </summary>
		/// <param name="pos">
		/// The pos.
		/// </param>
		public void Vec3(ref Vector3 pos)
		{
			int x = 0;
			int y = 0;
			int z = 0;
			if (this.IsWriting())
			{
				x = (int)pos.X;
				y = (int)pos.Y;
				z = (int)pos.Z;
			}

			this.Int32(ref x);
			this.Int32(ref y);
			this.Int32(ref z);
			if (this.IsReading())
			{
				pos.X = x;
				pos.Y = y;
				pos.Z = z;
			}
		}

		/// <summary>
		/// The vec 3 fixed.
		/// </summary>
		/// <param name="pos">
		/// The pos.
		/// </param>
		public void Vec3Fixed(ref Vector3 pos)
		{
			int x = 0;
			int y = 0;
			int z = 0;
			if (this.IsWriting())
			{
				x = (int)(pos.X * S3E.IwGeomOne);
				y = (int)(pos.Y * S3E.IwGeomOne);
				z = (int)(pos.Z * S3E.IwGeomOne);
			}

			this.Int32(ref x);
			this.Int32(ref y);
			this.Int32(ref z);
			if (this.IsReading())
			{
				pos.X = (float)x / S3E.IwGeomOne;
				pos.Y = (float)y / S3E.IwGeomOne;
				pos.Z = (float)z / S3E.IwGeomOne;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// The dispose.
		/// </summary>
		/// <param name="b">
		/// The b.
		/// </param>
		protected virtual void Dispose(bool b)
		{
			if (this.isDisposed)
			{
				return;
			}

			this.isDisposed = true;

			if (this.stream != null)
			{
				this.stream.Dispose();
			}
		}

		/// <summary>
		/// The read info buffer.
		/// </summary>
		/// <param name="numBytes">
		/// The num bytes.
		/// </param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// </exception>
		protected virtual void ReadInfoBuffer(int numBytes)
		{
			if (numBytes < 0 || numBytes > this.buffer.Length)
			{
				throw new ArgumentOutOfRangeException("numBytes");
			}

			int bytesRead = 0;
			int n = 0;

			if (numBytes == 1)
			{
				n = this.stream.ReadByte();
				if (n == -1)
				{
					this.EndOfFile();
				}

				this.buffer[0] = (byte)n;
				return;
			}

			do
			{
				n = this.stream.Read(this.buffer, bytesRead, numBytes - bytesRead);
				if (n == 0)
				{
					this.EndOfFile();
				}

				bytesRead += n;
			}
			while (bytesRead < numBytes);
		}

		private void EndOfFile()
		{
			throw new EndOfStreamException();
		}

		private void LookUp()
		{
			new BinaryWriter(this.stream).Write((uint)1234);
			new BinaryReader(this.stream).ReadUInt32();
		}

		private void Serialise(ref float value)
		{
			this.Float(value);
		}

		#endregion
	}
}