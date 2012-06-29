using Autofac;

namespace Toe.Core
{
	/// <summary>
	/// The core module.
	/// </summary>
	public class CoreModule : Module
	{
		#region Methods

		/// <summary>
		/// The load.
		/// </summary>
		/// <param name="builder">
		/// The builder.
		/// </param>
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<GameWorld>().SingleInstance();
			base.Load(builder);
		}

		#endregion
	}
}