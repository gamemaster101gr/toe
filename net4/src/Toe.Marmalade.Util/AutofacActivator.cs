using System;
using System.Reflection;

using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Reflection;

namespace Toe.Marmalade.Util
{
	/// <summary>
	/// The autofac activator.
	/// </summary>
	public static class AutofacActivator
	{
		#region Public Methods and Operators

		/// <summary>
		/// The create instance.
		/// </summary>
		/// <param name="context">
		/// The context.
		/// </param>
		/// <typeparam name="T">
		/// </typeparam>
		/// <returns>
		/// </returns>
		public static T CreateInstance<T>(this IComponentContext context)
		{
			return (T)context.CreateInstance(typeof(T), false);
		}

		/// <summary>
		/// The create instance.
		/// </summary>
		/// <param name="context">
		/// The context.
		/// </param>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <returns>
		/// The create instance.
		/// </returns>
		public static object CreateInstance(this IComponentContext context, Type type)
		{
			return context.CreateInstance(type, false);
		}

		/// <summary>
		/// The create instance.
		/// </summary>
		/// <param name="context">
		/// The context.
		/// </param>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <param name="nonPublic">
		/// The non public.
		/// </param>
		/// <returns>
		/// The create instance.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// </exception>
		public static object CreateInstance(this IComponentContext context, Type type, bool nonPublic)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}

			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			object res;
			if (context.TryResolve(type, out res))
			{
				return res;
			}

			var a = new ReflectionActivator(
				type, 
				new BindingFlagsConstructorFinder(nonPublic ? BindingFlags.NonPublic : BindingFlags.Public), 
				new MostParametersConstructorSelector(), 
				new Parameter[] { }, 
				new Parameter[] { });
			return a.ActivateInstance(context, new Parameter[] { });
		}

		#endregion
	}
}