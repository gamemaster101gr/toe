using System;
using System.Diagnostics;

using OpenTK.Graphics.OpenGL;

using Toe.Marmalade.Gx;

namespace Toe.Marmalade.Graphics
{
	public class CIwModelBlockPrimBase : CIwModelBlock
	{
		private _IwModelPrim[] prims;

		private ushort[] m_TupleIDs;
		private uint m_MaterialID;
		ushort m_NumTupleIDs;  // number of tuples used by this block

		public override void Serialise(Util.IwSerialise serialise)
		{
			base.Serialise(serialise);

			serialise.UInt32(ref m_MaterialID);
			serialise.UInt16(ref m_NumTupleIDs);

			if (serialise.IsReading())
			{
				m_TupleIDs = new ushort[m_NumTupleIDs];
				prims = new _IwModelPrim[this.numItems];
			}

			serialise.Serialise(ref m_TupleIDs);

			for (int i=0; i<this.numItems;++i)
				prims[i].Serialise(serialise);
		}

		public override uint Render(CIwModel model, uint flags)
		{
			CIwMaterial material = model.GetMaterial(m_MaterialID);
			if (material != null)
				material.Enable();

			S3E.CheckOpenGLStatus();
			GL.Begin(BeginMode.Triangles);

			var verts = model.ResolveBlock<CIwModelBlockVerts>();
			var colors = model.ResolveBlock<CIwModelBlockCols>();
			var normals = model.ResolveBlock<CIwModelBlockNorms>();
			var indGroups = model.ResolveBlock<CIwModelBlockIndGroups>();
			
			if (verts != null)
			{
				foreach (var iwModelPrim in prims)
				{
					iwModelPrim.Render(model, flags, verts, indGroups, colors);
				}
			}

			GL.End();
			if (material != null)
				material.Disable();
			S3E.CheckOpenGLStatus();

			return 0;
		}
	}
}