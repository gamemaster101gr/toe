using Toe.Marmalade.Util;

namespace Toe.Marmalade.Anim
{
	/// <summary>
	/// The c iw anim.
	/// </summary>
	public class CIwAnim : CIwResource
	{
		public override void Serialise(IwSerialise serialise)
		{
			base.Serialise(serialise);
			serialise.ManagedHash<CIwAnimSkel>(ref m_Skel);
			{
				uint numBones = 2;
				serialise.UInt32(ref numBones);
			}
			boneFlags = new uint[1];
			{
				serialise.UInt32(ref boneFlags[0]);
			}
			m_KeyFrames.Serialise(serialise);

			serialise.Fixed(ref m_Duration);
			serialise.Fixed(ref m_TransformPrecision);
			serialise.ManagedHash(ref m_OfsAnim);
			serialise.DebugWrite(256);


		}
		uint[] boneFlags;
		CIwAnimSkel m_Skel;         // ptr to owner skeleton
		CIwManagedList m_KeyFrames = new CIwManagedList();    // all keyframes (arrays of transforms)
		float m_Duration;     // duration of keyframe (0x1000 = 1 sec)
		CIwAnim m_OfsAnim;  // offset anim that tracks root position
		float m_TransformPrecision; // the precision this anim is built at
	}
}