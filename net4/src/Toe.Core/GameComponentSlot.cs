namespace Toe.Core
{
	internal struct GameComponentSlot
	{
		/// <summary>
		/// Slot name hash value.
		/// </summary>
		internal uint Slot;

		/// <summary>
		/// Component name hash value.
		/// </summary>
		internal uint Component;

		/// <summary>
		/// Component value.
		/// </summary>
		internal GameComponent GameComponent;
	}
}