namespace Toe.Core
{
	/// <summary>
	/// Game object.
	/// </summary>
	public struct GameObject
	{
		#region Constants and Fields

		/// <summary>
		/// Previous sibling in world collection.
		/// </summary>
		internal int CollectionPrev;

		/// <summary>
		/// Next sibling in world collection.
		/// </summary>
		internal int CollectionNext;

		/// <summary>
		/// First child.
		/// </summary>
		internal int FirstChild;

		/// <summary>
		/// Last child.
		/// </summary>
		internal int LastChild;

		/// <summary>
		/// Next sibling.
		/// </summary>
		internal int Next;

		/// <summary>
		/// Parent game object.
		/// </summary>
		internal int Parent;

		/// <summary>
		/// Previous sibling.
		/// </summary>
		internal int Prev;

		#endregion

		#region Methods

		internal void AttachHead(int index, ref int first, ref int last, GameWorld world)
		{
			this.CollectionPrev = 0;
			this.CollectionNext = first;
			first = index;
			if (last == 0)
			{
				last = index;
			}
			else
			{
				world.objects[this.CollectionNext].CollectionPrev = index;
			}
		}

		internal void AttachTail(int index, ref int first, ref int last, GameWorld world)
		{
			this.CollectionPrev = last;
			this.CollectionNext = 0;
			last = index;
			if (first == 0)
			{
				first = index;
			}
			else
			{
				world.objects[this.CollectionPrev].CollectionNext = index;
			}
		}

		internal void Detach(int index, ref int first, ref int last, GameWorld world)
		{
			if (last == index)
			{
				last = this.CollectionPrev;
			}

			if (first == index)
			{
				first = this.CollectionNext;
			}

			if (this.CollectionPrev != 0)
			{
				world.objects[this.CollectionPrev].CollectionNext = this.CollectionNext;
			}

			if (this.CollectionNext != 0)
			{
				world.objects[this.CollectionNext].CollectionPrev = this.CollectionPrev;
			}
		}

		#endregion
	}
}