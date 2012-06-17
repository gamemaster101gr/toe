using System.Windows.Forms;

using Toe.Core;
using Toe.Editor.Forms;

namespace Toe.Editor
{
	public class EditorApplication: IToeApplication
	{
		/// <summary>
		/// Run the application.
		/// </summary>
		public void Run()
		{
			Application.Run(new EditorWindow());
		}
	}
}