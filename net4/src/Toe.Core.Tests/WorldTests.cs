using System.Collections.Generic;
using System.Diagnostics;

using Autofac;

using NUnit.Framework;

using OpenTK;

using Toe.Core.Messages;
using Toe.Marmalade;
using Toe.Marmalade.Anim;
using Toe.Marmalade.Graphics;
using Toe.Marmalade.Gx;
using Toe.Marmalade.ResManager;
using Toe.Marmalade.Util;

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
			var builder = new ContainerBuilder();
			builder.RegisterModule<CoreModule>();
			builder.RegisterModule<UtilModule>();
			builder.RegisterModule<ResManagerModule>();
			builder.RegisterModule<GxModule>();
			builder.RegisterModule<GraphicsModule>();
			builder.RegisterModule<AnimModule>();
			var container = builder.Build();
			foreach (var module in container.Resolve<IEnumerable<IMarmaladeModule>>())
			{
				Debug.WriteLine(module.GetType().FullName);
			}

			var w = container.Resolve<GameWorld>();
			var available = w.NumOfAvailable;
			var i1 = w.CreateObject();
			Assert.AreEqual(1, i1.Index);
			var i2 = w.CreateObject();
			Assert.AreEqual(2, i2.Index);
			var i3 = w.CreateObject();
			Assert.AreEqual(3, i3.Index);
			Assert.AreEqual(available - 3, w.NumOfAvailable);

			// w.SendMessageToObjectComponentAtSlot<CreateComponent>(i2, GameWorld.GraphicsSlot, typeof(Level).Name.ToeHash());
			w.SendMessageToObject<DestroyObject>(i2);

			// w.SendMessageToObjectComponentAtSlot<CreateComponent>(i1, GameWorld.GraphicsSlot, typeof(Camera).Name.ToeHash());
			w.SendMessageToObject<SetPosition>(i1).Position = new Vector3(1, 2, 3);
			w.ProcessGame();

			w.SendMessageToObject<DestroyObject>(i1);
			w.SendMessageToObject<DestroyObject>(i3);
			w.ProcessGame();
			Assert.AreEqual(available, w.NumOfAvailable);

			////i1 = w.CreateObject();
			////Assert.AreEqual(2, i1.Index);
			////i2 = w.CreateObject();
			////Assert.AreEqual(1, i2.Index);
			////i3 = w.CreateObject();
			////Assert.AreEqual(3, i3.Index);
		}

		#endregion
	}
}