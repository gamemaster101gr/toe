namespace Toe.Core
{
	/// <summary>
	/// Game object component.
	/// </summary>
	public abstract class GameComponent
	{
		protected virtual void RecieveMessage(uint methodNameHash, MessageArgs arguments)
		{
		}
	}
}