using Autofac;

namespace Toe.Marmalade.Gx
{
	/// <summary>
	/// Autofac module.
	/// </summary>
	public class GxModule : Module
	{
		#region Methods

		/// <summary>
		/// Adding registrations to the container.
		/// </summary>
		/// <param name="builder">
		/// The builder through which components can be registered.
		/// </param>
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<IwGx>().As<IwGx>().As<IMarmaladeModule>().SingleInstance();

			// builder.RegisterType<ClassRegistry>().SingleInstance();
			// builder.RegisterType<IwClassFactory>().SingleInstance();
			// builder.RegisterType<CIwManagedRefCount>();
			// builder.RegisterType<CIwResource>();
			base.Load(builder);
		}

		#endregion
	}
}