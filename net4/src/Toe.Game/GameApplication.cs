using System.Windows.Forms;

using Toe.Core;
using Toe.Game.Windows;

namespace Toe.Game
{
	public class GameApplication: IToeApplication
	{
		/// <summary>
		/// Run the application.
		/// </summary>
		public void Run()
		{
			Application.Run(new GameWindow());
		}
	}
}