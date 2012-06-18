using System.ComponentModel;

using NUnit.Framework;

using OpenTK;

using Toe.Core.Messages;
using Toe.CubeScene;

namespace Toe.Core.Tests
{
	/// <summary>
	/// The world tests.
	/// </summary>
	[TestFixture]
	public class WorldTests
	{
		#region Public Methods and Operators

		/// <summary>
		/// The test 1.
		/// </summary>
		[Test]
		public void Test1()
		{
			var classRegistry = new ClassRegistry();
			var scene = new CubeSceneSubsystem();
			scene.RegisterTypes(classRegistry);
			var w = new GameWorld(3, classRegistry, new IGameSubsystem[] { scene });
			var i1 = w.CreateObject();
			Assert.AreEqual(1, i1.Index);
			var i2 = w.CreateObject();
			Assert.AreEqual(2, i2.Index);
			var i3 = w.CreateObject();
			Assert.AreEqual(3, i3.Index);
			w.SendMessageToObjectComponentAtSlot<CreateComponent>(i2, GameWorld.GraphicsSlot, typeof(Level).Name.ToeHash());
			w.SendMessageToObject<DestroyObject>(i2);
			w.SendMessageToObjectComponentAtSlot<CreateComponent>(i1, GameWorld.GraphicsSlot, typeof(Camera).Name.ToeHash());
			w.SendMessageToObject<SetPosition>(i1).Position = new Vector3(1, 2, 3);
			w.ProcessGame();

			w.SendMessageToObject<DestroyObject>(i1);
			w.SendMessageToObject<DestroyObject>(i3);
			w.ProcessGame();
			i1 = w.CreateObject();
			Assert.AreEqual(2, i1.Index);
			i2 = w.CreateObject();
			Assert.AreEqual(1, i2.Index);
			i3 = w.CreateObject();
			Assert.AreEqual(3, i3.Index);
		}

		#endregion
	}
}