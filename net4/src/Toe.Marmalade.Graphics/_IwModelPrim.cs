using OpenTK.Graphics.OpenGL;

namespace Toe.Marmalade.Graphics
{
	public struct _IwModelPrim
	{
		public const uint IW_MODEL_PRIM_NO_RENDER_F   = (1 << 0);     //!< do not render this prim
		public const uint IW_MODEL_PRIM_QUAD_F        = (1 << 1);     //!< prim is a quad, not a tri
		public const uint IW_MODEL_PRIM_GOURAUD_F     = (1 << 2);     //!< prim has colour-per-vertex
		public const uint IW_MODEL_PRIM_TEXTURED_F    = (1 << 3);   //!< prim has texture data

		public uint m_Flags;
		public ushort[] m_IGIDs;
		public short[] m_U0;

		public void Serialise(Util.IwSerialise serialise)
		{
			if (serialise.IsReading())
			{
				this.m_IGIDs = new ushort[4];
				this.m_U0 = new short[8];
			}
			serialise.UInt32(ref this.m_Flags);
			int numVerts = 4;
			if (0 == (this.m_Flags & IW_MODEL_PRIM_QUAD_F) && serialise.IsVersionOlderThen(3, 5, 6))
			{
				numVerts = 3;
			}
			serialise.Serialise(ref this.m_IGIDs, numVerts);
			if (this.IsTextured)
			{
				//serialise.Int16(ref this.m_U0[0 * 2]);
				//serialise.Int16(ref this.m_U0[0 * 2 + 1]);

				//serialise.Int16(ref this.m_U0[1 * 2]);
				//serialise.Int16(ref this.m_U0[1 * 2 + 1]);

				//serialise.Int16(ref this.m_U0[2 * 2]);
				//serialise.Int16(ref this.m_U0[2 * 2 + 1]);

				//serialise.Int16(ref this.m_U0[3 * 2]);
				//serialise.Int16(ref this.m_U0[3 * 2 + 1]);

				if (0 != (this.m_Flags & IW_MODEL_PRIM_QUAD_F))
				{
					serialise.Int16(ref this.m_U0[0 * 2]);
					serialise.Int16(ref this.m_U0[0 * 2 + 1]);

					serialise.Int16(ref this.m_U0[2 * 2]);
					serialise.Int16(ref this.m_U0[2 * 2 + 1]);

					serialise.Int16(ref this.m_U0[3 * 2]);
					serialise.Int16(ref this.m_U0[3 * 2 + 1]);

					serialise.Int16(ref this.m_U0[1 * 2]);
					serialise.Int16(ref this.m_U0[1 * 2 + 1]);
				}
				else
				{
					serialise.Int16(ref this.m_U0[0 * 2]);
					serialise.Int16(ref this.m_U0[0 * 2 + 1]);

					serialise.Int16(ref this.m_U0[2 * 2]);
					serialise.Int16(ref this.m_U0[2 * 2 + 1]);

					serialise.Int16(ref this.m_U0[1 * 2]);
					serialise.Int16(ref this.m_U0[1 * 2 + 1]);

					serialise.Int16(ref this.m_U0[3 * 2]);
					serialise.Int16(ref this.m_U0[3 * 2 + 1]);
				}


			}
		}

		private bool IsTextured
		{
			get
			{
				return 0 != (this.m_Flags & IW_MODEL_PRIM_TEXTURED_F);
			}
		}

		public void Render(CIwModel model, uint flags, CIwModelBlockVerts verts, CIwModelBlockIndGroups indGroups, CIwModelBlockCols colors)
		{
			if (0 == (this.m_Flags & IW_MODEL_PRIM_QUAD_F))
			{
				// Triangle
				this.RenderVertex(2, verts, indGroups, colors);
				this.RenderVertex(1, verts, indGroups, colors);
				this.RenderVertex(0, verts, indGroups, colors);
			}
			else
			{
				this.RenderVertex(3, verts, indGroups, colors);
				this.RenderVertex(1, verts, indGroups, colors);
				this.RenderVertex(0, verts, indGroups, colors);

				this.RenderVertex(2, verts, indGroups, colors);
				this.RenderVertex(3, verts, indGroups, colors);
				this.RenderVertex(0, verts, indGroups, colors);
			}
		}

		private void RenderVertex(int index, CIwModelBlockVerts verts, CIwModelBlockIndGroups indGroups, CIwModelBlockCols colors)
		{
			if (colors != null && indGroups.HasColors)
			{
				GL.Color4(colors.Colors[indGroups.GetColor(this.m_IGIDs[index])]);
			}
			if (this.IsTextured)
			{
				GL.TexCoord2(this.m_U0[index * 2] / 4096.0f, this.m_U0[index * 2 + 1] / 4096.0f);
			}
			GL.Vertex3(verts.Verts[indGroups.GetVert(this.m_IGIDs[index])]);
		}
	}
}