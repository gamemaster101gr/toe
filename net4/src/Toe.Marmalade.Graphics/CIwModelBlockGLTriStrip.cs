using OpenTK.Graphics.OpenGL;

using Toe.Marmalade.Gx;
using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model block gl tri strip.
	/// </summary>
	public class CIwModelBlockGLTriStrip : CIwModelBlockGLPrimBase
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
			CIwMaterial material = model.GetMaterial(this.materialId);
			if (material != null)
			{
				material.Enable();
			}

			GL.Begin(BeginMode.TriangleStrip);
			base.Render(model, flags);
			GL.End();
			S3E.CheckOpenGLStatus();

			if (material != null)
			{
				material.Disable();
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