using Autofac;

using Toe.Core;

namespace Toe.Editor
{
	/// <summary>
	/// Editor Autofac module.
	/// </summary>
	public class EditorModule : Module
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
			builder.RegisterType<EditorApplication>().As<IToeApplication>().SingleInstance();
			base.Load(builder);
		}

		#endregion
	}
}