using OpenTK;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Geom
{
	/// <summary>
	/// The c iw sphere.
	/// </summary>
	public class CIwSphere
	{
		#region Constants and Fields

		private Vector3 center;

		private float radius;

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="serialise">
		/// The serialise.
		/// </param>
		public void Serialise(IwSerialise serialise)
		{
			serialise.Vec3(ref this.center);

			if (serialise.IsReading())
			{
				int r = 0;
				serialise.Int32(ref r);
				this.radius = r;
			}
			else
			{
				int r = (int)this.radius;
				serialise.Int32(ref r);
			}
		}

		#endregion
	}
}