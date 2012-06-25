using System.Collections.Generic;

namespace Toe.Marmalade.Util
{
	public class CIwArray<T>
	{
		List<T> list = new List<T>();

		public uint Size
		{
			get
			{
				return (uint)list.Count;
			}
		}

		public void SerialiseHeader(IwSerialise serialise)
		{
			if (serialise.IsReading())
			{
				uint l = 0;
				serialise.UInt32(ref l);
				while (list.Count < (int)l)
				{
					list.Add(default(T));
				}
				while (list.Count > (int)l)
				{
					list.RemoveAt(list.Count - 1);
				}
			}
			else
			{
				uint l = (uint)list.Count;
				serialise.UInt32(ref l);
			}
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
			list.Add(item);
		}
	}
}