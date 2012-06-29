using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Toe.Marmalade.Util
{
	/// <summary>
	/// The c iw text parser itx.
	/// </summary>
	public class CIwTextParserITX
	{
		#region Constants and Fields

		private readonly ClassRegistry classRegistry;

		private readonly List<CIwManaged> parseStack = new List<CIwManaged>();

		private string nextString;

		private StreamReader reader;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CIwTextParserITX"/> class.
		/// </summary>
		/// <param name="classRegistry">
		/// The class registry.
		/// </param>
		public CIwTextParserITX(ClassRegistry classRegistry)
		{
			this.classRegistry = classRegistry;
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The get object.
		/// </summary>
		/// <param name="level">
		/// The level.
		/// </param>
		/// <returns>
		/// </returns>
		public CIwManaged GetObject(int level = 0)
		{
			int index = this.parseStack.Count - 1 + level;
			if (index < 0)
			{
				return null;
			}

			return this.parseStack[index];
		}

		/// <summary>
		/// The parse file.
		/// </summary>
		/// <param name="pPathname">
		/// The p pathname.
		/// </param>
		/// <param name="allowNonExist">
		/// The allow non exist.
		/// </param>
		/// <returns>
		/// The parse file.
		/// </returns>
		public virtual object ParseFile(string pPathname, bool allowNonExist = false)
		{
			if (!File.Exists(pPathname))
			{
				return null;
			}

			using (var tr = File.OpenText(pPathname))
			{
				return this.ParseFile(tr);
			}
		}

		/// <summary>
		/// The parse file.
		/// </summary>
		/// <param name="reader">
		/// The reader.
		/// </param>
		/// <returns>
		/// The parse file.
		/// </returns>
		public virtual object ParseFile(StreamReader reader)
		{
			this.reader = reader;
			this.Parse();
			return null;
		}

		/// <summary>
		/// Read the next string into a buffer and reset the read position, so ReadString will read the same string. 
		/// </summary>
		/// <returns>
		/// The peek string.
		/// </returns>
		public string PeekString()
		{
			if (this.nextString != null)
			{
				return this.nextString;
			}

			var sb = new StringBuilder();
			retry:
			int ch = -1;
			while (this.reader.Peek() != -1)
			{
				if (!char.IsWhiteSpace((char)this.reader.Peek()))
				{
					break;
				}

				this.reader.Read();
			}

			switch (this.reader.Peek())
			{
				case -1:
					break;
				case '/':
					this.reader.Read();
					if (this.reader.Peek() == '/')
					{
						this.reader.Read();
						while ((ch = this.reader.Read()) != '\n')
						{
							if (ch == -1)
							{
								break;
							}
						}

						goto retry;
					}
					else
					{
						sb.Append('/');
					}

					break;
				case '\'':
					{
						this.reader.Read();
						while ((ch = this.reader.Read()) != '\'')
						{
							if (ch == -1)
							{
								break;
							}

							sb.Append((char)ch);
						}
					}

					break;
				case '\"':
					{
						this.reader.Read();
						while ((ch = this.reader.Read()) != '\"')
						{
							if (ch == -1)
							{
								break;
							}

							sb.Append((char)ch);
						}
					}

					break;
				default:
					while (this.reader.Peek() != -1)
					{
						if (char.IsWhiteSpace((char)this.reader.Peek()))
						{
							break;
						}

						sb.Append((char)this.reader.Read());
					}

					break;
			}

			if (sb.Length > 0)
			{
				this.nextString = sb.ToString();
			}

			return this.nextString;
		}

		/// <summary>
		/// The pop object.
		/// </summary>
		/// <returns>
		/// </returns>
		public CIwManaged PopObject()
		{
			var o = this.GetObject(0);
			this.parseStack.RemoveAt(this.parseStack.Count - 1);
			return o;
		}

		/// <summary>
		/// The push object.
		/// </summary>
		/// <param name="pObject">
		/// The p object.
		/// </param>
		public void PushObject(CIwManaged pObject)
		{
			this.parseStack.Add(pObject);
		}

		/// <summary>
		/// The read string.
		/// </summary>
		/// <returns>
		/// The read string.
		/// </returns>
		public string ReadString()
		{
			var s = this.PeekString();
			this.nextString = null;
			return s;
		}

		/// <summary>
		/// The read string hash.
		/// </summary>
		/// <returns>
		/// The read string hash.
		/// </returns>
		public uint ReadStringHash()
		{
			return S3E.HashString(this.ReadString());
		}

		#endregion

		#region Methods

		private void Parse()
		{
			for (;;)
			{
				var s = this.ReadString();
				if (s == null)
				{
					break;
				}

				if (s == "include")
				{
					var fileName = this.ReadString();
					continue;
				}

				if (s == "}")
				{
					this.GetObject(0).ParseClose(this);
					this.PopObject();
					continue;
				}

				if (this.parseStack.Count > 0)
				{
					if (this.GetObject(0).ParseAttribute(this, s))
					{
						continue;
					}
				}

				if (this.PeekString() == "{")
				{
					this.ReadString();
					IwClassFactory iwClassFactory = this.classRegistry.Get(S3E.HashString(s));
					if (iwClassFactory == null)
					{
						throw new Exception(string.Format("Can't find class {0}", s));
					}

					this.PushObject(iwClassFactory.Create());
					this.GetObject(0).ParseOpen(this);
				}
				else
				{
					throw new Exception(string.Format("{0} is not an attribute in {1}", s, this.GetObject(0).GetType().Name));
				}
			}
		}

		#endregion
	}
}