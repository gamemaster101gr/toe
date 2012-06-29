using System;
using System.Collections.Generic;
using System.Diagnostics;
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

		private readonly List<string> dataFolders = new List<string>();

		private readonly List<CIwResGroup> groups = new List<CIwResGroup>();

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

		#region Public Methods and Operators

		/// <summary>
		/// The add data path.
		/// </summary>
		/// <param name="folder">
		/// The folder.
		/// </param>
		/// <exception cref="DirectoryNotFoundException">
		/// </exception>
		public void AddDataPath(string folder)
		{
			if (Directory.Exists(folder))
			{
				this.dataFolders.Add(Path.GetFullPath(folder));
				return;
			}

			foreach (var dataFolder in this.dataFolders)
			{
				var f = Path.Combine(dataFolder, folder);
				if (Directory.Exists(folder))
				{
					this.dataFolders.Add(Path.GetFullPath(folder));
					return;
				}
			}

			throw new DirectoryNotFoundException(string.Format("Directory {0} not found", folder));
		}

		/// <summary>
		/// Destroys a group and all its resources. 
		/// </summary>
		/// <param name="group">
		/// </param>
		public void DestroyGroup(CIwResGroup group)
		{
			this.groups.Remove(group);
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
			if (allowNonExist && !File.Exists(groupPath))
			{
				return null;
			}

			var gr = new CIwResGroup(this);

			string originalDir = Directory.GetCurrentDirectory();

			try
			{
				var file = this.ResolveSourceOrBinaryFilePath(groupPath);
				Debug.WriteLine(string.Format("Loading {0}", file));
				this.groups.Add(gr);

				// Directory.SetCurrentDirectory(Path.GetDirectoryName(file));
				if (file.EndsWith(".bin", StringComparison.InvariantCultureIgnoreCase))
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

		/// <summary>
		/// The resolve.
		/// </summary>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <param name="hash">
		/// The hash.
		/// </param>
		/// <returns>
		/// </returns>
		public CIwManaged Resolve(uint type, uint hash)
		{
			foreach (var resGroup in this.groups)
			{
				CIwResource res = resGroup.GetResHashed(hash, type);
				if (res != null)
				{
					return res;
				}
			}

			return null;
		}

		/// <summary>
		/// The resolve file path.
		/// </summary>
		/// <param name="filePath">
		/// The file path.
		/// </param>
		/// <param name="ram">
		/// The ram.
		/// </param>
		/// <returns>
		/// The resolve file path.
		/// </returns>
		public string ResolveFilePath(string filePath, bool ram)
		{
			if (File.Exists(filePath))
			{
				return Path.GetFullPath(filePath);
			}

			return filePath;
		}

		/// <summary>
		/// The resolve source or binary file path.
		/// </summary>
		/// <param name="filePath">
		/// The file path.
		/// </param>
		/// <returns>
		/// The resolve source or binary file path.
		/// </returns>
		/// <exception cref="FileNotFoundException">
		/// </exception>
		public string ResolveSourceOrBinaryFilePath(string filePath)
		{
			bool originalExists;
			bool binExists;
			string binFilePath;
			filePath = filePath.Replace('/', Path.DirectorySeparatorChar);
			if (!ResolveFilePath(ref filePath, out binFilePath, out originalExists, out binExists))
			{
				foreach (var dataFolder in this.dataFolders)
				{
					var f = filePath.StartsWith(string.Empty + Path.DirectorySeparatorChar)
					        	? Path.Combine(dataFolder, filePath.Substring(1))
					        	: Path.Combine(dataFolder, filePath);
					if (ResolveFilePath(ref f, out binFilePath, out originalExists, out binExists))
					{
						filePath = f;
						goto success;
					}
				}

				throw new FileNotFoundException(string.Format("file {0} not found", filePath));
			}

			success:
			if (binExists && !originalExists)
			{
				return Path.GetFullPath(binFilePath);
			}

			if (originalExists && !binExists)
			{
				return Path.GetFullPath(filePath);
			}

			var timeBin = File.GetLastWriteTime(binFilePath);
			var timeOriginal = File.GetLastWriteTime(filePath);
			if (timeBin < timeOriginal)
			{
				return Path.GetFullPath(filePath);
			}

			return Path.GetFullPath(binFilePath);
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

		private static bool ResolveFilePath(
			ref string filePath, out string binFilePath, out bool originalExists, out bool binExists)
		{
			if (filePath.EndsWith(".bin", StringComparison.InvariantCultureIgnoreCase))
			{
				binFilePath = filePath;
				filePath = filePath.Substring(0, filePath.Length - 4);
			}
			else
			{
				binFilePath = filePath + ".bin";
			}

			binExists = File.Exists(binFilePath);
			originalExists = File.Exists(filePath);
			if (!binExists && !originalExists)
			{
				return false;
			}

			return true;
		}

		#endregion
	}
}