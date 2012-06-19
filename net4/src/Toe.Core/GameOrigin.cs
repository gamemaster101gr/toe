using System;

using OpenTK;

namespace Toe.Core
{
	/// <summary>
	/// The game origin.
	/// </summary>
	public struct GameOrigin
	{
		#region Constants and Fields

		private Vector3 position;

		private Quaternion rotation;

		private Vector3 worldPosition;

		private Quaternion worldRotation;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets Position.
		/// </summary>
		public Vector3 Position
		{
			get
			{
				return this.position;
			}

			internal set
			{
				this.position = value;
			}
		}

		/// <summary>
		/// Gets or sets Rotation.
		/// </summary>
		public Quaternion Rotation
		{
			get
			{
				return this.rotation;
			}

			internal set
			{
				this.rotation = value;
			}
		}

		/// <summary>
		/// Gets or sets WorldPosition.
		/// </summary>
		public Vector3 WorldPosition
		{
			get
			{
				return this.worldPosition;
			}

			internal set
			{
				this.worldPosition = value;
			}
		}

		/// <summary>
		/// Gets or sets WorldRotation.
		/// </summary>
		public Quaternion WorldRotation
		{
			get
			{
				return this.worldRotation;
			}

			internal set
			{
				this.worldRotation = value;
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The update world position.
		/// </summary>
		/// <param name="origin">
		/// The origin.
		/// </param>
		public void UpdateWorldPosition(ref GameOrigin origin)
		{
			Vector3.Transform(ref this.position, ref origin.worldRotation, out this.worldPosition);

			// TODO: check if the order is right
			Quaternion.Multiply(ref this.rotation, ref origin.worldRotation, out this.worldRotation);
		}

		#endregion

		public void UpdateLocalPosition(GameOrigin parent)
		{
			throw new NotImplementedException();
		}

		public void UpdateLocalPositionByWorldPosition()
		{
			this.position = this.worldPosition;
			this.rotation = this.worldRotation;
		}
	}
}