using System;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Anim
{
	/// <summary>
	/// The Marmalade IwAnim module.
	/// </summary>
	public class IwAnim : IMarmaladeModule
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
		/// Initializes a new instance of the <see cref="IwAnim"/> class. 
		/// </summary>
		/// <param name="classRegistry">
		/// The class Registry.
		/// </param>
		public IwAnim(ClassRegistry classRegistry)
		{
			this.classRegistry = classRegistry;
			this.Init();
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="IwAnim"/> class. 
		/// </summary>
		~IwAnim()
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
			this.classRegistry.Add(typeof(CIwAnimBone));
			this.classRegistry.Add(typeof(CIwAnim));
			this.classRegistry.Add(typeof(CIwAnimPlayer));
			this.classRegistry.Add(typeof(CIwAnimSkel));
			this.classRegistry.Add(typeof(CIwAnimSkinSet));
			this.classRegistry.Add(typeof(CIwAnimSkin));
			this.classRegistry.Add(typeof(CIwAnimKeyFrame));
			
			this.classRegistry.Add(typeof(CIwResTemplateANIM));
			this.classRegistry.Add(typeof(CIwResHandlerANIM));
			this.classRegistry.Add(typeof(CIwResTemplateSKEL));
			this.classRegistry.Add(typeof(CIwResHandlerSKEL));
			this.classRegistry.Add(typeof(CIwResTemplateSKIN));
			this.classRegistry.Add(typeof(CIwResHandlerSKIN));
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