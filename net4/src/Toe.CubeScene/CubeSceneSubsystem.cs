using System;

using Toe.Core;

namespace Toe.CubeScene
{
	/// <summary>
	/// The cube scene component fabric.
	/// </summary>
	public class CubeSceneSubsystem : IGameSubsystem
	{
		#region Implementation of IGameSubsystem

		public void RegisterTypes(ClassRegistry registry)
		{
			registry.RegisterType<Camera>();
			registry.RegisterType<Level>();
		}

		#endregion
	}
}