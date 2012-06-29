using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autofac;

namespace Toe.Lua
{
	public class LuaModule: Module
	{
		#region Methods

		/// <summary>
		/// Override to add registrations to the container.
		/// </summary>
		/// <remarks>
		/// Note that the ContainerBuilder parameter is unique to this module.
		/// </remarks>
		/// <param name="builder">
		/// The builder through which components can be registered.
		/// </param>
		protected override void Load(ContainerBuilder builder)
		{
			//builder.RegisterType<GameApplication>().As<IToeApplication>().SingleInstance();
			base.Load(builder);
		}

		#endregion
	}
}
