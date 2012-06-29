namespace Toe.Core
{
	/// <summary>
	/// The game component slot.
	/// </summary>
	internal struct GameComponentSlot
	{
		#region Constants and Fields

		/// <summary>
		/// Component name hash value.
		/// </summary>
		internal uint Component;

		/// <summary>
		/// Component value.
		/// </summary>
		internal GameComponent GameComponent;

		/// <summary>
		/// Slot name hash value.
		/// </summary>
		internal uint Slot;

		#endregion
	}
}