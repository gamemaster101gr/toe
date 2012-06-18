using OpenTK;

namespace Toe.Core.Messages
{
	public class SetPosition : MessageArgs
	{
		public Vector3 Position { get; set; }
	}
}