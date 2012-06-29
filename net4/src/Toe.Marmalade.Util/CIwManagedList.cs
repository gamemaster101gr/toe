namespace Toe.Marmalade.Util
{
	/// <summary>
	/// The c iw managed list.
	/// </summary>
	public class CIwManagedList
	{
		#region Constants and Fields

		private readonly CIwArray<CIwManaged> array = new CIwArray<CIwManaged>();

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets Size.
		/// </summary>
		public uint Size
		{
			get
			{
				return this.array.Size;
			}
		}

		#endregion

		#region Public Indexers

		/// <summary>
		/// The this.
		/// </summary>
		/// <param name="i">
		/// The i.
		/// </param>
		public CIwManaged this[int i]
		{
			get
			{
				return this.array[i];
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Adds a managed object pointer to the list.
		/// </summary>
		/// <param name="object">
		/// The object pointer to add.
		/// </param>
		/// <param name="allowDups">
		/// Set to true only if you wish to allow the list to contain.
		/// </param>
		public void Add(CIwResource @object, bool allowDups = false)
		{
			this.array.PushBack(@object);
		}

		/// <summary>
		/// The pop back.
		/// </summary>
		/// <returns>
		/// </returns>
		public CIwManaged PopBack()
		{
			return this.array.PopBack();
		}

		/// <summary>
		/// Interprets a list as stack: pushes item onto the top of the stack.
		/// </summary>
		/// <param name="object">
		/// The object pointer to add.
		/// </param>
		/// <param name="allowDups">
		/// Set to true only if you wish to allow the list to contain.
		/// </param>
		public void Push(CIwResource @object, bool allowDups = false)
		{
			this.array.PushBack(@object);
		}

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="serialise">
		/// The serialise.
		/// </param>
		public void Serialise(IwSerialise serialise)
		{
			this.array.SerialiseHeader(serialise);
			for (uint i = 0; i < this.array.Size; ++i)
			{
				CIwManaged m = this.array[(int)i];
				serialise.ManagedObject(ref m);
				this.array[(int)i] = m;
			}
		}

		#endregion
	}
}