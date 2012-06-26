using OpenTK.Graphics.OpenGL;

using Toe.Marmalade.Gx;
using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model block gl prim base.
	/// </summary>
	public class CIwModelBlockGLPrimBase : CIwModelBlock
	{
		#region Constants and Fields

		/// <summary>
		/// The inds.
		/// </summary>
		protected ushort[] inds;

		protected uint materialId;

		private ushort streamIdBegin;

		private ushort streamIdEnd;

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
		/// <returns>
		/// The render.
		/// </returns>
		public override uint Render(CIwModel model, uint flags)
		{
			base.Render(model, flags);
			var verts = model.ResolveBlock<CIwModelBlockVerts>();
			var colors = model.ResolveBlock<CIwModelBlockCols>();
			var normals = model.ResolveBlock<CIwModelBlockNorms>();
			var uvs = model.ResolveBlock<CIwModelBlockGLUVs>();
			var uvs2 = model.ResolveBlock<CIwModelBlockGLUVs2>();
			if (verts != null)
			{
				foreach (var index in this.inds)
				{
					if (colors != null)
					{
						GL.Color4(colors.Colors[index]);
					}
					if (normals != null)
					{
						GL.Normal3(normals.Normal[index]);
					}
					if (uvs != null)
					{
						GL.TexCoord2(uvs.UVs[index]);
						GL.MultiTexCoord2(TextureUnit.Texture0,  ref uvs.UVs[index]);
					}
					GL.Vertex3(verts.Verts[index]);
				}
			}

			return 0;
		}

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="serialise">
		/// The serialise.
		/// </param>
		public override void Serialise(IwSerialise serialise)
		{
			base.Serialise(serialise);

			serialise.UInt32(ref this.materialId);

			this.inds = new ushort[this.numItems];
			serialise.Serialise(ref this.inds);
			{
				serialise.UInt16(ref this.streamIdBegin);
			}
			{
				serialise.UInt16(ref this.streamIdEnd);
			}
		}

		#endregion
	}
}