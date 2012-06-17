using System;

using Autofac;
using Autofac.Configuration;

using Toe.Core;

namespace Toe.App
{
	/// <summary>
	/// Main application class.
	/// </summary>
	public class MainClass
	{
		/// <summary>
		/// Main application method.
		/// </summary>
		/// <param name="args">Command line arguments.</param>
		public static void Main(string[] args)
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule(new ConfigurationSettingsReader());
			var container = builder.Build();

			container.Resolve<IToeApplication>().Run();
		}
	}
}
