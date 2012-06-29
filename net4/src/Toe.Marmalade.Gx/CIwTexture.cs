using OpenTK.Graphics.OpenGL;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Gx
{
	/// <summary>
	/// The c iw texture.
	/// </summary>
	public class CIwTexture : CIwResource
	{
		#region Constants and Fields

		/// <summary>
		/// The allo w_ lo w_ qualit y_ f.
		/// </summary>
		public const uint ALLOW_LOW_QUALITY_F = 1u << 31; // Indicate low quality compression can be used for this texture

		/// <summary>
		/// The animat e_ u v_ f.
		/// </summary>
		public const uint ANIMATE_UV_F = 1 << 16; // Animate UVs. USED BY LEGACY CODE ONLY.

		/// <summary>
		/// The attache d_ t o_ surfac e_ f.
		/// </summary>
		public const uint ATTACHED_TO_SURFACE_F = 1 << 26;
		                  // It is an error to use a texture that's bound to the active surface

		/// <summary>
		/// The boun d_ t o_ activ e_ surfac e_ f.
		/// </summary>
		public const uint BOUND_TO_ACTIVE_SURFACE_F = 1 << 27;
		                  // It is an error to use a texture that's bound to the active surface

		/// <summary>
		/// The clam p_ u v_ f.
		/// </summary>
		public const uint CLAMP_UV_F = 1 << 19; // Clamp (rather than wrap) in U and V

		/// <summary>
		/// The clam p_ u v_ se t_ f.
		/// </summary>
		public const uint CLAMP_UV_SET_F = 1 << 24; // Has HW target been set to clamp?

		/// <summary>
		/// The creat e_ fo r_2 d_ ap i_ f.
		/// </summary>
		public const uint CREATE_FOR_2D_API_F = 1 << 22;
		                  // For targets which support separate 2D and 3D APIs (e.g. J2ME + JSR-184); create the 2D API image (e.g. MIDP's Image)

		/// <summary>
		/// The creat e_ fo r_3 d_ ap i_ f.
		/// </summary>
		public const uint CREATE_FOR_3D_API_F = 1 << 21;
		                  // For targets which support separate 2D and 3D APIs (e.g. J2ME + JSR-184); create the 3D API image (e.g. JSR-184's Image2D and Texture2D)

		/// <summary>
		/// The cub e_ ma p_ f.
		/// </summary>
		public const uint CUBE_MAP_F = 1 << 29;
		                  // This texture contains a cube map. The pre-upload texture is a column of cube map faces in the order +ve/-ve x, +ve/-ve y, +ve/-ve z.

		/// <summary>
		/// The don t_ serialis e_ imag e_ f.
		/// </summary>
		public const uint DONT_SERIALISE_IMAGE_F = 1 << 25; // Do not save any image data for this texture

		/// <summary>
		/// The d o_ h w_ f.
		/// </summary>
		public const uint DO_HW_F = 1 << 14; // Perform HW-related functions on this texture

		/// <summary>
		/// The d o_ s w_ f.
		/// </summary>
		public const uint DO_SW_F = 1 << 13; // Perform SW-related functions on this texture

		/// <summary>
		/// The ignor e_ mipma p_ offse t_ f.
		/// </summary>
		public const uint IGNORE_MIPMAP_OFFSET_F = 1 << 30; // This texture will not be affected by the mipmap offset

		/// <summary>
		/// The kee p_ dat a_ afte r_ uploa d_ f.
		/// </summary>
		public const uint KEEP_DATA_AFTER_UPLOAD_F = 1 << 15;
		                  // Don't delete the texels and palette after upload. Use this for palette animation. Uses no extra memory if software rendering is enabled.

		/// <summary>
		/// The nativ e_16 bi t_ f.
		/// </summary>
		public const uint NATIVE_16BIT_F = 1 << 11; // Convert 16bit texture to native when uploading to VRAM

		/// <summary>
		/// The need s_ s w_ transparenc y_ f.
		/// </summary>
		public const uint NEEDS_SW_TRANSPARENCY_F = 1 << 1; // This texture has (and uses chromakey).

		/// <summary>
		/// The neve r_ s w_ f.
		/// </summary>
		public const uint NEVER_SW_F = 1 << 17; // Never upload this texture for SW

		/// <summary>
		/// The n o_ filte r_ f.
		/// </summary>
		public const uint NO_FILTER_F = 1 << 6; // Disable filtering.

		/// <summary>
		/// The n o_ mipmappin g_ f.
		/// </summary>
		public const uint NO_MIPMAPPING_F = 1 << 18; // Disable mipmapping.

		/// <summary>
		/// The n o_ transparenc y_ f.
		/// </summary>
		public const uint NO_TRANSPARENCY_F = 1 << 4; // Texture doesn't use colour-keying.

		/// <summary>
		/// The own s_ h w_ texel s_ f.
		/// </summary>
		public const uint OWNS_HW_TEXELS_F = 1 << 28; // Does this texture own its HW storage?

		/// <summary>
		/// The own s_ palett e_ cach e_ rgb s_ f.
		/// </summary>
		public const uint OWNS_PALETTE_CACHE_RGBS_F = 1 << 10; // m_PaletteCacheRGBs points to an owned allocation

		/// <summary>
		/// The own s_ palett e_ f.
		/// </summary>
		public const uint OWNS_PALETTE_F = 1 << 9; // m_Palette (palette data) points to an owned allocation.

		/// <summary>
		/// The own s_ texel s_ f.
		/// </summary>
		public const uint OWNS_TEXELS_F = 1 << 8; // m_Texels (texel data) points to an owned allocation.

		/// <summary>
		/// The share d_ sourc e_ f.
		/// </summary>
		public const uint SHARED_SOURCE_F = 1 << 23; // Other textures share this texture's texels

		/// <summary>
		/// The share s_ texel s_ f.
		/// </summary>
		public const uint SHARES_TEXELS_F = 1 << 12; // Uses another texture's texels

		/// <summary>
		/// The skippe d_ mipma p_ generation.
		/// </summary>
		public const uint SKIPPED_MIPMAP_GENERATION = 1 << 20; // Record if mip maps should have been generated, but weren't

		/// <summary>
		/// The uploade d_ f.
		/// </summary>
		public const uint UPLOADED_F = 1 << 0; // This texture has been uploaded.

		/// <summary>
		/// The uploa d_ o n_ loa d_ f.
		/// </summary>
		public const uint UPLOAD_ON_LOAD_F = 1 << 2; // Upload this texture as soon as it's been loaded.

		/// <summary>
		/// The use s_ transparenc y_ f.
		/// </summary>
		public const uint USES_TRANSPARENCY_F = 1 << 3; // Texture uses colour-keying.

		private byte FormatHW;

		private byte FormatSW;

		private uint flags = 0x00204314;

		private int glTexture;

		private CIwImage image;

		#endregion

		#region Properties

		/// <summary>
		/// Gets GlTexture.
		/// </summary>
		protected int GlTexture
		{
			get
			{
				if (this.glTexture == 0)
				{
					this.glTexture = GL.GenTexture();
					S3E.CheckOpenGLStatus();
					this.Upload();
				}

				return this.glTexture;
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The disable.
		/// </summary>
		/// <param name="index">
		/// The index.
		/// </param>
		public void Disable(int index)
		{
			GL.ActiveTexture(TextureUnit.Texture0 + index);
			GL.Disable(EnableCap.Texture2D);
			GL.BindTexture(TextureTarget.Texture2D, 0);
		}

		/// <summary>
		/// The enable.
		/// </summary>
		/// <param name="index">
		/// The index.
		/// </param>
		public void Enable(int index)
		{
			GL.ActiveTexture(TextureUnit.Texture0 + index);
			GL.Enable(EnableCap.Texture2D);
			S3E.CheckOpenGLStatus();
			GL.BindTexture(TextureTarget.Texture2D, this.GlTexture);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
			S3E.CheckOpenGLStatus();

			// GL.Enable(EnableCap.Blend);
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
			serialise.UInt32(ref this.flags);
			serialise.UInt8(ref this.FormatSW);
			serialise.UInt8(ref this.FormatHW);
			short unknown3 = 0x1000;
			serialise.Int16(ref unknown3);
			short unknown4 = 0x1000;
			serialise.Int16(ref unknown4);
			if (this.image == null)
			{
				this.image = new CIwImage();
			}

			this.image.Serialise(serialise);
			bool unknown5 = false;
			serialise.Bool(ref unknown5);
		}

		#endregion

		#region Methods

		/// <summary>
		/// The dispose.
		/// </summary>
		/// <param name="disposing">
		/// The disposing.
		/// </param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (this.glTexture != 0)
			{
				GL.DeleteTexture(this.glTexture);
				this.glTexture = 0;
			}
		}

		private void Upload()
		{
			GL.BindTexture(TextureTarget.Texture2D, this.glTexture);
			S3E.CheckOpenGLStatus();
			this.image.Upload();
		}

		#endregion
	}
}