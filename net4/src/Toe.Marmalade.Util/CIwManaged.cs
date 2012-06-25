using System;

namespace Toe.Marmalade.Util
{
	public class CIwManaged : CIwParseable
	{
		private uint hash;

		public uint Hash
		{
			get
			{
				return this.hash;
			}
			set
			{
				this.hash = value;
			}
		}

		public virtual string GetClassName()
		{
			return this.GetType().Name;
		}
		
		public virtual bool HandleEvent(CIwEvent pEvent)
		{
			return false;
		}
		public override bool ParseAttribute(CIwTextParserITX pParser, string pAttrName)
		{
			if (0 == string.Compare(pAttrName, "name", StringComparison.InvariantCultureIgnoreCase))
			{
				pParser.ReadStringHash();
				return true;
			}
			return false;
		}
		public override void ParseClose(CIwTextParserITX pParser)
		{
			var parent = pParser.GetObject(-1) as CIwManaged;
			if (parent != null)
				parent.ParseCloseChild(pParser, this);
		}
		public virtual void ParseCloseChild(CIwTextParserITX pParser, CIwManaged pChild)
		{
		}
		public override void ParseOpen(CIwTextParserITX pParser)
		{
		}
		public virtual void Resolve()
		{
		}
		public virtual void Serialise(IwSerialise serialise)
		{
			serialise.UInt32(ref hash);
		}
		public virtual void SetName(string pName)
		{
			hash = pName.ToeHash();
		}

	}
}