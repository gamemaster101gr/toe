namespace Toe.Core.Messages
{
	/// <summary>
	/// Message arguments basic class.
	/// </summary>
	public class MessageArgs
	{
		/// <summary>
		/// Message identifier.
		/// </summary>
		public uint MessageNameHash { get; set; }

		/// <summary>
		/// Source object index.
		/// </summary>
		public GameObjectReference SourceObject { get; set; }

		/// <summary>
		/// Source component slot name hash.
		/// </summary>
		public uint SourceComponentSlot { get; set; }

		/// <summary>
		/// Destination object index.
		/// </summary>
		public GameObjectReference DestinationObject { get; set; }

		/// <summary>
		/// Destination component slot name hash.
		/// </summary>
		public uint DestinationComponentSlot { get; set; }

		/// <summary>
		/// Destination component name hash.
		/// </summary>
		public uint DestinationComponent { get; set; }

		/// <summary>
		/// Is broadcasting to child objects.
		/// </summary>
		public MessageBroadcasting MessageBroadcasting { get; set; }

		/// <summary>
		/// Is already handled and should not be broadcasted further.
		/// </summary>
		public bool IsHandled { get; set; }
	}
}