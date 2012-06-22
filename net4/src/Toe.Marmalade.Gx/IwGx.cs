using System;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Gx
{
	/// <summary>
	/// The Marmalade IwGx module.
	/// </summary>
	public class IwGx : IMarmaladeModule
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
		/// Initializes a new instance of the <see cref="IwGx"/> class. 
		/// </summary>
		/// <param name="classRegistry">
		/// The class Registry.
		/// </param>
		public IwGx(ClassRegistry classRegistry)
		{
			this.classRegistry = classRegistry;
			this.Init();
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="IwGx"/> class. 
		/// </summary>
		~IwGx()
		{
			this.Dispose(false);
		}

		#endregion

		#region Public Methods and Operators

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
		/// Initializes the module.
		/// </summary>
		public void Init()
		{
			this.classRegistry.Add(typeof(_IwGxDebugDataCacheType));
			this.classRegistry.Add(typeof(CIwGxShaderTechnique));
			this.classRegistry.Add(typeof(CIwMaterial));
			this.classRegistry.Add(typeof(CIwResTemplateMTL));
			this.classRegistry.Add(typeof(CIwResHandlerMTL));
			this.classRegistry.Add(typeof(CIwTexture));

			// this.classRegistry.Add(typeof(CIwManagedRefCount));
		}

		/// <summary>
		/// Terminates the module.
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