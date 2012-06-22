using Autofac;

namespace Toe.Core
{
	/// <summary>
	/// </summary>
	public class CoreModule: Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<GameWorld>().SingleInstance();
			base.Load(builder);
		}
	}
}