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
		/// Gets unique identifier.
		/// </summary>
		public int Uid
		{
			get
			{
				return this.uid;
			}
		}

		public bool IsNil
		{
			get
			{
				return index == 0;
			}
			set
			{
				throw new System.NotImplementedException();
			}
		}

		#endregion
	}
}