using Autofac;

namespace Toe.Core
{
	/// <summary>
	/// </summary>
	public class CoreModule: Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);
			builder.RegisterType<ClassRegistry>().SingleInstance();
		}
	}
}