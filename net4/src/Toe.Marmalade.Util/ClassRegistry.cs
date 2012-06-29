using System;
using System.Collections.Generic;
using System.Diagnostics;

using Autofac;

namespace Toe.Marmalade.Util
{
	/// <summary>
	/// The class registry.
	/// </summary>
	public class ClassRegistry
	{
		#region Constants and Fields

		private readonly Dictionary<uint, IwClassFactory> classMap = new Dictionary<uint, IwClassFactory>();

		private readonly IComponentContext context;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ClassRegistry"/> class.
		/// </summary>
		/// <param name="context">
		/// The context.
		/// </param>
		public ClassRegistry(IComponentContext context)
		{
			this.context = context;
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The add.
		/// </summary>
		/// <param name="name">
		/// The name.
		/// </param>
		/// <param name="t">
		/// The t.
		/// </param>
		public void Add(string name, Type t)
		{
			this.classMap[IwUtil.HashString(name)] = new IwClassFactory(this.context, t);
		}

		/// <summary>
		/// The add.
		/// </summary>
		/// <param name="t">
		/// The t.
		/// </param>
		public void Add(Type t)
		{
			this.Add(t.Name, t);
		}

		/// <summary>
		/// The get.
		/// </summary>
		/// <param name="hash">
		/// The hash.
		/// </param>
		/// <returns>
		/// </returns>
		public IwClassFactory Get(uint hash)
		{
			IwClassFactory v;
			if (this.classMap.TryGetValue(hash, out v))
			{
				return v;
			}

			Debug.WriteLine(string.Format("Hash not found {0}", hash));
			return null;
		}

		#endregion
	}
}