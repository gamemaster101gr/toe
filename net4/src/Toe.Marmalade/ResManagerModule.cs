using Autofac;

namespace Toe.Marmalade
{
	/// <summary>
	/// Autofac module.
	/// </summary>
	public class MarmaladeModule : Module
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
			////builder.RegisterType<CIwResHandlerITX>();
			base.Load(builder);
		}

		#endregion
	}
}