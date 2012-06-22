using System;
using System.Reflection;

using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Reflection;

namespace Toe.Marmalade.Util
{
	public static class AutofacActivator
	{
		public static T CreateInstance<T>(this IComponentContext context)
		{
			return (T)context.CreateInstance(typeof(T), false);
		}
		public static object CreateInstance(this IComponentContext context, Type type)
		{
			return context.CreateInstance(type, false);
		}
		public static object CreateInstance(this IComponentContext context, Type type, bool nonPublic)
		{
			if (type == null)
				throw new ArgumentNullException("type");
			if (context == null)
				throw new ArgumentNullException("context");
			object res;
			if (context.TryResolve(type, out res))
			{
				return res;
			}
			var a = new ReflectionActivator(type,
				new BindingFlagsConstructorFinder(nonPublic ? BindingFlags.NonPublic : BindingFlags.Public),
				new MostParametersConstructorSelector(), new Parameter[] { }, new Parameter[] { });
			return a.ActivateInstance(context, new Parameter[] { });
		}
	}
}