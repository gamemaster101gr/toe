using OpenTK.Graphics.OpenGL;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The _ iw model prim.
	/// </summary>
	public struct _IwModelPrim
	{
		#region Constants and Fields

		/// <summary>
		/// The i w_ mode l_ pri m_ gourau d_ f.
		/// </summary>
		public const uint IW_MODEL_PRIM_GOURAUD_F = 1 << 2; // !< prim has colour-per-vertex

		/// <summary>
		/// The i w_ mode l_ pri m_ n o_ rende r_ f.
		/// </summary>
		public const uint IW_MODEL_PRIM_NO_RENDER_F = 1 << 0; // !< do not render this prim

		/// <summary>
		/// The i w_ mode l_ pri m_ qua d_ f.
		/// </summary>
		public const uint IW_MODEL_PRIM_QUAD_F = 1 << 1; // !< prim is a quad, not a tri

		/// <summary>
		/// The i w_ mode l_ pri m_ texture d_ f.
		/// </summary>
		public const uint IW_MODEL_PRIM_TEXTURED_F = 1 << 3; // !< prim has texture data

		/// <summary>
		/// The m_ flags.
		/// </summary>
		public uint m_Flags;

		/// <summary>
		/// The m_ igi ds.
		/// </summary>
		public ushort[] m_IGIDs;

		/// <summary>
		/// The m_ u 0.
		/// </summary>
		public short[] m_U0;

		#endregion

		#region Properties

		private bool IsTextured
		{
			get
			{
				return 0 != (this.m_Flags & IW_MODEL_PRIM_TEXTURED_F);
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The render.
		/// </summary>
		/// <param name="model">
		/// The model.
		/// </param>
		/// <param name="flags">
		/// The flags.
		/// </param>
		/// <param name="verts">
		/// The verts.
		/// </param>
		/// <param name="indGroups">
		/// The ind groups.
		/// </param>
		/// <param name="colors">
		/// The colors.
		/// </param>
		public void Render(
			CIwModel model, uint flags, CIwModelBlockVerts verts, CIwModelBlockIndGroups indGroups, CIwModelBlockCols colors)
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

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="serialise">
		/// The serialise.
		/// </param>
		public void Serialise(IwSerialise serialise)
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
				// serialise.Int16(ref this.m_U0[0 * 2]);
				// serialise.Int16(ref this.m_U0[0 * 2 + 1]);

				// serialise.Int16(ref this.m_U0[1 * 2]);
				// serialise.Int16(ref this.m_U0[1 * 2 + 1]);

				// serialise.Int16(ref this.m_U0[2 * 2]);
				// serialise.Int16(ref this.m_U0[2 * 2 + 1]);

				// serialise.Int16(ref this.m_U0[3 * 2]);
				// serialise.Int16(ref this.m_U0[3 * 2 + 1]);

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

		#endregion

		#region Methods

		private void RenderVertex(
			int index, CIwModelBlockVerts verts, CIwModelBlockIndGroups indGroups, CIwModelBlockCols colors)
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

		#endregion
	}
}