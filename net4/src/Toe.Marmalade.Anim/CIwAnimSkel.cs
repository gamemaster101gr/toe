using System.Collections.Generic;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Anim
{
	/// <summary>
	/// The c iw anim skel.
	/// </summary>
	public class CIwAnimSkel: CIwResource
	{
		public override void Serialise(IwSerialise serialise)
		{
			base.Serialise(serialise);

			bones.Serialise(serialise);
			
			serialise.Fixed(ref m_TransformPrecision);
			// 758 bytes

		}
		float m_TransformPrecision = 1;
		

		CIwManagedList bones = new CIwManagedList();
	}

	
}