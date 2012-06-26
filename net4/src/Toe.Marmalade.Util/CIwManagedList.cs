using System;
using System.Collections.Generic;

namespace Toe.Marmalade.Util
{
	public class CIwManagedList
	{
		CIwArray<CIwManaged> array = new CIwArray<CIwManaged>();

		public uint Size
		{
			get
			{
				return array.Size;
			}
		}

		public void Serialise(IwSerialise serialise)
		{
			array.SerialiseHeader(serialise);
			for (uint i=0; i<array.Size; ++i)
			{
				CIwManaged m = array[(int)i];
				serialise.ManagedObject(ref m);
				array[(int)i] = m;
			}
			uint hash = 0;
			serialise.UInt32(ref hash);
			if (hash != 0)
				throw new FormatException();
		}

		/// <summary>
		/// Adds a managed object pointer to the list.
		/// </summary>
		/// <param name="object">The object pointer to add.</param>
		/// <param name="allowDups">Set to true only if you wish to allow the list to contain.</param>
		public void Add(CIwResource @object, bool allowDups = false)
		{
			array.PushBack(@object);
		}

		/// <summary>
		/// Interprets a list as stack: pushes item onto the top of the stack.
		/// </summary>
		/// <param name="object">The object pointer to add.</param>
		/// <param name="allowDups">Set to true only if you wish to allow the list to contain.</param>
		public void Push(CIwResource @object, bool allowDups = false)
		{
			array.PushBack(@object);
		}

		public CIwManaged this[int i]
		{
			get
			{
				return this.array[i];
			}
		}

		public CIwManaged PopBack()
		{
			return this.array.PopBack();
		}
	}
}