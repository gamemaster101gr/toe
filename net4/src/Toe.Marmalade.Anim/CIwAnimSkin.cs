using Toe.Marmalade.Graphics;
using Toe.Marmalade.Util;

namespace Toe.Marmalade.Anim
{
	/// <summary>
	/// The c iw anim skin.
	/// </summary>
	public class CIwAnimSkin: CIwResource
	{
		uint m_Flags;    // e.g. BONESPACE_VERTS_F
		CIwModel m_Model;    // ptr to model data
		CIwAnimSkel m_Skel;     // ptr to owner skeleton
		CIwManagedList m_Sets = new CIwManagedList();     // all skin sets (groups of weights)

		public override void Serialise(IwSerialise serialise)
		{
			base.Serialise(serialise);
			serialise.UInt32(ref m_Flags);
			serialise.ManagedHash<CIwModel>(ref m_Model);
			serialise.ManagedHash<CIwAnimSkel>(ref m_Skel);
			m_Sets.Serialise(serialise);
			//serialise.DebugWrite(758);
		}
	}
}