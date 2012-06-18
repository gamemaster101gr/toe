using OpenTK;

using Toe.Core.Messages;

namespace Toe.Core
{
	/// <summary>
	/// Game object.
	/// </summary>
	public struct GameObject
	{
		#region Constants and Fields

		/// <summary>
		/// Max number of slots.
		/// </summary>
		public const int NumSlots = 8;

		/// <summary>
		/// Unique identifier.
		/// </summary>
		internal int Uid;

		/// <summary>
		/// Game object state.
		/// </summary>
		internal GameObjectFlag State;

		/// <summary>
		/// Next sibling in world collection.
		/// </summary>
		internal int CollectionNext;

		/// <summary>
		/// Previous sibling in world collection.
		/// </summary>
		internal int CollectionPrev;

		internal int MovedPrev;
		internal int MovedNext;

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

		internal GameComponentSlot[] Slots;

		internal GameOrigin Origin;

		public bool IsOccupied
		{
			get
			{
				return (State & GameObjectFlag.AvailabilityMask) == GameObjectFlag.Occupied;
			}
			set
			{
				State = (State & ~GameObjectFlag.AvailabilityMask) | GameObjectFlag.Occupied;
			}
		}

		public bool IsAvailable
		{
			get
			{
				return (State & GameObjectFlag.AvailabilityMask) == GameObjectFlag.Available;
			}
			set
			{
				State = (State & ~GameObjectFlag.AvailabilityMask) | GameObjectFlag.Available;
			}
		}

		public bool IsGarbage
		{
			get
			{
				return (State & GameObjectFlag.AvailabilityMask) == GameObjectFlag.Garbage;
			}
			set
			{
				State = (State & ~GameObjectFlag.AvailabilityMask) | GameObjectFlag.Garbage;
			}
		}

		public bool IsMoved
		{
			get
			{
				return (State & GameObjectFlag.Moved) == GameObjectFlag.Moved;
			}
			set
			{
				if (value)
					State |= GameObjectFlag.Moved;
				else
					State &= ~GameObjectFlag.Moved;
			}
		}

		#endregion

		#region Constructors and Destructors

		internal void Init()
		{
			this.State = GameObjectFlag.Available;
			this.CollectionPrev = 0;
			this.CollectionNext = 0;
			this.FirstChild = 0;
			this.LastChild = 0;
			this.Next = 0;
			this.Parent = 0;
			this.Prev = 0;
			this.Uid = 0;
			this.Origin.Position = Vector3.Zero;
			this.Origin.Rotation = Quaternion.Identity;
			this.Slots = new GameComponentSlot[NumSlots];
		}

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
				world.Objects[this.CollectionNext].CollectionPrev = index;
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
				world.Objects[this.CollectionPrev].CollectionNext = index;
			}
		}

		internal void AttachToMoved(int index, ref int first, ref int last, GameWorld world)
		{
			this.MovedPrev = last;
			this.MovedNext = 0;
			last = index;
			if (first == 0)
			{
				first = index;
			}
			else
			{
				world.Objects[this.MovedPrev].MovedNext = index;
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
				world.Objects[this.CollectionPrev].CollectionNext = this.CollectionNext;
			}

			if (this.CollectionNext != 0)
			{
				world.Objects[this.CollectionNext].CollectionPrev = this.CollectionPrev;
			}
		}

		#endregion

		public void DetachFromMoved(int index, ref int first, ref int last, GameWorld world)
		{
			if (last == index)
			{
				last = this.MovedPrev;
			}

			if (first == index)
			{
				first = this.MovedNext;
			}

			if (this.MovedPrev != 0)
			{
				world.Objects[this.MovedPrev].MovedNext = this.MovedNext;
			}

			if (this.MovedNext != 0)
			{
				world.Objects[this.MovedNext].MovedPrev = this.MovedPrev;
			}
		}
	}
}