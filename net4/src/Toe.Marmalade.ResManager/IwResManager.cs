using System;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.ResManager
{
	public class IwResManager : IMarmaladeModule
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
		/// Initializes a new instance of the <see cref="IwResManager"/> class. 
		/// </summary>
		/// <param name="classRegistry">
		/// The class Registry.
		/// </param>
		public IwResManager(ClassRegistry classRegistry)
		{
			this.classRegistry = classRegistry;
			this.Init();
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="IwResManager"/> class. 
		/// </summary>
		~IwResManager()
		{
			this.Dispose(false);
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

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void Init()
		{
			this.classRegistry.Add(typeof(ResScale));
			this.classRegistry.Add(typeof(CIwResGroupBuildData));
			this.classRegistry.Add(typeof(CIwResList));
			this.classRegistry.Add(typeof(CIwResGroup));
			this.classRegistry.Add(typeof(CIwResTemplate));
			this.classRegistry.Add(typeof(CIwResHandler));
			this.classRegistry.Add(typeof(CIwResTemplateGROUP));
			this.classRegistry.Add(typeof(CIwResHandlerGROUP));
			this.classRegistry.Add(typeof(CIwResTemplateImage));
			this.classRegistry.Add(typeof(CIwResHandlerImage));
			this.classRegistry.Add(typeof(CIwResHandlerITX));
		}

		protected void Terminate()
		{
			
		}
	}
}
