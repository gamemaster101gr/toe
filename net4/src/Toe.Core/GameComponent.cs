using System;

using Toe.Core.Messages;
using Toe.Marmalade.Util;

namespace Toe.Core
{
	/// <summary>
	/// Game object component.
	/// </summary>
	public abstract class GameComponent : CIwManaged, IDisposable
	{
		#region Constants and Fields

		private bool isDisposed;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Finalizes an instance of the <see cref="GameComponent"/> class. 
		/// </summary>
		~GameComponent()
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.Dispose(false);
			}
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets GameObject.
		/// </summary>
		public GameObjectReference GameObject { get; set; }

		/// <summary>
		/// Gets or sets Slot.
		/// </summary>
		public uint Slot { get; set; }

		#endregion

		#region Public Methods and Operators

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

			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Handle incoming message.
		/// </summary>
		/// <param name="arguments">
		/// Message argument.
		/// </param>
		public virtual void HandleMessage(MessageArgs arguments)
		{
		}

		/// <summary>
		/// The on position changed.
		/// </summary>
		/// <param name="position">
		/// The position.
		/// </param>
		public virtual void OnPositionChanged(ref GameOrigin position)
		{
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
		}

		#endregion
	}
}