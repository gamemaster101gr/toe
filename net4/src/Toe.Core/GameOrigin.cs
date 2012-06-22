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
		/// The ==.
		/// </summary>
		/// <param name="left">
		/// The left.
		/// </param>
		/// <param name="right">
		/// The right.
		/// </param>
		/// <returns>
		/// </returns>
		public static bool operator ==(GameOrigin left, GameOrigin right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// The !=.
		/// </summary>
		/// <param name="left">
		/// The left.
		/// </param>
		/// <param name="right">
		/// The right.
		/// </param>
		/// <returns>
		/// </returns>
		public static bool operator !=(GameOrigin left, GameOrigin right)
		{
			return !left.Equals(right);
		}

		/// <summary>
		/// The equals.
		/// </summary>
		/// <param name="other">
		/// The other.
		/// </param>
		/// <returns>
		/// The equals.
		/// </returns>
		public bool Equals(GameOrigin other)
		{
			return other.position.Equals(this.position) && other.rotation.Equals(this.rotation);
		}

		/// <summary>
		/// Indicates whether this instance and a specified object are equal.
		/// </summary>
		/// <returns>
		/// True if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
		/// </returns>
		/// <param name="obj">
		/// Another object to compare to. 
		/// </param>
		/// <filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			if (obj.GetType() != typeof(GameOrigin))
			{
				return false;
			}

			return this.Equals((GameOrigin)obj);
		}

		/// <summary>
		/// Returns the hash code for this instance.
		/// </summary>
		/// <returns>
		/// A 32-bit signed integer that is the hash code for this instance.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			unchecked
			{
				return (this.position.GetHashCode() * 397) ^ this.rotation.GetHashCode();
			}
		}

		/// <summary>
		/// The update local position.
		/// </summary>
		/// <param name="parent">
		/// The parent.
		/// </param>
		/// <exception cref="NotImplementedException">
		/// </exception>
		public void UpdateLocalPosition(GameOrigin parent)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// The update local position by world position.
		/// </summary>
		public void UpdateLocalPositionByWorldPosition()
		{
			this.position = this.worldPosition;
			this.rotation = this.worldRotation;
		}

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
	}
}