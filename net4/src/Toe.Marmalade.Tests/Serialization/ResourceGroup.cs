using Autofac;

using NUnit.Framework;

using Toe.Marmalade.ResManager;
using Toe.Marmalade.Util;

namespace Toe.Marmalade.Tests.Serialization
{
	/// <summary>
	/// Test resource group.
	/// </summary>
	[TestFixture]
	public class ResourceGroup : BaseTest
	{
		[Test]
		public void EmptyResourceGroup()
		{
			var rm = container.Resolve<IwResManager>();
			CIwResGroup group = rm.LoadGroup("TestData\\empty.group.bin", false); 
			//rm.DestroyGroup(group);
		}

		[Test]
		public void TextureResourceGroup()
		{
			var rm = container.Resolve<IwResManager>();
			CIwResGroup group = rm.LoadGroup("TestData\\texture.group.bin", false);
			//rm.DestroyGroup(group);
		}

		[Test]
		public void ModelResourceGroup()
		{
			var rm = container.Resolve<IwResManager>();
			CIwResGroup group = rm.LoadGroup("TestData\\model.group.bin", false);
			//rm.DestroyGroup(group);
		}

		[Test]
		public void ModelSwResourceGroup()
		{
			var rm = container.Resolve<IwResManager>();
			CIwResGroup group = rm.LoadGroup("TestData\\modelsw.group.bin", false);
			//rm.DestroyGroup(group);
		}

		[Test]
		public void Model2ResourceGroup()
		{
			var rm = container.Resolve<IwResManager>();
			CIwResGroup group = rm.LoadGroup(@"Z:\MyWork\toe.git\data-ram\data-gles1\testdata\model.group.bin", false);
			//rm.DestroyGroup(group);
		}

		[Test]
		public void MaterialResourceGroup()
		{
			var rm = container.Resolve<IwResManager>();
			CIwResGroup group = rm.LoadGroup("TestData\\material.group.bin", false);
			//rm.DestroyGroup(group);
		}

		[Test]
		public void Material2ResourceGroup()
		{
			var rm = container.Resolve<IwResManager>();
			CIwResGroup group = rm.LoadGroup(@"Z:\MyWork\toe.git\data-ram\data-gles1\testdata\material.group.bin ", false);
			//rm.DestroyGroup(group);
		}

		[Test]
		public void BikeResourceGroup()
		{
			var rm = container.Resolve<IwResManager>();
			CIwResGroup group = rm.LoadGroup("TestData\\bike.group.bin", false);
			//rm.DestroyGroup(group);
		}
	}
}
