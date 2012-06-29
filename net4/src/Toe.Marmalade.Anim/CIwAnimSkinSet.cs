using System;
using System.Collections.Generic;

using OpenTK;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Anim
{
	public struct CIwAnimSkinSetVertexBone
	{
		public Vector3 BindPose;
		public byte Weight;
	}
	public struct CIwAnimSkinSetVertex
	{
		public UInt16 VertexId;

		public CIwAnimSkinSetVertexBone[] Bones;
	}
	/// <summary>
	/// The c iw anim skin set.
	/// </summary>
	public class CIwAnimSkinSet: CIwManaged
	{
		private List<byte> boneIds = new List<byte>();
		CIwAnimSkinSetVertex[] vertices;
		uint m_NumVerts; // number of verts in skin set
		public override void Serialise(IwSerialise serialise)
		{
			base.Serialise(serialise);
			serialise.UInt32(ref m_NumVerts);
			if (serialise.IsReading())
			{
				uint dataLen = 0;
				serialise.UInt32(ref dataLen);
				long pos = serialise.Position + dataLen;

				byte boneId = 0;
				
				for (; ; )
				{
					serialise.UInt8(ref boneId);
					if (boneId == 0xFF)
						break;
					boneIds.Add(boneId);
				}
				// pad
				if ((boneIds.Count % 2) == 0)
					serialise.UInt8(ref boneId);

				vertices = new CIwAnimSkinSetVertex[m_NumVerts];

				for (int i = 0; i < m_NumVerts;++i )
				{
					serialise.UInt16(ref vertices[i].VertexId);
					vertices[i].Bones = new CIwAnimSkinSetVertexBone[boneIds.Count];

					for (int j = 0; j < boneIds.Count; j++)
					{
						serialise.SVec3(ref vertices[i].Bones[j].BindPose);
					}
					for (int j = 0; j < boneIds.Count; j++)
					{
						serialise.UInt8(ref vertices[i].Bones[j].Weight);
					}
					// pad
					if ( boneIds.Count % 2 == 1)
						serialise.UInt8(ref boneId);
				}

					if (serialise.Position != pos)
						throw new FormatException("CIwAnimSkinSet");
			}
			else
			{
				throw new ApplicationException();
			}
		}
	}
}