using Autofac;

using NUnit.Framework;

using Toe.Marmalade.ResManager;

namespace Toe.Marmalade.Tests.Serialization
{
	/// <summary>
	/// Test resource group.
	/// </summary>
	[TestFixture]
	public class ResourceGroup : BaseTest
	{
		#region Public Methods and Operators
		/// <summary>
		/// Test on graphics scalable pipeline resources.
		/// </summary>
		[Test]
		public void AnimGles1ResourceGroup()
		{
			var rm = this.container.Resolve<IwResManager>();
			rm.AddDataPath(@"TestData\data-gles1");
			CIwResGroup group = rm.LoadGroup(@"TestData\data-gles1\iwanimskeleton.group.bin", false);

			// rm.DestroyGroup(group);
		}

		/// <summary>
		/// Test om graphics scalable pipeline resources.
		/// </summary>
		[Test]
		public void AnimSwResourceGroup()
		{
			var rm = this.container.Resolve<IwResManager>();
			rm.AddDataPath(@"TestData\data-sw");
			CIwResGroup group = rm.LoadGroup(@"TestData\data-sw\iwanimskeleton.group.bin", false);

			// rm.DestroyGroup(group);
		}

		/// <summary>
		/// Test on graphics scalable pipeline resources.
		/// </summary>
		[Test]
		public void BikeGles1ResourceGroup()
		{
			var rm = this.container.Resolve<IwResManager>();
			rm.AddDataPath(@"TestData\data-gles1");
			CIwResGroup group = rm.LoadGroup(@"TestData\data-gles1\iwgraphicsscalablepipeline.group.bin", false);

			// rm.DestroyGroup(group);
		}

		/// <summary>
		/// Test om graphics scalable pipeline resources.
		/// </summary>
		[Test]
		public void BikeSwResourceGroup()
		{
			var rm = this.container.Resolve<IwResManager>();
			rm.AddDataPath(@"TestData\data-sw");
			CIwResGroup group = rm.LoadGroup(@"TestData\data-sw\iwgraphicsscalablepipeline.group.bin", false);

			// rm.DestroyGroup(group);
		}

		/// <summary>
		/// The empty resource group.
		/// </summary>
		[Test]
		public void EmptyResourceGroup()
		{
			var rm = this.container.Resolve<IwResManager>();
			CIwResGroup group = rm.LoadGroup("TestData\\empty.group.bin", false);

			// rm.DestroyGroup(group);
		}

		/// <summary>
		/// The material resource group.
		/// </summary>
		[Test]
		public void MaterialResourceGroup()
		{
			var rm = this.container.Resolve<IwResManager>();
			CIwResGroup group = rm.LoadGroup("TestData\\material.group.bin", false);

			// rm.DestroyGroup(group);
		}

		/// <summary>
		/// The model resource group.
		/// </summary>
		[Test]
		public void ModelResourceGroup()
		{
			var rm = this.container.Resolve<IwResManager>();
			CIwResGroup group = rm.LoadGroup("TestData\\model.group.bin", false);

			// rm.DestroyGroup(group);
		}

		/// <summary>
		/// The model sw resource group.
		/// </summary>
		[Test]
		public void ModelSwResourceGroup()
		{
			var rm = this.container.Resolve<IwResManager>();
			CIwResGroup group = rm.LoadGroup("TestData\\modelsw.group.bin", false);

			// rm.DestroyGroup(group);
		}

		/// <summary>
		/// The texture resource group.
		/// </summary>
		[Test]
		public void TextureResourceGroup()
		{
			var rm = this.container.Resolve<IwResManager>();
			CIwResGroup group = rm.LoadGroup("TestData\\texture.group.bin", false);

			// rm.DestroyGroup(group);
		}

		#endregion
	}
}