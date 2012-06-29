using System;

namespace Toe.Marmalade.Util
{
	/// <summary>
	/// The c iw managed.
	/// </summary>
	public class CIwManaged : CIwParseable
	{
		#region Constants and Fields

		private uint hash;

		private bool isDisposed;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Finalizes an instance of the <see cref="CIwManaged"/> class. 
		/// </summary>
		~CIwManaged()
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.Dispose(true);
			}
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets Hash.
		/// </summary>
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

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>
		/// The get class name.
		/// </summary>
		/// <returns>
		/// The get class name.
		/// </returns>
		public virtual string GetClassName()
		{
			return this.GetType().Name;
		}

		/// <summary>
		/// The handle event.
		/// </summary>
		/// <param name="pEvent">
		/// The p event.
		/// </param>
		/// <returns>
		/// The handle event.
		/// </returns>
		public virtual bool HandleEvent(CIwEvent pEvent)
		{
			return false;
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
			if (0 == string.Compare(pAttrName, "name", StringComparison.InvariantCultureIgnoreCase))
			{
				pParser.ReadStringHash();
				return true;
			}

			return false;
		}

		/// <summary>
		/// The parse close.
		/// </summary>
		/// <param name="pParser">
		/// The p parser.
		/// </param>
		public override void ParseClose(CIwTextParserITX pParser)
		{
			var parent = pParser.GetObject(-1);
			if (parent != null)
			{
				parent.ParseCloseChild(pParser, this);
			}
		}

		/// <summary>
		/// The parse close child.
		/// </summary>
		/// <param name="pParser">
		/// The p parser.
		/// </param>
		/// <param name="pChild">
		/// The p child.
		/// </param>
		public virtual void ParseCloseChild(CIwTextParserITX pParser, CIwManaged pChild)
		{
		}

		/// <summary>
		/// The parse open.
		/// </summary>
		/// <param name="pParser">
		/// The p parser.
		/// </param>
		public override void ParseOpen(CIwTextParserITX pParser)
		{
		}

		/// <summary>
		/// The resolve.
		/// </summary>
		public virtual void Resolve()
		{
		}

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="serialise">
		/// The serialise.
		/// </param>
		public virtual void Serialise(IwSerialise serialise)
		{
			serialise.UInt32(ref this.hash);
		}

		/// <summary>
		/// The set name.
		/// </summary>
		/// <param name="pName">
		/// The p name.
		/// </param>
		public virtual void SetName(string pName)
		{
			this.hash = pName.ToeHash();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <param name="disposing">
		/// The disposing.
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
		}

		#endregion
	}
}