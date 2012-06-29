using System.Collections.Generic;

using Autofac;

using NUnit.Framework;

using Toe.Marmalade.Anim;
using Toe.Marmalade.Graphics;
using Toe.Marmalade.Gx;
using Toe.Marmalade.ResManager;
using Toe.Marmalade.Util;

namespace Toe.Marmalade.Tests.Serialization
{
	/// <summary>
	/// The base test.
	/// </summary>
	public class BaseTest
	{
		#region Constants and Fields

		/// <summary>
		/// The container.
		/// </summary>
		protected IContainer container;

		private ContainerBuilder builder;

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The set up test.
		/// </summary>
		[SetUp]
		public void SetUpTest()
		{
			this.builder = new ContainerBuilder();
			this.builder.RegisterModule<UtilModule>();
			this.builder.RegisterModule<ResManagerModule>();
			this.builder.RegisterModule<GxModule>();
			this.builder.RegisterModule<GraphicsModule>();
			this.builder.RegisterModule<AnimModule>();
			this.container = this.builder.Build();
			foreach (var module in this.container.Resolve<IEnumerable<IMarmaladeModule>>())
			{
			}
		}

		/// <summary>
		/// The tear down test.
		/// </summary>
		[TearDown]
		public void TearDownTest()
		{
			this.container.Dispose();
		}

		#endregion
	}
}