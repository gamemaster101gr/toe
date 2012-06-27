using System.Collections.Generic;

namespace Toe.Marmalade.Util
{
	public class CIwArray<T>
	{
		public delegate bool IwArrayItemFunc(ref T index);
		T[] list;

		private uint size;

		private uint capacity;

		public uint Size
		{
			get
			{
				return (uint)size;
			}
		}
		public void ForEach(IwArrayItemFunc f)
		{
			if (list != null)
			for (int index = 0; index < this.Size; index++)
			{
				if (!f(ref this.list[index]))
				{
					return;
				}
			}
		}

		public void SerialiseHeader(IwSerialise serialise)
		{
			if (serialise.IsReading())
			{
				uint l = 0;
				serialise.UInt32(ref l);
				Resize(l);
			}
			else
			{
				uint l = (uint)size;
				serialise.UInt32(ref size);
			}
		}

		private void Resize(uint u)
		{
			if (u <= capacity)
			{
				size = u;
				return;
			}
			var buf = new T[u * 2];
			for (int i=0; i<size;++i)
			{
				buf[i] = list[i];
			}
			list = buf;
			size = u;
			return;
		}

		public T this[int index]
		{
			get
			{
				return list[(int)index];
			}
			set
			{
				list[(int)index] = value;
			}
		}

		public void PushBack(T item)
		{
			Resize(size + 1);
			list[size-1] = item;
		}

		public T PopBack()
		{
			var res = list[size - 1];
			this.Resize(size-1);
			return res;
		}
	}
}