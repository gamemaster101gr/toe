using System;

using OpenTK;

using Toe.Core.Messages;

namespace Toe.Core
{
	/// <summary>
	/// Game object component.
	/// </summary>
	public abstract class GameComponent: IDisposable
	{
		private bool isDisposed = false;

		~GameComponent()
		{
			if (!isDisposed)
			{
				isDisposed = true;
				this.Dispose(false);
			}
		}

		public GameObjectReference GameObject { get; set; }

		public uint Slot { get; set; }


		protected virtual void Dispose(bool disposing)
		{
			
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

		#region Implementation of IDisposable

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
			if (!isDisposed)
			{
				isDisposed = true;
				this.Dispose(true);
			}
			GC.SuppressFinalize(this);
		}

		#endregion

		public virtual void OnPositionChanged(ref GameOrigin position)
		{
			
		}
	}
}