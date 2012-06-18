using OpenTK;

using Toe.Core;

namespace Toe.CubeScene
{
	/// <summary>
	/// Camera component.
	/// </summary>
	public class Camera: GameComponent
	{
		public override void HandleMessage(Core.Messages.MessageArgs arguments)
		{
			base.HandleMessage(arguments);
		}

		public override void OnPositionChanged(ref GameOrigin origin)
		{
			base.OnPositionChanged(ref origin);
		}
	}
}