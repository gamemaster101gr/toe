using System;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The Marmalade IwGraphics module.
	/// </summary>
	public class IwGraphics : IMarmaladeModule
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
		/// Initializes a new instance of the <see cref="IwGraphics"/> class. 
		/// </summary>
		/// <param name="classRegistry">
		/// The class Registry.
		/// </param>
		public IwGraphics(ClassRegistry classRegistry)
		{
			this.classRegistry = classRegistry;
			this.Init();
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="IwGraphics"/> class. 
		/// </summary>
		~IwGraphics()
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
		/// The init.
		/// </summary>
		public void Init()
		{
			this.classRegistry.Add(typeof(CIwModel));
			this.classRegistry.Add(typeof(CIwModelBlock));
			this.classRegistry.Add(typeof(CIwModelBuildInfo));
			this.classRegistry.Add(typeof(CIwModelExtPos));
			this.classRegistry.Add(typeof(CIwModelExtSphere));
			this.classRegistry.Add(typeof(CIwModelExtSelSet));
			this.classRegistry.Add(typeof(CIwModelExtSelSetVert));
			this.classRegistry.Add(typeof(CIwModelExtSelSetEdge));
			this.classRegistry.Add(typeof(CIwModelExtSelSetFace));
			this.classRegistry.Add(typeof(CIwModelExt));
			this.classRegistry.Add(typeof(CIwResTemplateGEO));
			this.classRegistry.Add(typeof(CIwResHandlerGEO));

			// this.classRegistry.Add(typeof(CIwManagedRefCount));
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