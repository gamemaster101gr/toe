using NUnit.Framework;

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
			var w = new GameWorld(4);
			int i1 = w.CreateObject();
			Assert.AreEqual(1, i1);
			int i2 = w.CreateObject();
			Assert.AreEqual(2, i2);
			int i3 = w.CreateObject();
			Assert.AreEqual(3, i3);
			w.Destroy(i2);
			w.Destroy(i1);
			w.Destroy(i3);
			w.CollectGarbage();
			i1 = w.CreateObject();
			Assert.AreEqual(2, i1);
			i2 = w.CreateObject();
			Assert.AreEqual(1, i2);
			i3 = w.CreateObject();
			Assert.AreEqual(3, i3);
		}

		#endregion
	}
}