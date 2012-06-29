using OpenTK;

namespace Toe.Core.Messages
{
	/// <summary>
	/// The set rotation.
	/// </summary>
	public class SetRotation : MessageArgs
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets Rotation.
		/// </summary>
		public Quaternion Rotation { get; set; }

		#endregion
	}
}