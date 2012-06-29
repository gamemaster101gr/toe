using OpenTK;

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
		/// Next sibling in world collection.
		/// </summary>
		internal int CollectionNext;

		/// <summary>
		/// Previous sibling in world collection.
		/// </summary>
		internal int CollectionPrev;

		/// <summary>
		/// First child.
		/// </summary>
		internal int FirstChild;

		/// <summary>
		/// Last child.
		/// </summary>
		internal int LastChild;

		internal int MovedNext;

		internal int MovedPrev;

		/// <summary>
		/// Next sibling.
		/// </summary>
		internal int Next;

		internal GameOrigin Origin;

		/// <summary>
		/// Parent game object.
		/// </summary>
		internal int Parent;

		/// <summary>
		/// Previous sibling.
		/// </summary>
		internal int Prev;

		internal GameComponentSlot[] Slots;

		/// <summary>
		/// Game object state.
		/// </summary>
		internal GameObjectFlags State;

		/// <summary>
		/// Unique identifier.
		/// </summary>
		internal int Uid;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets a value indicating whether IsAvailable.
		/// </summary>
		public bool IsAvailable
		{
			get
			{
				return (this.State & GameObjectFlags.AvailabilityMask) == GameObjectFlags.Available;
			}

			set
			{
				this.State = (this.State & ~GameObjectFlags.AvailabilityMask) | GameObjectFlags.Available;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether IsGarbage.
		/// </summary>
		public bool IsGarbage
		{
			get
			{
				return (this.State & GameObjectFlags.AvailabilityMask) == GameObjectFlags.Garbage;
			}

			set
			{
				this.State = (this.State & ~GameObjectFlags.AvailabilityMask) | GameObjectFlags.Garbage;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether IsMoved.
		/// </summary>
		public bool IsMoved
		{
			get
			{
				return (this.State & GameObjectFlags.Moved) == GameObjectFlags.Moved;
			}

			set
			{
				if (value)
				{
					this.State |= GameObjectFlags.Moved;
				}
				else
				{
					this.State &= ~GameObjectFlags.Moved;
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether IsOccupied.
		/// </summary>
		public bool IsOccupied
		{
			get
			{
				return (this.State & GameObjectFlags.AvailabilityMask) == GameObjectFlags.Occupied;
			}

			set
			{
				this.State = (this.State & ~GameObjectFlags.AvailabilityMask) | GameObjectFlags.Occupied;
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The attach node tail.
		/// </summary>
		/// <param name="index">
		/// The index.
		/// </param>
		/// <param name="parent">
		/// The parent.
		/// </param>
		/// <param name="parentObject">
		/// The parent object.
		/// </param>
		/// <param name="world">
		/// The world.
		/// </param>
		public void AttachNodeTail(int index, int parent, ref GameObject parentObject, GameWorld world)
		{
			this.Parent = parent;
			this.Prev = parentObject.LastChild;
			this.Next = 0;
			parentObject.LastChild = index;
			if (parentObject.FirstChild == 0)
			{
				parentObject.FirstChild = index;
			}
			else
			{
				world.Objects[this.Prev].Next = index;
			}
		}

		/// <summary>
		/// The detach from moved.
		/// </summary>
		/// <param name="index">
		/// The index.
		/// </param>
		/// <param name="first">
		/// The first.
		/// </param>
		/// <param name="last">
		/// The last.
		/// </param>
		/// <param name="world">
		/// The world.
		/// </param>
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

		/// <summary>
		/// The detach node.
		/// </summary>
		/// <param name="index">
		/// The index.
		/// </param>
		/// <param name="gameWorld">
		/// The game world.
		/// </param>
		public void DetachNode(int index, GameWorld gameWorld)
		{
			if (this.Parent == 0)
			{
				return;
			}

			this.DetachNode(index, gameWorld, ref gameWorld.Objects[this.Parent]);
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

		internal void Init()
		{
			this.State = GameObjectFlags.Available;
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

		private void DetachNode(int index, GameWorld gameWorld, ref GameObject parent)
		{
			if (this.Next != 0)
			{
				gameWorld.Objects[this.Next].Prev = this.Prev;
			}

			if (this.Prev != 0)
			{
				gameWorld.Objects[this.Prev].Next = this.Next;
			}

			if (parent.FirstChild == index)
			{
				parent.FirstChild = this.Next;
			}

			if (parent.LastChild == index)
			{
				parent.LastChild = this.Prev;
			}

			this.Parent = 0;
			this.Next = 0;
			this.Prev = 0;
		}

		#endregion
	}
}