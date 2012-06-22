namespace Toe.Core
{
	/// <summary>
	/// The game object reference.
	/// </summary>
	public struct GameObjectReference
	{
		#region Constants and Fields

		/// <summary>
		/// Null object (zero index).
		/// </summary>
		public static GameObjectReference Nil = new GameObjectReference(0, 0);

		private readonly int index;

		private readonly int uid;

		#endregion

		#region Constructors and Destructors

		internal GameObjectReference(int index, int uid)
		{
			this.index = index;
			this.uid = uid;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets index.
		/// </summary>
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether index is pointing to empty object.
		/// </summary>
		public bool IsNil
		{
			get
			{
				return this.index == 0;
			}
		}

		/// <summary>
		/// Gets unique identifier.
		/// </summary>
		public int Uid
		{
			get
			{
				return this.uid;
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The == operator.
		/// </summary>
		/// <param name="left">
		/// The left.
		/// </param>
		/// <param name="right">
		/// The right.
		/// </param>
		/// <returns>
		/// </returns>
		public static bool operator ==(GameObjectReference left, GameObjectReference right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// The != operator.
		/// </summary>
		/// <param name="left">
		/// The left.
		/// </param>
		/// <param name="right">
		/// The right.
		/// </param>
		/// <returns>
		/// </returns>
		public static bool operator !=(GameObjectReference left, GameObjectReference right)
		{
			return !left.Equals(right);
		}

		/// <summary>
		/// Indicates whether this instance and a specified object are equal.
		/// </summary>
		/// <param name="other">
		/// The other.
		/// </param>
		/// <returns>
		/// The equals.
		/// </returns>
		public bool Equals(GameObjectReference other)
		{
			return other.index == this.index && other.uid == this.uid;
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

			if (obj.GetType() != typeof(GameObjectReference))
			{
				return false;
			}

			return this.Equals((GameObjectReference)obj);
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
				return (this.index * 397) ^ this.uid;
			}
		}

		#endregion
	}
}