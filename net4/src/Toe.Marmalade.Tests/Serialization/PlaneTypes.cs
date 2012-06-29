using Autofac;

using NUnit.Framework;

using Toe.Marmalade.Anim;
using Toe.Marmalade.Util;

namespace Toe.Marmalade.Tests.Serialization
{
	/// <summary>
	/// Test plane types serialization.
	/// </summary>
	[TestFixture]
	public class PlaneTypes : BaseTest
	{
		#region Public Methods and Operators

		/// <summary>
		/// The bone.
		/// </summary>
		[Test]
		public void Bone()
		{
			var s = IwSerialise.Open("TestData\\managedobject_bone.bin", true, this.container.Resolve<ClassRegistry>(), null);
			CIwManaged obj = null;
			s.ManagedObject(ref obj);
			s.Close();
			Assert.IsTrue(obj is CIwAnimBone);
			Assert.AreEqual(obj.Hash, "bonename".ToeHash());
		}

		/// <summary>
		/// The bool.
		/// </summary>
		[Test]
		public void Bool()
		{
			var s = IwSerialise.Open("TestData\\bool.bin", true, null, null);
			bool v = false;
			s.Bool(ref v);
			Assert.AreEqual(v, false);
			s.Bool(ref v);
			Assert.AreEqual(v, true);
			s.Close();
		}

		/// <summary>
		/// The int 32.
		/// </summary>
		[Test]
		public void Int32()
		{
			var s = IwSerialise.Open("TestData\\int32.bin", true, null, null);
			int[] v = new int[16];
			s.Int32(ref v[0]);
			Assert.AreEqual(v[0], int.MinValue);
			s.Int32(ref v[0]);
			Assert.AreEqual(v[0], int.MaxValue);
			s.Int32(ref v[0]);
			Assert.AreEqual(v[0], 0x01020304);
			s.Close();
		}

		#endregion
	}
}