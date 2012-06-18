using System;

namespace Toe.Core
{
	/// <summary>
	/// Game object state.
	/// </summary>
	[Flags]
	public enum GameObjectFlag
	{
		/// <summary>
		/// Available object.
		/// </summary>
		Available = 1,

		/// <summary>
		/// Occupied game object.
		/// </summary>
		Occupied = 2,

		/// <summary>
		/// Game object is garbage.
		/// </summary>
		Garbage = 3,

		AvailabilityMask = 3,

		Moved = 4,
	}
}