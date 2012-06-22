using System;
using System.Collections.Generic;
using System.Linq;

using Toe.Core.Messages;
using Toe.Marmalade;
using Toe.Marmalade.Util;

namespace Toe.Core
{
	/// <summary>
	/// Game world.
	/// </summary>
	public class GameWorld : IDisposable
	{
		#region Constants and Fields

		/// <summary>
		/// The graphics slot.
		/// </summary>
		public const uint GraphicsSlot = 3154702134; // "Graphics".ToeHash();

		/// <summary>
		/// Array of game Objects.
		/// </summary>
		internal GameObject[] Objects;

		private int numOfAvailable = 0;

		private readonly ClassRegistry classRegistry;

		private readonly Queue<GameComponent> componentsTrashQueue = new Queue<GameComponent>();

		private readonly IEnumerable<IGameSubsystem> gameSubsystems;

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

		private int lastMovedObject;

		/// <summary>
		/// Index of the last occupied element.
		/// </summary>
		private int lastOccupied;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="GameWorld"/> class.
		/// </summary>
		/// <param name="classRegistry">
		/// </param>
		/// <param name="gameSubsystems">
		/// </param>
		public GameWorld(ClassRegistry classRegistry, IEnumerable<IGameSubsystem> gameSubsystems)
		{
			int size = 65535;

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
				this.Destroy(this.firstOccupied, ref this.Objects[this.firstOccupied]);
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
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>
		/// Process game.
		/// </summary>
		public void ProcessGame()
		{
			while (this.messageQueue.Count > 0 || this.firstGarbage != 0 || this.firstMovedObject != 0 || this.componentsTrashQueue.Count > 0)
			{
				this.ProcessMessages();

				this.ProcessPositionChanged();

				this.CollectGarbage();

				this.DisposeComponents();
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
			throw new NotImplementedException();
			//int current = this.Objects.Length;
			//if (current >= numObjects)
			//{
			//    return;
			//}

			//Array.Resize(ref this.Objects, numObjects);

			//while (current < numObjects)
			//{
			//    this.InitGameObject(current, ref this.Objects[current]);
			//    ++current;
			//}
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
		public void DetachObject(GameObjectReference index)
		{
			if (index.IsNil)
				return;
			this.DetachObject(index, ref this.Objects[index.Index]);
		}

		private void DetachObject(GameObjectReference index, ref GameObject gameObject)
		{
			if (index.Uid != gameObject.Uid)
				return;
			if (gameObject.Parent == 0)
				return;

			// TODO: update world position by don't notify yet
			this.UpdateWorldPositionAndNotifyComponents(ref gameObject);

			gameObject.DetachNode(index.Index, this);

			gameObject.Origin.UpdateLocalPositionByWorldPosition();

			this.EnqueuePositionChanged(index.Index, ref gameObject);
		}

		public void AttachObject(GameObjectReference child,GameObjectReference parent)
		{
			if (child.IsNil || parent.IsNil)
				return;
			this.AttachObject(child, ref this.Objects[child.Index], parent, ref this.Objects[parent.Index]);
		}

		private void AttachObject(GameObjectReference child, ref GameObject childObject, GameObjectReference parent, ref GameObject parentObject)
		{
			if (child.Uid != childObject.Uid)
				return;
			if (parent.Uid != parentObject.Uid)
				return;

			// TODO: update world position by don't notify yet
			if (childObject.IsMoved)
				this.UpdateWorldPositionAndNotifyComponents(ref childObject);

			if (childObject.Parent != 0)
				childObject.DetachNode(child.Index, this);

			// TODO: update world position by don't notify yet
			if (parentObject.IsMoved)
				this.UpdateWorldPositionAndNotifyComponents(ref parentObject);

			childObject.AttachNodeTail(child.Index, parent.Index, ref parentObject, this);

			childObject.Origin.UpdateLocalPosition(parentObject.Origin);

			this.EnqueuePositionChanged(child.Index, ref childObject);
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
			while (this.firstOccupied != 0)
			{
				this.Destroy(this.firstOccupied, ref this.Objects[this.firstOccupied]);
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

		private void CollectGarbage()
		{
			// Collect garbage.
			while (this.firstGarbage != 0)
			{
				this.MakeAvailableFromGarbage(this.firstGarbage);
			}
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
			var gameComponent = (GameComponent)this.classRegistry.Get(messageArgs.DestinationComponent).Create();
			gameComponent.GameObject = messageArgs.DestinationObject;
			gameComponent.Slot = messageArgs.DestinationComponentSlot;
			gameComponentSlot.GameComponent = gameComponent;
			gameComponentSlot.Component = messageArgs.DestinationComponent;
			gameComponentSlot.Slot = messageArgs.DestinationComponentSlot;
		}

		private void Destroy(int index, ref GameObject gameObject)
		{
			if (!gameObject.IsOccupied)
			{
				return;
			}

			while (gameObject.FirstChild != 0)
			{
				this.Destroy(gameObject.FirstChild, ref this.Objects[gameObject.FirstChild]);
			}

			gameObject.DetachNode(index, this);

			for (int i = 0; i < gameObject.Slots.Length; ++i)
			{
				this.DestroyComponent(ref gameObject.Slots[i]);
			}

			this.MakeGarbageFromOccupied(index, ref gameObject);
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

		private void DisposeComponents()
		{
			while (this.componentsTrashQueue.Count > 0)
			{
				this.componentsTrashQueue.Dequeue().Dispose();
			}
		}

		////private void MakeAvailable(int current)
		////{
		////    this.Objects[current].State = GameObjectState.Available;
		////    this.Objects[current].AttachTail(current, ref this.firstAvailable, ref this.lastAvailable, this);
		////}
		private void EnqueuePositionChanged(int current, ref GameObject gameObject)
		{
			if (gameObject.IsMoved)
			{
				return;
			}

			gameObject.IsMoved = true;
			gameObject.AttachToMoved(current, ref this.firstMovedObject, ref this.lastMovedObject, this);
		}

		private void EnsureParentWorldPosition(int index, ref GameObject gameObject, ref GameObject child)
		{
			if (gameObject.IsMoved)
			{
				this.ProcessPositionChanged(index, ref gameObject);
			}

			child.Origin.UpdateWorldPosition(ref gameObject.Origin);
		}

		private void InitGameObject(int current, ref GameObject gameObject)
		{
			gameObject.AttachTail(current, ref this.firstAvailable, ref this.lastAvailable, this);
			++numOfAvailable;
			gameObject.Init();
		}

		private void MakeAvailableFromGarbage(int current)
		{
			this.MakeAvailableFromGarbage(current, ref this.Objects[current]);
		}

		private void MakeAvailableFromGarbage(int current, ref GameObject gameObject)
		{
			gameObject.IsAvailable = true;
			++numOfAvailable;
			gameObject.Detach(current, ref this.firstGarbage, ref this.lastGarbage, this);
			gameObject.AttachTail(current, ref this.firstAvailable, ref this.lastAvailable, this);
		}

		private void MakeGarbageFromOccupied(int current)
		{
			this.MakeGarbageFromOccupied(current, ref this.Objects[current]);
		}

		private void MakeGarbageFromOccupied(int current, ref GameObject gameObject)
		{
			if (gameObject.IsMoved)
			{
				gameObject.IsMoved = false;
				gameObject.DetachFromMoved(current, ref this.firstMovedObject, ref this.lastMovedObject, this);
			}
			
			gameObject.IsGarbage = true;
			gameObject.Detach(current, ref this.firstOccupied, ref this.lastOccupied, this);
			gameObject.AttachTail(current, ref this.firstGarbage, ref this.lastGarbage, this);
		}

		private void MakeOccupiedFromAvailable(int current)
		{
			this.MakeOccupiedFromAvailable(current, ref this.Objects[current]);
		}

		private void MakeOccupiedFromAvailable(int current, ref GameObject gameObject)
		{
			--numOfAvailable;
			gameObject.IsOccupied = true;
			gameObject.Detach(current, ref this.firstAvailable, ref this.lastAvailable, this);
			gameObject.AttachTail(current, ref this.firstOccupied, ref this.lastOccupied, this);
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

		private void ProcessPositionChanged()
		{
			while (this.firstMovedObject != 0)
			{
				this.ProcessPositionChanged(this.firstMovedObject, ref this.Objects[this.firstMovedObject]);
			}
		}

		private void ProcessPositionChanged(int index, ref GameObject gameObject)
		{
			gameObject.IsMoved = false;
			gameObject.DetachFromMoved(index, ref this.firstMovedObject, ref this.lastMovedObject, this);
			this.UpdateWorldPositionAndNotifyComponents(ref gameObject);
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
					this.Destroy(index, ref gameObject);
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

		private static void SendPositionChanged(ref GameObject gameObject, ref GameComponentSlot gameComponentSlot)
		{
			if (gameComponentSlot.Component != 0)
			{
				gameComponentSlot.GameComponent.OnPositionChanged(ref gameObject.Origin);
			}
		}

		private void UpdateWorldPositionAndNotifyComponents(ref GameObject gameObject)
		{
			this.UpdateWorldPosition(ref gameObject);

			for (int i = 0; i < gameObject.Slots.Length; i++)
			{
				SendPositionChanged(ref gameObject, ref gameObject.Slots[0]);
			}
		}

		private void UpdateWorldPosition(ref GameObject gameObject)
		{
			if (gameObject.Parent != 0)
			{
				this.EnsureParentWorldPosition(gameObject.Parent, ref this.Objects[gameObject.Parent], ref gameObject);
			}
			else
			{
				gameObject.Origin.WorldPosition = gameObject.Origin.Position;
				gameObject.Origin.WorldRotation = gameObject.Origin.Rotation;
			}
		}

		#endregion

		public int NumOfAvailable
		{
			get
			{
				return numOfAvailable;
			}
		}
	}
}