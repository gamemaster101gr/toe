using System;

namespace Toe.Core
{
	/// <summary>
	/// Game object state.
	/// </summary>
	[Flags]
	public enum GameObjectFlags
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

		/// <summary>
		/// The availability mask.
		/// </summary>
		AvailabilityMask = 3, 

		/// <summary>
		/// The moved.
		/// </summary>
		Moved = 4, 
	}
}