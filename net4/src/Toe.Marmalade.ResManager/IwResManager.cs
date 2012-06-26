﻿using System;
using System.Collections.Generic;
using System.IO;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.ResManager
{
	/// <summary>
	/// The iw res manager.
	/// </summary>
	public class IwResManager : IResourceResolver, IMarmaladeModule
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

		List<CIwResGroup> groups = new List<CIwResGroup>();

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

		public CIwManaged Resolve(uint type, uint hash)
		{
			foreach (var resGroup in groups)
			{
				CIwResource res = resGroup.GetResHashed(hash, type);
				if (res != null)
				{
					return res;
				}
			}
			return null;
		}

		public string ResolveFilePath(string filePath, bool ram)
		{
			if (File.Exists(filePath)) return Path.GetFullPath(filePath);
			return filePath;
		}

		public string ResolveSourceOrBinaryFilePath(string filePath)
		{
			string binFilePath;
			if (filePath.EndsWith(".bin", StringComparison.InvariantCultureIgnoreCase))
			{
				binFilePath = filePath;
				filePath = filePath.Substring(0, filePath.Length - 4);
			}
			else
			{
				binFilePath = filePath + ".bin";
			}
			bool binExists = File.Exists(binFilePath);
			bool originalExists = File.Exists(filePath);
			if (!binExists && !originalExists)
				throw new FileNotFoundException("File not found",filePath);
			if (binExists && !originalExists) return Path.GetFullPath(binFilePath);
			if (originalExists && !binExists) return Path.GetFullPath(filePath);
			var timeBin = File.GetLastWriteTime(binFilePath);
			var timeOriginal = File.GetLastWriteTime(filePath);
			if (timeBin < timeOriginal) return Path.GetFullPath(filePath);
			return Path.GetFullPath(binFilePath);
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Destroys a group and all its resources. 
		/// </summary>
		/// <param name="group">
		/// </param>
		public void DestroyGroup(CIwResGroup group)
		{
			groups.Remove(group);
			group.Dispose();
		}

		/// <summary>
		/// Destroys a named group and all its resources. 
		/// </summary>
		/// <param name="group">
		/// </param>
		public void DestroyGroup(string group)
		{
			throw new NotImplementedException();
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
		/// Loads a resource group from a file. For more information on build mode and load mode, see @ ref resourceManagerModes "Resource Manager Modes". 
		/// </summary>
		/// <param name="groupPath">
		/// Pathname to GROUP text file to load. 
		/// </param>
		/// <param name="allowNonExist">
		/// True only if we wish to permit the non-existence of the GROUP file. Defaults to false. 
		/// </param>
		/// <returns>
		/// </returns>
		public CIwResGroup LoadGroup(string groupPath, bool allowNonExist)
		{
			if (allowNonExist && !File.Exists(groupPath)) return null;

			var gr = new CIwResGroup(this);

			string originalDir = Directory.GetCurrentDirectory();

			try
			{
				var file = this.ResolveSourceOrBinaryFilePath(groupPath);
				groups.Add(gr);
				//Directory.SetCurrentDirectory(Path.GetDirectoryName(file));

				if (file.EndsWith(".bin",StringComparison.InvariantCultureIgnoreCase))
				{
					using (var s = IwSerialise.Open(file, true, this.classRegistry, this))
					{
						gr.Read(s);
					}
				}
				else
				{
					throw new NotImplementedException();
				}

			} 
			finally
			{
				Directory.SetCurrentDirectory(originalDir);
			}

			return gr;
		}

		

		/// <summary>
		/// Loads a resource group from a memory buffer. 
		/// </summary>
		/// <param name="buffer">
		/// Pointer to buffer to read from.
		/// </param>
		/// <param name="offset">
		/// </param>
		/// <param name="length">
		/// </param>
		/// <returns>
		/// </returns>
		public CIwResGroup LoadGroupFromMemory(byte[] buffer, int offset, int length)
		{
			using (var ms = new MemoryStream(buffer, offset, length))
			{
			}

			return null;
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

		/// <summary>
		/// The init.
		/// </summary>
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

		/// <summary>
		/// The terminate.
		/// </summary>
		protected void Terminate()
		{
		}

		#endregion
	}
}