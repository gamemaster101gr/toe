using OpenTK.Graphics.OpenGL;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Gx
{
	/// <summary>
	/// The c iw texture.
	/// </summary>
	public class CIwTexture: CIwResource
	{
		public const uint UPLOADED_F          = (1 << 0);     // This texture has been uploaded.
        public const uint NEEDS_SW_TRANSPARENCY_F = (1 << 1);     // This texture has (and uses chromakey).
        public const uint UPLOAD_ON_LOAD_F    = (1 << 2);     // Upload this texture as soon as it's been loaded.
        public const uint USES_TRANSPARENCY_F = (1 << 3);     // Texture uses colour-keying.
        public const uint NO_TRANSPARENCY_F   = (1 << 4);     // Texture doesn't use colour-keying.
        //                  = (1 << 5);     //
        public const uint NO_FILTER_F         = (1 << 6);     // Disable filtering.

        public const uint OWNS_TEXELS_F       = (1 << 8);     // m_Texels (texel data) points to an owned allocation.
        public const uint OWNS_PALETTE_F      = (1 << 9);     // m_Palette (palette data) points to an owned allocation.
        public const uint OWNS_PALETTE_CACHE_RGBS_F = (1 << 10);  // m_PaletteCacheRGBs points to an owned allocation
        public const uint NATIVE_16BIT_F      = (1 << 11);    // Convert 16bit texture to native when uploading to VRAM
        public const uint SHARES_TEXELS_F     = (1 << 12);    // Uses another texture's texels

        public const uint DO_SW_F             = (1 << 13);    // Perform SW-related functions on this texture
        public const uint DO_HW_F             = (1 << 14);    // Perform HW-related functions on this texture

        public const uint KEEP_DATA_AFTER_UPLOAD_F = (1 << 15); // Don't delete the texels and palette after upload. Use this for palette animation. Uses no extra memory if software rendering is enabled.
        public const uint ANIMATE_UV_F        = (1 << 16);    // Animate UVs. USED BY LEGACY CODE ONLY.

        public const uint NEVER_SW_F          = (1 << 17);    // Never upload this texture for SW
        public const uint NO_MIPMAPPING_F     = (1 << 18);    // Disable mipmapping.
        public const uint CLAMP_UV_F          = (1 << 19);    // Clamp (rather than wrap) in U and V

        public const uint SKIPPED_MIPMAP_GENERATION = (1 << 20); // Record if mip maps should have been generated, but weren't

        public const uint CREATE_FOR_3D_API_F = (1 << 21);    // For targets which support separate 2D and 3D APIs (e.g. J2ME + JSR-184); create the 3D API image (e.g. JSR-184's Image2D and Texture2D)
        public const uint CREATE_FOR_2D_API_F = (1 << 22);    // For targets which support separate 2D and 3D APIs (e.g. J2ME + JSR-184); create the 2D API image (e.g. MIDP's Image)

        public const uint SHARED_SOURCE_F     = (1 << 23);    // Other textures share this texture's texels

        public const uint CLAMP_UV_SET_F      = (1 << 24);    // Has HW target been set to clamp?

        public const uint DONT_SERIALISE_IMAGE_F  = (1 << 25);    // Do not save any image data for this texture

        public const uint ATTACHED_TO_SURFACE_F   = (1 << 26);    // It is an error to use a texture that's bound to the active surface
        public const uint BOUND_TO_ACTIVE_SURFACE_F   = (1 << 27);    // It is an error to use a texture that's bound to the active surface
        public const uint OWNS_HW_TEXELS_F        = (1 << 28);    // Does this texture own its HW storage?

        public const uint CUBE_MAP_F              = (1 << 29);    // This texture contains a cube map. The pre-upload texture is a column of cube map faces in the order +ve/-ve x, +ve/-ve y, +ve/-ve z.

        public const uint IGNORE_MIPMAP_OFFSET_F  = (1 << 30);    // This texture will not be affected by the mipmap offset

		public const uint ALLOW_LOW_QUALITY_F = (1u << 31);    // Indicate low quality compression can be used for this texture

		private uint flags = 0x00204314;

		private byte FormatSW;

		private byte FormatHW;
		private CIwImage image;
		public override void Serialise(IwSerialise serialise)
		{
			base.Serialise(serialise);
			serialise.UInt32(ref flags);
			serialise.UInt8(ref FormatSW);
			serialise.UInt8(ref FormatHW);
			short unknown3 = 0x1000;
			serialise.Int16(ref unknown3);
			short unknown4 = 0x1000;
			serialise.Int16(ref unknown4);
			if (image == null)
			{
				image = new CIwImage();
			}
			image.Serialise(serialise);
			bool unknown5 = false;
			serialise.Bool(ref unknown5);
		}

		private int glTexture = 0;

		protected int GlTexture
		{
			get
			{
				if (glTexture == 0)
				{
					glTexture = GL.GenTexture();
					S3E.CheckOpenGLStatus();
					this.Upload();
				}
				return glTexture;
			}
		}

		private void Upload()
		{
			GL.BindTexture(TextureTarget.Texture2D, this.glTexture);
			S3E.CheckOpenGLStatus();
			image.Upload();
			
		}

		public void Enable(int index)
		{
			GL.ActiveTexture(TextureUnit.Texture0+index);
			GL.Enable(EnableCap.Texture2D);
			S3E.CheckOpenGLStatus();
			GL.BindTexture(TextureTarget.Texture2D, GlTexture);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
			S3E.CheckOpenGLStatus();
			//GL.Enable(EnableCap.Blend);
		}
		public void Disable(int index)
		{
			GL.ActiveTexture(TextureUnit.Texture0 + index);
			GL.Disable(EnableCap.Texture2D);
			GL.BindTexture(TextureTarget.Texture2D, 0);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (glTexture != 0)
			{
				GL.DeleteTexture(glTexture);
				glTexture = 0;
			}
		}
	}
}