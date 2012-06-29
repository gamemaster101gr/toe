namespace Toe.Marmalade.Util
{
	/// <summary>
	/// The c iw array.
	/// </summary>
	/// <typeparam name="T">
	/// </typeparam>
	public class CIwArray<T>
	{
		#region Constants and Fields

		private uint capacity;

		private T[] list;

		private uint size;

		#endregion

		#region Delegates

		/// <summary>
		/// The iw array item func.
		/// </summary>
		/// <param name="index">
		/// The index.
		/// </param>
		public delegate bool IwArrayItemFunc(ref T index);

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets Size.
		/// </summary>
		public uint Size
		{
			get
			{
				return this.size;
			}
		}

		#endregion

		#region Public Indexers

		/// <summary>
		/// The this.
		/// </summary>
		/// <param name="index">
		/// The index.
		/// </param>
		public T this[int index]
		{
			get
			{
				return this.list[index];
			}

			set
			{
				this.list[index] = value;
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The for each.
		/// </summary>
		/// <param name="f">
		/// The f.
		/// </param>
		public void ForEach(IwArrayItemFunc f)
		{
			if (this.list != null)
			{
				for (int index = 0; index < this.Size; index++)
				{
					if (!f(ref this.list[index]))
					{
						return;
					}
				}
			}
		}

		/// <summary>
		/// The pop back.
		/// </summary>
		/// <returns>
		/// </returns>
		public T PopBack()
		{
			var res = this.list[this.size - 1];
			this.Resize(this.size - 1);
			return res;
		}

		/// <summary>
		/// The push back.
		/// </summary>
		/// <param name="item">
		/// The item.
		/// </param>
		public void PushBack(T item)
		{
			this.Resize(this.size + 1);
			this.list[this.size - 1] = item;
		}

		/// <summary>
		/// The serialise header.
		/// </summary>
		/// <param name="serialise">
		/// The serialise.
		/// </param>
		public void SerialiseHeader(IwSerialise serialise)
		{
			if (serialise.IsReading())
			{
				uint l = 0;
				serialise.UInt32(ref l);
				this.Resize(l);
			}
			else
			{
				uint l = this.size;
				serialise.UInt32(ref this.size);
			}
		}

		#endregion

		#region Methods

		private void Resize(uint u)
		{
			if (u <= this.capacity)
			{
				this.size = u;
				return;
			}

			var buf = new T[u * 2];
			for (int i = 0; i < this.size; ++i)
			{
				buf[i] = this.list[i];
			}

			this.list = buf;
			this.size = u;
			return;
		}

		#endregion
	}
}