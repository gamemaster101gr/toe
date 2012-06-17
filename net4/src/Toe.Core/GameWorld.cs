using System;

namespace Toe.Core
{
	/// <summary>
	/// Game world.
	/// </summary>
	public class GameWorld
	{
		#region Constants and Fields

		/// <summary>
		/// Index of the first available element.
		/// </summary>
		private int firstAvailable;

		/// <summary>
		/// Index of the fist element in garbage.
		/// </summary>
		private int firstGarbage;

		/// <summary>
		/// Index of the first occupied element.
		/// </summary>
		private int firstOccupied;

		/// <summary>
		/// Index of the last available element.
		/// </summary>
		private int lastAvailable;

		/// <summary>
		/// Index of the last element in garbage.
		/// </summary>
		private int lastGarbage;

		/// <summary>
		/// Index of the last occupied element.
		/// </summary>
		private int lastOccupied;

		internal GameObject[] objects;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="GameWorld"/> class.
		/// </summary>
		public GameWorld(int size)
		{
			this.objects = new GameObject[size];
			for (int current = 1; current < this.objects.Length; ++current)
			{
				objects[current].AttachTail(current, ref this.firstAvailable, ref this.lastAvailable, this);
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Chainging size of the game world;.
		/// </summary>
		/// <param name="numObjects">
		/// The num objects.
		/// </param>
		public void Resize(int numObjects)
		{
			int current = this.objects.Length;
			if (current >= numObjects)
			{
				return;
			}

			Array.Resize(ref this.objects, numObjects);

			while (current < numObjects)
			{
				this.MakeAvailable(current);
				++current;
			}
		}

		private void MakeAvailable(int current)
		{
			this.objects[current].AttachTail(current, ref this.firstAvailable, ref this.lastAvailable, this);
		}
		private void MakeOccupiedFromAvailable(int current)
		{
			this.objects[current].Detach(current, ref this.firstAvailable, ref this.lastAvailable, this);
			this.objects[current].AttachTail(current, ref this.firstOccupied, ref this.lastOccupied, this);
		}
		private void MakeGarbageFromOccupied(int current)
		{
			this.objects[current].Detach(current, ref this.firstOccupied, ref this.lastOccupied, this);
			this.objects[current].AttachTail(current, ref this.firstGarbage, ref this.lastGarbage, this);
		}
		private void MakeAvailableFromGarbage(int current)
		{
			this.objects[current].Detach(current, ref this.firstGarbage, ref this.lastGarbage, this);
			this.objects[current].AttachTail(current, ref this.firstAvailable, ref this.lastAvailable, this);
		}
		#endregion

		public int CreateObject()
		{
			if (firstAvailable == 0)
			{
				this.Resize(objects.Length*2);
			}

			var r = firstAvailable;
			this.MakeOccupiedFromAvailable(r);
			return r;
		}

		public void CollectGarbage()
		{
			while (firstGarbage != 0)
			{
				MakeAvailableFromGarbage(firstGarbage);
			}
		}

		public void Destroy(int index)
		{
			this.MakeGarbageFromOccupied(index);
		}
	}
}