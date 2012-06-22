using System.IO;

namespace Toe.Marmalade.Util
{
	/// <summary>
	/// The iw serialise.
	/// </summary>
	public static class IwSerialise
	{
		#region Constants and Fields

		private static BinaryReader reader;

		private static BinaryWriter writer;

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The bool.
		/// </summary>
		/// <param name="val">
		/// The val.
		/// </param>
		public static void Bool(ref bool val)
		{
			if (writer != null)
			{
				writer.Write(val);
			}
			else if (reader != null)
			{
				val = reader.ReadBoolean();
			}
		}

		/// <summary>
		/// The char.
		/// </summary>
		/// <param name="val">
		/// The val.
		/// </param>
		public static void Char(ref char val)
		{
			if (writer != null)
			{
				writer.Write(val);
			}
			else if (reader != null)
			{
				val = reader.ReadChar();
			}
		}

		/// <summary>
		/// The close.
		/// </summary>
		public static void Close()
		{
			if (reader != null)
			{
				reader.Close();
				reader = null;
			}

			if (writer != null)
			{
				writer.Close();
				writer = null;
			}
		}

		/// <summary>
		/// The is open.
		/// </summary>
		/// <returns>
		/// The is open.
		/// </returns>
		public static bool IsOpen()
		{
			return writer != null || reader != null;
		}

		/// <summary>
		/// The is reading.
		/// </summary>
		/// <returns>
		/// The is reading.
		/// </returns>
		public static bool IsReading()
		{
			return reader != null;
		}

		/// <summary>
		/// The is writing.
		/// </summary>
		/// <returns>
		/// The is writing.
		/// </returns>
		public static bool IsWriting()
		{
			return writer != null;
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
		// void  int (int ref var, int n=1, int numBits=sizeof(int)*8-1, int stride=sizeof(int)) 
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
	}
}