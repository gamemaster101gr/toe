using System;

using Autofac;

namespace Toe.Marmalade.Util
{
	/// <summary>
	/// The iw class factory.
	/// </summary>
	public class IwClassFactory
	{
		#region Constants and Fields

		private readonly IComponentContext context;

		private readonly Type type;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="IwClassFactory"/> class.
		/// </summary>
		/// <param name="context">
		/// The context.
		/// </param>
		/// <param name="t">
		/// The t.
		/// </param>
		public IwClassFactory(IComponentContext context, Type t)
		{
			this.context = context;
			this.type = t;
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The create.
		/// </summary>
		/// <returns>
		/// The create.
		/// </returns>
		public CIwManaged Create()
		{
			return (CIwManaged)this.context.CreateInstance(this.type);
		}

		#endregion
	}
}