using System;

using OpenTK.Graphics.OpenGL;

using Toe.Marmalade.Gx;
using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model block gl tri list.
	/// </summary>
	public class CIwModelBlockGLTriList : CIwModelBlockGLPrimBase
	{
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
			try
			{
				CIwMaterial material = model.GetMaterial(this.materialId);
				if (material != null)
				{
					material.Enable();
				}

				S3E.CheckOpenGLStatus();
				GL.Begin(BeginMode.Triangles);
				base.Render(model, flags);
				GL.End();
				if (material != null)
				{
					material.Disable();
				}

				S3E.CheckOpenGLStatus();
			}
			catch (Exception ex)
			{
				throw;
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
		}

		#endregion
	}
}