using System;
using System.Collections.Generic;
using System.Linq;

using OpenTK;

using Toe.Core.Messages;

namespace Toe.Core
{
	/// <summary>
	/// Game world.
	/// </summary>
	public class GameWorld : IDisposable
	{
		private readonly ClassRegistry classRegistry;

		private readonly IEnumerable<IGameSubsystem> gameSubsystems;

		#region Constants and Fields

		/// <summary>
		/// The graphics slot.
		/// </summary>
		public const uint GraphicsSlot = 3154702134; // "Graphics".ToeHash();

		/// <summary>
		/// Array of game Objects.
		/// </summary>
		internal GameObject[] Objects;

		private readonly Queue<GameComponent> componentsTrashQueue = new Queue<GameComponent>();

		private readonly Queue<MessageArgs> messageQueue = new Queue<MessageArgs>();

		/// <summary>
		/// Index of the first available element.
		/// </summary>
		private int firstAvailable;

		/// <summary>
		/// Index of the fist element in garbage.
		/// </summary>
		private int firstGarbage;

		private int firstMovedObject;
		private int lastMovedObject;

		/// <summary>
		/// Index of the first occupied element.
		/// </summary>
		private int firstOccupied;

		private bool isDisposed;

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

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="GameWorld"/> class.
		/// </summary>
		/// <param name="size">
		///   The size of game world - number of game object slots.
		/// </param>
		/// <param name="classRegistry"> </param>
		/// <param name="gameSubsystems"> </param>
		/// <param name="fabrics">
		/// The fabrics.
		/// </param>
		public GameWorld(int size, ClassRegistry classRegistry, IEnumerable<IGameSubsystem> gameSubsystems)
		{
			this.classRegistry = classRegistry;

			this.gameSubsystems = gameSubsystems.ToArray();

			// Zero indexed slot is reserved as special "null" value.
			this.Objects = new GameObject[size + 1];
			for (int current = 1; current < this.Objects.Length; ++current)
			{
				this.InitGameObject(current, ref this.Objects[current]);
			}
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="GameWorld"/> class. 
		/// </summary>
		~GameWorld()
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.Dispose(false);
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Create and queue message.
		/// </summary>
		/// <typeparam name="T">
		/// </typeparam>
		public T AllocateMessage<T>() where T : MessageArgs, new()
		{
			var t = new T();
			this.messageQueue.Enqueue(t);
			return t;
		}

		/// <summary>
		/// The clean.
		/// </summary>
		public void Clean()
		{
			while (this.firstOccupied != 0)
			{
				this.Destroy(
					new GameObjectReference(this.firstOccupied, this.Objects[this.firstOccupied].Uid), 
					ref this.Objects[this.firstOccupied]);
			}
		}

		/// <summary>
		/// Create new object.
		/// </summary>
		/// <returns>
		/// Index of new object.
		/// </returns>
		public GameObjectReference CreateObject()
		{
			if (this.firstAvailable == 0)
			{
				this.Resize(this.Objects.Length * 2);
			}

			var r = this.firstAvailable;
			this.MakeOccupiedFromAvailable(r);
			return new GameObjectReference(r, this.Objects[r].Uid);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.Dispose(true);
			}
		}

		/// <summary>
		/// Process game.
		/// </summary>
		public void ProcessGame()
		{
			while (this.messageQueue.Count > 0 || this.firstGarbage != 0)
			{
				this.ProcessMessages();

				this.CollectGarbage();

				this.DisposeComponents();
			}
		}
		private void ProcessPositionChanged()
		{
			while (this.firstMovedObject != 0)
			{
				ProcessPositionChanged(this.firstMovedObject, ref this.Objects[firstMovedObject]);
			}
		}

		private void ProcessPositionChanged(int index, ref GameObject gameObject)
		{
			gameObject.IsMoved = false;
			gameObject.DetachFromMoved(index, ref this.firstMovedObject, ref this.lastMovedObject, this);
			this.UpdateWorldPosition(this, ref gameObject);
		}

		private void UpdateWorldPosition(GameWorld gameWorld, ref GameObject gameObject)
		{
			if (gameObject.Parent != 0) 
				this.EnsureParentWorldPosition(gameObject.Parent, ref this.Objects[gameObject.Parent], ref gameObject);
			else
			{
				gameObject.Origin.WorldPosition = gameObject.Origin.Position;
				gameObject.Origin.WorldRotation = gameObject.Origin.Rotation;
			}
			for (int i = 0; i < gameObject.Slots.Length; i++)
			{
				SendPositionChanged(ref gameObject, ref gameObject.Slots[0]);
			}
		}

		private void SendPositionChanged(ref GameObject gameObject, ref GameComponentSlot gameComponentSlot)
		{
			if (gameComponentSlot.Component != 0) gameComponentSlot.GameComponent.OnPositionChanged(ref	gameObject.Origin);
		}

		private void EnsureParentWorldPosition(int index, ref GameObject gameObject, ref GameObject child)
		{
			if (gameObject.IsMoved) this.ProcessPositionChanged(index, ref gameObject);

			Vector3.Transform(ref child.Origin.Position, ref gameObject.Origin.WorldRotation, out child.Origin.WorldPosition);
			// TODO: check if the order is right
			Quaternion.Multiply(ref child.Origin.Rotation, ref gameObject.Origin.WorldRotation, out child.Origin.Rotation);
		}

		private void ProcessMessages()
		{
			while (this.messageQueue.Count > 0)
			{
				var msg = this.messageQueue.Dequeue();
				if (msg.DestinationObject.IsNil)
				{
					this.BroadcastGlobalMessage(msg);
				}
				else
				{
					this.BroadcastLocalMessage(msg.DestinationObject.Index, ref this.Objects[msg.DestinationObject.Index], msg);
				}
			}
		}

		private void DisposeComponents()
		{
			while (this.componentsTrashQueue.Count > 0)
			{
				this.componentsTrashQueue.Dequeue().Dispose();
			}
		}

		private void CollectGarbage()
		{
			// Collect garbage.
			while (this.firstGarbage != 0)
			{
				this.MakeAvailableFromGarbage(this.firstGarbage);
			}
		}

		/// <summary>
		/// Chainging size of the game world;.
		/// </summary>
		/// <param name="numObjects">
		/// The num Objects.
		/// </param>
		public void Resize(int numObjects)
		{
			int current = this.Objects.Length;
			if (current >= numObjects)
			{
				return;
			}

			Array.Resize(ref this.Objects, numObjects);

			while (current < numObjects)
			{
				this.InitGameObject(current, ref this.Objects[current]);
				++current;
			}
		}

		/// <summary>
		/// The send message to object.
		/// </summary>
		/// <param name="index">
		/// The index.
		/// </param>
		/// <typeparam name="T">
		/// </typeparam>
		/// <returns>
		/// </returns>
		public T SendMessageToObject<T>(GameObjectReference index) where T : MessageArgs, new()
		{
			var msg = this.AllocateMessage<T>();
			msg.MessageNameHash = typeof(T).Name.ToeHash();
			msg.MessageBroadcasting = MessageBroadcasting.None;
			msg.DestinationObject = index;
			return msg;
		}

		/// <summary>
		/// The send message to object component at slot.
		/// </summary>
		/// <param name="index">
		/// The index.
		/// </param>
		/// <param name="slot">
		/// The slot.
		/// </param>
		/// <param name="component">
		/// The component.
		/// </param>
		/// <typeparam name="T">
		/// </typeparam>
		/// <returns>
		/// </returns>
		public T SendMessageToObjectComponentAtSlot<T>(GameObjectReference index, uint slot, uint component)
			where T : MessageArgs, new()
		{
			var msg = this.AllocateMessage<T>();
			msg.MessageNameHash = typeof(T).Name.ToeHash();
			msg.MessageBroadcasting = MessageBroadcasting.None;
			msg.DestinationObject = index;
			msg.DestinationComponentSlot = slot;
			msg.DestinationComponent = component;
			return msg;
		}

		#endregion

		#region Methods

		/// <summary>
		/// The dispose.
		/// </summary>
		/// <param name="disposing">
		/// The disposing.
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
			while (firstOccupied != 0)
			{
				this.Destroy(new GameObjectReference(firstOccupied, this.Objects[firstOccupied].Uid), ref this.Objects[firstOccupied]);
			}

			this.CollectGarbage();
			this.DisposeComponents();
		}

		private static bool SendMessageToComponent(ref GameComponentSlot slot, MessageArgs msg)
		{
			if (slot.Component == 0)
			{
				return false;
			}

			slot.GameComponent.HandleMessage(msg);
			return msg.IsHandled;
		}

		private void BroadcastGlobalMessage(MessageArgs msg)
		{
			int i = this.firstOccupied;
			while (i != 0)
			{
				if (this.SendMessage(i, ref this.Objects[i], msg))
				{
					return;
				}
			}

			throw new NotImplementedException();
		}

		private bool BroadcastLocalMessage(int index, ref GameObject gameObject, MessageArgs msg)
		{
			if (gameObject.Uid != msg.DestinationObject.Uid)
			{
				return msg.IsHandled;
			}

			this.SendMessage(index, ref gameObject, msg);
			if (msg.IsHandled)
			{
				return msg.IsHandled;
			}

			if (msg.MessageBroadcasting == MessageBroadcasting.None)
			{
				return msg.IsHandled;
			}

			if (msg.MessageBroadcasting == MessageBroadcasting.Up)
			{
				if (gameObject.Parent == 0)
				{
					return msg.IsHandled;
				}

				return this.BroadcastLocalMessage(gameObject.Parent, ref this.Objects[gameObject.Parent], msg);
			}

			int currentChild = gameObject.FirstChild;
			while (currentChild != 0)
			{
				int nextChild = this.Objects[currentChild].Next;
				this.BroadcastLocalMessage(currentChild, ref this.Objects[currentChild], msg);
				if (msg.IsHandled)
				{
					return msg.IsHandled;
				}

				currentChild = nextChild;
			}

			return msg.IsHandled;
		}

		private bool CheckSlotAndDestroyIfMatch(ref GameComponentSlot gameComponentSlot, MessageArgs messageArgs)
		{
			if (gameComponentSlot.Component == 0)
			{
				return false;
			}

			if (gameComponentSlot.Slot != messageArgs.DestinationComponentSlot)
			{
				return false;
			}

			if (messageArgs.DestinationComponent != 0 && gameComponentSlot.Component != messageArgs.DestinationComponent)
			{
				return false;
			}

			this.DestroyComponent(ref gameComponentSlot);
			return true;
		}

		private void CreateComponent(MessageArgs msg)
		{
			msg.IsHandled = true;
			this.CreateComponent(ref this.Objects[msg.DestinationObject.Index], msg);
		}

		private void CreateComponent(ref GameObject gameObject, MessageArgs messageArgs)
		{
			for (int index = 0; index < gameObject.Slots.Length; index++)
			{
				if (this.CheckSlotAndDestroyIfMatch(ref gameObject.Slots[index], messageArgs))
				{
					this.CreateComponentAtSlot(ref gameObject.Slots[index], messageArgs);
					return;
				}
			}

			for (int index = 0; index < gameObject.Slots.Length; index++)
			{
				if (gameObject.Slots[index].Component == 0)
				{
					this.CreateComponentAtSlot(ref gameObject.Slots[index], messageArgs);
					return;
				}
			}

			throw new IndexOutOfRangeException("All component slots are occupied");
		}

		private void CreateComponentAtSlot(ref GameComponentSlot gameComponentSlot, MessageArgs messageArgs)
		{
			var gameComponent = (GameComponent)this.classRegistry.CreateInstance(messageArgs.DestinationComponent);
			gameComponent.GameObject = messageArgs.DestinationObject;
			gameComponent.Slot = messageArgs.DestinationComponentSlot;
			gameComponentSlot.GameComponent = gameComponent;
			gameComponentSlot.Component = messageArgs.DestinationComponent;
			gameComponentSlot.Slot = messageArgs.DestinationComponentSlot;
		}

		private void Destroy(GameObjectReference index, ref GameObject gameObject)
		{
			if (!gameObject.IsOccupied)
			{
				return;
			}

			for (int i = 0; i < gameObject.Slots.Length; ++i)
			{
				this.DestroyComponent(ref gameObject.Slots[i]);
			}

			this.MakeGarbageFromOccupied(index.Index);
			++gameObject.Uid;
		}

		private void DestroyComponent(MessageArgs msg)
		{
			msg.IsHandled = true;
			this.DestroyComponent(ref this.Objects[msg.DestinationObject.Index], msg);
		}

		private void DestroyComponent(ref GameComponentSlot gameComponentSlot)
		{
			if (gameComponentSlot.Component == 0)
			{
				return;
			}

			this.componentsTrashQueue.Enqueue(gameComponentSlot.GameComponent);
			gameComponentSlot.GameComponent = null;
			gameComponentSlot.Component = 0;
			gameComponentSlot.Slot = 0;
		}

		private void DestroyComponent(ref GameObject gameObject, MessageArgs messageArgs)
		{
			for (int index = 0; index < gameObject.Slots.Length; index++)
			{
				if (this.CheckSlotAndDestroyIfMatch(ref gameObject.Slots[index], messageArgs))
				{
					return;
				}
			}
		}

		private void InitGameObject(int current, ref GameObject gameObject)
		{
			gameObject.AttachTail(current, ref this.firstAvailable, ref this.lastAvailable, this);
			gameObject.Init();
		}

		////private void MakeAvailable(int current)
		////{
		////    this.Objects[current].State = GameObjectState.Available;
		////    this.Objects[current].AttachTail(current, ref this.firstAvailable, ref this.lastAvailable, this);
		////}

		private void EnqueuePositionChanged(int current, ref GameObject gameObject)
		{
			if (gameObject.IsMoved)
				return;
			gameObject.IsMoved = true;
			gameObject.AttachToMoved(current, ref firstMovedObject, ref lastMovedObject, this);
		}

		private void MakeAvailableFromGarbage(int current)
		{
			this.Objects[current].IsAvailable = true;
			this.Objects[current].Detach(current, ref this.firstGarbage, ref this.lastGarbage, this);
			this.Objects[current].AttachTail(current, ref this.firstAvailable, ref this.lastAvailable, this);
		}

		private void MakeGarbageFromOccupied(int current)
		{
			this.Objects[current].IsGarbage = true;
			this.Objects[current].Detach(current, ref this.firstOccupied, ref this.lastOccupied, this);
			this.Objects[current].AttachTail(current, ref this.firstGarbage, ref this.lastGarbage, this);
		}

		private void MakeOccupiedFromAvailable(int current)
		{
			this.Objects[current].IsOccupied = true;
			this.Objects[current].Detach(current, ref this.firstAvailable, ref this.lastAvailable, this);
			this.Objects[current].AttachTail(current, ref this.firstOccupied, ref this.lastOccupied, this);
		}

		private bool SendMessage(int index, ref GameObject gameObject, MessageArgs msg)
		{
			if (!gameObject.IsOccupied)
			{
				return false;
			}

			switch (msg.MessageNameHash)
			{
				case WellKnownMessages.DestroyComponent:
					this.DestroyComponent(msg);
					return true;
			}

			if (msg.DestinationComponentSlot != 0 && msg.DestinationComponent != 0)
			{
				switch (msg.MessageNameHash)
				{
					case WellKnownMessages.CreateComponent:
						this.CreateComponent(msg);
						return true;
				}

				for (int slot = 0; slot < GameObject.NumSlots; ++slot)
				{
					if (gameObject.Slots[slot].Slot == msg.DestinationComponentSlot
					    && gameObject.Slots[slot].Component == msg.DestinationComponent)
					{
						gameObject.Slots[slot].GameComponent.HandleMessage(msg);
						if (msg.IsHandled)
						{
							return true;
						}
					}
				}

				return false;
			}

			if (msg.DestinationComponentSlot != 0)
			{
				for (int slot = 0; slot < GameObject.NumSlots; ++slot)
				{
					if (gameObject.Slots[slot].Slot == msg.DestinationComponentSlot)
					{
						gameObject.Slots[slot].GameComponent.HandleMessage(msg);
						if (msg.IsHandled)
						{
							return true;
						}
					}
				}

				return false;
			}

			if (msg.DestinationComponent != 0)
			{
				for (int slot = 0; slot < GameObject.NumSlots; ++slot)
				{
					if (gameObject.Slots[slot].Component == msg.DestinationComponent)
					{
						gameObject.Slots[slot].GameComponent.HandleMessage(msg);
						if (msg.IsHandled)
						{
							return true;
						}
					}
				}

				return false;
			}

			switch (msg.MessageNameHash)
			{
				case WellKnownMessages.DestroyObject:
					this.Destroy(new GameObjectReference(index, gameObject.Uid), ref gameObject);
					msg.IsHandled = true;
					return true;
				case WellKnownMessages.SetPosition:
					gameObject.Origin.Position = ((SetPosition)msg).Position;
					this.EnqueuePositionChanged(index, ref gameObject);
					msg.IsHandled = true;
					return true;
				case WellKnownMessages.SetRotation:
					gameObject.Origin.Rotation = ((SetRotation)msg).Rotation;
					this.EnqueuePositionChanged(index, ref gameObject);
					msg.IsHandled = true;
					return true;
			}

			for (int slot = 0; slot < GameObject.NumSlots; ++slot)
			{
				if (SendMessageToComponent(ref gameObject.Slots[slot], msg))
				{
					return true;
				}
			}

			return false;
		}

		#endregion
	}
}