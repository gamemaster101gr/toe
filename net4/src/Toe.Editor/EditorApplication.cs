using System.Windows.Forms;

using Toe.Core;
using Toe.Editor.Forms;

namespace Toe.Editor
{
	/// <summary>
	/// The editor application.
	/// </summary>
	public class EditorApplication : IToeApplication
	{
		#region Public Methods and Operators

		/// <summary>
		/// Run the application.
		/// </summary>
		public void Run()
		{
			Application.Run(new EditorWindow());
		}

		#endregion
	}
}