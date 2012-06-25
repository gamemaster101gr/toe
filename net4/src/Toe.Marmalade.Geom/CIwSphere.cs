using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Geom
{
	public class CIwSphere
	{
		private Vector3 center;

		private float radius;

		public void Serialise(IwSerialise serialise)
		{
			serialise.Vec3(ref this.center);

			if (serialise.IsReading())
			{
				int r = 0;
				serialise.Int32(ref r);
				radius = (float)r;
			}
			else
			{
				int r = (int)radius;
				serialise.Int32(ref r);
			}
		}
	}
}
