using System;

namespace Toe.Marmalade.Util
{
	/// <summary>
	/// The Marmalade IwUtil module.
	/// </summary>
	public class IwUtil : IMarmaladeModule
	{
		#region Constants and Fields

		/// <summary>
		/// Class registry.
		/// </summary>
		private readonly ClassRegistry classRegistry;

		/// <summary>
		/// True if objest is already disposed.
		/// </summary>
		private bool isDisposed;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="IwUtil"/> class. 
		/// </summary>
		/// <param name="classRegistry">
		/// The class Registry.
		/// </param>
		public IwUtil(ClassRegistry classRegistry)
		{
			this.classRegistry = classRegistry;
			this.Init();
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="IwUtil"/> class. 
		/// </summary>
		~IwUtil()
		{
			this.Dispose(false);
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The hash string.
		/// </summary>
		/// <param name="name">
		/// The name.
		/// </param>
		/// <returns>
		/// The hash string.
		/// </returns>
		public static uint HashString(string name)
		{
			return S3E.HashString(name);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// The init.
		/// </summary>
		public void Init()
		{
			this.classRegistry.Add(typeof(CIwManagedRefCount));
			this.classRegistry.Add(typeof(CIwResource));
		}

		/// <summary>
		/// The terminate.
		/// </summary>
		public void Terminate()
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <param name="disposing">
		/// True if disposing, False if GC.
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
			if (this.isDisposed)
			{
				return;
			}

			this.isDisposed = true;

			this.Terminate();
		}

		#endregion
	}
}