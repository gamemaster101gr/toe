using System.Collections.Generic;

using Autofac;

using NUnit.Framework;

namespace Toe.Marmalade.Tests.Serialization
{
	public class BaseTest
	{
		private ContainerBuilder builder;

		protected IContainer container;

		[SetUp]
		public void SetUpTest()
		{
			this.builder = new ContainerBuilder();
			this.builder.RegisterModule<Toe.Marmalade.Util.UtilModule>();
			this.builder.RegisterModule<Toe.Marmalade.ResManager.ResManagerModule>();
			////builder.RegisterModule<Toe.Marmalade.Gx.GxModule>();
			////builder.RegisterModule<Toe.Marmalade.Graphics.GraphicsModule>();
			builder.RegisterModule<Toe.Marmalade.Anim.AnimModule>();
			this.container = this.builder.Build();
			foreach (var module in container.Resolve<IEnumerable<IMarmaladeModule>>())
			{
			}
		}

		[TearDown]
		public void TearDownTest()
		{
			this.container.Dispose();
		}
	}
}