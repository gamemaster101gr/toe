using OpenTK.Graphics.OpenGL;

using Toe.Marmalade.Gx;
using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model block prim base.
	/// </summary>
	public class CIwModelBlockPrimBase : CIwModelBlock
	{
		#region Constants and Fields

		private uint m_MaterialID;

		private ushort m_NumTupleIDs; // number of tuples used by this block

		private ushort[] m_TupleIDs;

		private _IwModelPrim[] prims;

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
			CIwMaterial material = model.GetMaterial(this.m_MaterialID);
			if (material != null)
			{
				material.Enable();
			}

			S3E.CheckOpenGLStatus();
			GL.Begin(BeginMode.Triangles);

			var verts = model.ResolveBlock<CIwModelBlockVerts>();
			var colors = model.ResolveBlock<CIwModelBlockCols>();
			var normals = model.ResolveBlock<CIwModelBlockNorms>();
			var indGroups = model.ResolveBlock<CIwModelBlockIndGroups>();

			if (verts != null)
			{
				foreach (var iwModelPrim in this.prims)
				{
					iwModelPrim.Render(model, flags, verts, indGroups, colors);
				}
			}

			GL.End();
			if (material != null)
			{
				material.Disable();
			}

			S3E.CheckOpenGLStatus();

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

			serialise.UInt32(ref this.m_MaterialID);
			serialise.UInt16(ref this.m_NumTupleIDs);

			if (serialise.IsReading())
			{
				this.m_TupleIDs = new ushort[this.m_NumTupleIDs];
				this.prims = new _IwModelPrim[this.numItems];
			}

			serialise.Serialise(ref this.m_TupleIDs);

			for (int i = 0; i < this.numItems; ++i)
			{
				this.prims[i].Serialise(serialise);
			}
		}

		#endregion
	}
}