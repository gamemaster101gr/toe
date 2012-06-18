using OpenTK;

namespace Toe.Core.Messages
{
	public class SetRotation : MessageArgs
	{
		public Quaternion Rotation { get; set; }
	}
}