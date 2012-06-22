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
	}
}
