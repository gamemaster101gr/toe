using System;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.ResManager
{
	public class CIwResGroup:CIwManaged
	{
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
			}
		}

		private void ReadResGroupMembers(IwSerialise iwSerialise)
		{
			string name = String.Empty;
			iwSerialise.String(ref name);

			uint unknown = 0;
			iwSerialise.UInt32(ref unknown);
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