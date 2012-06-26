using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

		private readonly IwSerialiseMode mode;

		private readonly ClassRegistry classRegistry;

		private readonly IResourceResolver resourceResolver;

		private readonly Stream stream;

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
		public IwSerialise(Stream stream, IwSerialiseMode mode, ClassRegistry classRegistry, IResourceResolver resourceResolver)
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

		public long Position
		{
			get
			{
				return stream.Position;
			}
			set
			{
				stream.Position = value;
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
		/// <param name="ram">
		/// The ram.
		/// </param>
		/// <returns>
		/// </returns>
		public static IwSerialise Open(string filePath, bool read, ClassRegistry classRegistry, IResourceResolver resourceResolver, bool ram = false)
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
		/// <param name="ram">
		/// The ram.
		/// </param>
		/// <returns>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// </exception>
		public static IwSerialise Open(string filePath, IwSerialiseMode mode, ClassRegistry classRegistry, IResourceResolver resourceResolver, bool ram = false)
		{
			switch (mode)
			{
				case IwSerialiseMode.Read:
					return new IwSerialise(File.OpenRead(resourceResolver.ResolveFilePath(filePath, ram)), mode, classRegistry, resourceResolver);
				case IwSerialiseMode.Write:
					return new IwSerialise(File.OpenWrite(resourceResolver.ResolveFilePath(filePath, ram)), mode, classRegistry, resourceResolver);
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

		private void LookUp()
		{
			
				new BinaryWriter(stream).Write((uint)1234);
				new BinaryReader(stream).ReadUInt32();
		}

		public void ManagedObject  (ref CIwManaged pObj)
		{
			if (this.mode == IwSerialiseMode.Read)
			{
				uint hash = 0;
				this.UInt32(ref hash);
				pObj = this.classRegistry.Get(hash).Create();
			}
			else
			{
				uint hash = pObj.Hash;
				this.UInt32(ref hash);
			}

			pObj.Serialise(this);
		}

		public void ManagedObject(UInt32 hash, ref CIwManaged pObj)
		{
			if (this.mode == IwSerialiseMode.Read)
			{
				IwClassFactory factory = this.classRegistry.Get(hash);
				pObj = factory.Create();
			}
			else
			{
				if (hash != pObj.Hash)
					throw new ArgumentException();
			}

			pObj.Serialise(this);
		}

		public void Int32(ref int val)
		{
			if (this.mode == IwSerialiseMode.Read)
			{ 
				this.ReadInfoBuffer(4);
				val = (int)(buffer[0] | buffer[1] << 8 | buffer[2] << 16 | buffer[3] << 24);
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

		public void UInt32(ref uint val)
		{
			if (this.mode == IwSerialiseMode.Read)
			{
				this.ReadInfoBuffer(4);
				val = (uint)(buffer[0] | buffer[1] << 8 | buffer[2] << 16 | buffer[3] << 24); 
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

		public void UInt16(ref ushort val)
		{
			if (this.mode == IwSerialiseMode.Read)
			{
				this.ReadInfoBuffer(2);
				val = (ushort)(buffer[0] | buffer[1] << 8);
			}
			else
			{
				this.buffer[0] = (byte)val;
				this.buffer[1] = (byte)(val >> 8);
				this.stream.Write(this.buffer, 0, 2);
			}
		}
		public void Int16(ref short val)
		{
			if (this.mode == IwSerialiseMode.Read)
			{
				this.ReadInfoBuffer(2);
				val = (short)(buffer[0] | buffer[1] << 8);
			}
			else
			{
				this.buffer[0] = (byte)val;
				this.buffer[1] = (byte)(val >> 8);
				this.stream.Write(this.buffer, 0, 2);
			}
		}
		public void UInt8(ref byte val)
		{
			if (this.mode == IwSerialiseMode.Read)
			{
				this.ReadInfoBuffer(1);
				val = (byte)(buffer[0]);
			}
			else
			{
				this.buffer[0] = (byte)val;
				this.stream.Write(this.buffer, 0, 1);
			}
		}
		public void Int8(ref sbyte val)
		{
			if (this.mode == IwSerialiseMode.Read)
			{
				this.ReadInfoBuffer(1);
				val = (sbyte)(buffer[0]);
			}
			else
			{
				this.buffer[0] = (byte)val;
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
				val = (char)(buffer[0]);
			}
			else
			{
				var l = Encoding.UTF8.GetBytes(new char[] { val }, 0, 1, buffer, 0);
				this.stream.Write(this.buffer, 0, l);
			}
		}

		/// <summary>
		/// The close.
		/// </summary>
		public void Close()
		{
			stream.Close();
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
		/// The is writing.
		/// </summary>
		/// <returns>
		/// The is writing.
		/// </returns>
		public bool IsWriting()
		{
			return this.mode == IwSerialiseMode.Write;
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
			throw new System.IO.EndOfStreamException();
		}

		#endregion

		// void  CharBitDepthRequired (char ref var, int n=1, int stride=sizeof(char)) 
		// void  Double (double ref var, int n=1, int numBits=sizeof(double)*8, int stride=sizeof(double)) 
		// void  Enum (void *pVar) 
		// bool  EOF () 
		// bool  Exists (const char *filename, bool ram=false) 
		////void  File (CIwTextParserITX *pParser, char const *filename, void *pptr, bool read) 
		////void  File (CIwTextParserITX *pParser, char const *filename, void *pptr, Mode mode) 
		// void  Float (float ref var, int n=1, int numBits=sizeof(float)*8, int stride=sizeof(float)) 
		// ushort  GetUserVersion () 
		// void  short (short ref var, int n=1, int numBits=sizeof(short)*8-1, int stride=sizeof(short)) 
		// void  shortBitDepthRequired (short ref var, int n=1, int stride=sizeof(short)) 
	

		// void  intBitDepthRequired (int ref var, int n=1, int stride=sizeof(int)) 
		// void  long (long ref var, int n=1, int numBits=sizeof(long)*8-1, int stride=sizeof(long)) 
		// void  sbyte (sbyte ref var, int n=1, int numBits=sizeof(sbyte)*8-1, int stride=sizeof(sbyte)) 
		// void  sbyteBitDepthRequired (sbyte ref var, int n=1, int stride=sizeof(sbyte)) 
		// void  ManagedHash (void *pptr) 
		// void  ManagedObject (CIwManaged *ref pObj) 
		// void  MappedData (const ushort *pMap, void *_pData, int numStructs, int stride) 
		// void  Open (const char *filename, bool read, bool ram=false) 
		// void  Open (const char *filename, Mode mode, bool ram=false) 
		// void  OpenFromMemory (void *pBuffer, int size, bool read) 
		// void  OpenFromMemory (void *pBuffer, int size, Mode mode) 
		// void  ResetCallbackCount () 
		// void  SetCallback (UserCallback cb) 
		// void  SetCallbackPeriod (uint p) 
		// void  SetUserVersion (ushort v) 
		// void  String (char *text, int maxLen=0) 
		// void  ushort (ushort ref var, int n=1, int numBits=sizeof(ushort)*8, int stride=sizeof(ushort)) 
		// void  ushortBitDepthRequired (ushort ref var, int n=1, int stride=sizeof(ushort)) 
		// void  Uint (uint ref var, int n=1, int numBits=sizeof(uint)*8, int stride=sizeof(uint)) 
		// void  UintBitDepthRequired (uint ref var, int n=1, int stride=sizeof(uint)) 
		// void  Ulong (ulong ref var, int n=1, int numBits=sizeof(ulong)*8, int stride=sizeof(ulong)) 
		// void  byte (byte ref var, int n=1, int numBits=sizeof(byte)*8, int stride=sizeof(byte)) 
		// void  byteBitDepthRequired (byte ref var, int n=1, int stride=sizeof(byte)) 
		public void SQuat(ref Quaternion rot)
		{
			uint unknown=0;
			this.UInt32(ref unknown);
		}

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
			Int32(ref w);
			Int32(ref x);
			Int32(ref y);
			Int32(ref z);
			if (this.IsReading())
			{
				rot.W = (float)w / S3E.IwGeomOne;
				rot.X = (float)x / S3E.IwGeomOne;
				rot.Y = (float)y / S3E.IwGeomOne;
				rot.Z = (float)z / S3E.IwGeomOne;
			}
		}

		public void SVec3(ref Vector3 pos)
		{
			short x=0;
			short y = 0;
			short z = 0;
			if (this.IsWriting())
			{
				x = (short)(pos.X * S3E.IwGeomOne);
				y = (short)(pos.Y * S3E.IwGeomOne);
				z = (short)(pos.Z * S3E.IwGeomOne);
			}
			Int16(ref x);
			Int16(ref y);
			Int16(ref z);
			if (this.IsReading())
			{
				pos.X = (float)x / S3E.IwGeomOne;
				pos.Y = (float)y / S3E.IwGeomOne;
				pos.Z = (float)z / S3E.IwGeomOne;
			}
		}

		public void String(ref string s)
		{
			if (this.IsReading())
			{
				char c = ' ';
				StringBuilder sb = new StringBuilder();
				for (; ; )
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
				stream.Write(buf,0,buf.Length);
				byte zero = 0;
				this.UInt8(ref zero);
			}
		}

		public void Serialise(ref ushort[] data)
		{
			for (int i = 0; i < data.Length; ++i) this.Serialise(ref data[i]);
		}
		public void Serialise(ref byte[] data)
		{
			for (int i = 0; i < data.Length; ++i) this.Serialise(ref data[i]);
		}

		public void Serialise(ref byte val)
		{
			this.UInt8(ref val);
		}
		public void Serialise(ref ushort val)
		{
			this.UInt16(ref val);
		}
		public void Serialise(ref uint val)
		{
			this.UInt32(ref val);
		}
		public void Serialise(ref sbyte val)
		{
			this.Int8(ref val);
		}
		public void Serialise(ref short val)
		{
			this.Int16(ref val);
		}
		public void Serialise(ref int val)
		{
			this.Int32(ref val);
		}

		public void ManagedHash(UInt32 type, ref CIwManaged val)
		{
			uint hash = 0;
			if (this.IsReading())
			{
				this.UInt32(ref hash);
				if (resourceResolver != null)
					val = resourceResolver.Resolve(type, hash);
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

		public void Colour(ref Color color)
		{
			if (this.IsReading())
			{
				byte a=0,r=0,g=0,b=0;
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
			Int32(ref x);
			Int32(ref y);
			Int32(ref z);
			if (this.IsReading())
			{
				pos.X = (float)x / S3E.IwGeomOne;
				pos.Y = (float)y / S3E.IwGeomOne;
				pos.Z = (float)z / S3E.IwGeomOne;
			}
		}
		public void Vec3(ref Vector3 pos)
		{
			int x = 0;
			int y = 0;
			int z = 0;
			if (this.IsWriting())
			{
				x = (int)(pos.X);
				y = (int)(pos.Y);
				z = (int)(pos.Z);
			}
			Int32(ref x);
			Int32(ref y);
			Int32(ref z);
			if (this.IsReading())
			{
				pos.X = (float)x;
				pos.Y = (float)y;
				pos.Z = (float)z;
			}
		}

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
				int r = (int)radius*S3E.IwGeomOne;
				this.Int32(ref r);
			}
		}

	}
}