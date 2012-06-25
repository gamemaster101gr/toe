using System.Windows.Forms;

using Toe.Core;
using Toe.Game.Windows;
using Toe.Marmalade.ResManager;

namespace Toe.Game
{
	public class GameApplication: IToeApplication
	{
		private readonly IwResManager resManager;

		public GameApplication(IwResManager resManager)
		{
			this.resManager = resManager;
		}

		/// <summary>
		/// Run the application.
		/// </summary>
		public void Run()
		{
			Application.Run(new GameWindow(resManager));
		}
	}
}