using Autofac;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.ResManager
{
	/// <summary>
	/// Autofac module.
	/// </summary>
	public class ResManagerModule : Module
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
			builder.RegisterType<IwResManager>().As<IwResManager>().As<IMarmaladeModule>().As<IResourceResolver>().SingleInstance();
			builder.RegisterType<ResScale>();
			builder.RegisterType<CIwResGroupBuildData>();
			builder.RegisterType<CIwResList>();
			builder.RegisterType<CIwResGroup>();
			builder.RegisterType<CIwResTemplate>();
			builder.RegisterType<CIwResHandler>();
			builder.RegisterType<CIwResTemplateGROUP>();
			builder.RegisterType<CIwResHandlerGROUP>();
			builder.RegisterType<CIwResTemplateImage>();
			builder.RegisterType<CIwResHandlerImage>();
			builder.RegisterType<CIwResHandlerITX>();
			base.Load(builder);
		}

		#endregion
	}
}