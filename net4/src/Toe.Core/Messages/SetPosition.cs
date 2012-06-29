using OpenTK;

namespace Toe.Core.Messages
{
	/// <summary>
	/// The set position.
	/// </summary>
	public class SetPosition : MessageArgs
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets Position.
		/// </summary>
		public Vector3 Position { get; set; }

		#endregion
	}
}