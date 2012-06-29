using System.Windows.Forms;

using Toe.Core;
using Toe.Game.Windows;
using Toe.Marmalade.ResManager;

namespace Toe.Game
{
	/// <summary>
	/// The game application.
	/// </summary>
	public class GameApplication : IToeApplication
	{
		#region Constants and Fields

		private readonly IwResManager resManager;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="GameApplication"/> class.
		/// </summary>
		/// <param name="resManager">
		/// The res manager.
		/// </param>
		public GameApplication(IwResManager resManager)
		{
			this.resManager = resManager;
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Run the application.
		/// </summary>
		public void Run()
		{
			Application.Run(new GameWindow(this.resManager));
		}

		#endregion
	}
}