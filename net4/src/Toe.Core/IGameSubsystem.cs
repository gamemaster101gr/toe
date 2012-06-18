namespace Toe.Core
{
	/// <summary>
	/// Subsystem in a game (graphics, physics etc.).
	/// </summary>
	public interface IGameSubsystem
	{
		void RegisterTypes(ClassRegistry registry);
	}
}