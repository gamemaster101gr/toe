using System;
using System.Collections.Generic;
using System.Diagnostics;

using Autofac;
using Autofac.Configuration;

using Toe.Core;
using Toe.Marmalade;

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
			foreach (var module in container.Resolve<IEnumerable<IMarmaladeModule>>())
			{
				Debug.WriteLine(module.GetType().FullName);
			}
			AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
			container.Resolve<IToeApplication>().Run();
		}

		private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			
		}
	}
}
