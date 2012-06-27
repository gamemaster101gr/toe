using System.Drawing;

using OpenTK.Graphics.OpenGL;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Gx
{
	/// <summary>
	/// The c iw material.
	/// </summary>
	public class CIwMaterial : CIwResource
	{
		#region Constants and Fields

		/// <summary>
		/// The alphatest mode is always.
		/// </summary>
		public const uint AlphatestAlways = 8;

		/// <summary>
		/// The alphatest mode is disabled.
		/// </summary>
		public const uint AlphatestDisabled = 0;

		/// <summary>
		/// The alphatest mode is equal.
		/// </summary>
		public const uint AlphatestEqual = 3;

		/// <summary>
		/// The alphatest mode is gequal.
		/// </summary>
		public const uint AlphatestGequal = 7;

		/// <summary>
		/// The alphatest mode is greater.
		/// </summary>
		public const uint AlphatestGreater = 5;

		/// <summary>
		/// The alphatest mode is lequal.
		/// </summary>
		public const uint AlphatestLequal = 4;

		/// <summary>
		/// The alphatest mode is less.
		/// </summary>
		public const uint AlphatestLess = 2;

		/// <summary>
		/// The alphatest mode is mod e_ mask.
		/// </summary>
		public const uint AlphatestModeMask = 0x1e000000;

		/// <summary>
		/// The alphatest mode is mod e_ shift.
		/// </summary>
		public const int AlphatestModeShift = 25;

		/// <summary>
		/// The alphatest mode is never.
		/// </summary>
		public const uint AlphatestNever = 1;

		/// <summary>
		/// The alphatest mode is notequal.
		/// </summary>
		public const uint AlphatestNotequal = 6;

		/// <summary>
		/// The alph a_ add.
		/// </summary>
		public const uint AlphaAdd = 2;

		/// <summary>
		/// The alph a_ blend.
		/// </summary>
		public const uint AlphaBlend = 4;

		/// <summary>
		/// The alph a_ default.
		/// </summary>
		public const uint AlphaDefault = 5;

		/// <summary>
		/// The alph a_ half.
		/// </summary>
		public const uint AlphaHalf = 1;

		/// <summary>
		/// The alph a_ mod e_ mask.
		/// </summary>
		public const uint AlphaModeMask = 0x00070000;

		/// <summary>
		/// The alph a_ mod e_ shift.
		/// </summary>
		public const int AlphaModeShift = 16;

		/// <summary>
		/// The alph a_ none.
		/// </summary>
		public const uint AlphaNone = 0;

		/// <summary>
		/// The alph a_ sub.
		/// </summary>
		public const uint AlphaSub = 3;

		/// <summary>
		/// The atla s_ materia l_ f.
		/// </summary>
		public const uint AtlasMaterialF = 1 << 9;

		/// <summary>
		/// The blen d_ add.
		/// </summary>
		public const uint BlendAdd = 2;

		/// <summary>
		/// The blen d_ blend.
		/// </summary>
		public const uint BlendBlend = 4;

		/// <summary>
		/// The blen d_ decal.
		/// </summary>
		public const uint BlendDecal = 1;

		/// <summary>
		/// The blen d_ mod e_ mask.
		/// </summary>
		public const uint BlendModeMask = 0x00380000;

		/// <summary>
		/// The blen d_ mod e_ shift.
		/// </summary>
		public const int BlendModeShift = 19;

		/// <summary>
		/// The blen d_ modulate.
		/// </summary>
		public const uint BlendModulate = 0;

		/// <summary>
		/// The blen d_ modulat e_2 x.
		/// </summary>
		public const uint BlendModulate_2X = 5;

		/// <summary>
		/// The blen d_ modulat e_4 x.
		/// </summary>
		public const uint BlendModulate_4X = 6;

		/// <summary>
		/// The blen d_ replace.
		/// </summary>
		public const uint BlendReplace = 3;

		/// <summary>
		/// The clam p_ u v_ f.
		/// </summary>
		public const uint ClampUvF = 1 << 8;

		/// <summary>
		/// The cul l_ back.
		/// </summary>
		public const uint CullBack = 1;

		/// <summary>
		/// The cul l_ front.
		/// </summary>
		public const uint CULL_FRONT = 0;

		/// <summary>
		/// The cul l_ none.
		/// </summary>
		public const uint CullNone = 2;

		/// <summary>
		/// The cull front f.
		/// </summary>
		public const uint CullFrontF = 1 << 4;

		/// <summary>
		/// The dept h_ writ e_ disabled.
		/// </summary>
		public const uint DEPTH_WRITE_DISABLED = 1;

		/// <summary>
		/// The dept h_ writ e_ mod e_ mask.
		/// </summary>
		public const uint DEPTH_WRITE_MODE_MASK = 0x1 << 29;

		/// <summary>
		/// The dept h_ writ e_ mod e_ shift.
		/// </summary>
		public const int DEPTH_WRITE_MODE_SHIFT = 29;

		/// <summary>
		/// The dept h_ writ e_ normal.
		/// </summary>
		public const uint DEPTH_WRITE_NORMAL = 0;

		/// <summary>
		/// The effec t_ constan t_ colou r_ channel.
		/// </summary>
		public const uint EFFECT_CONSTANT_COLOUR_CHANNEL = 4;

		/// <summary>
		/// The effec t_ default.
		/// </summary>
		public const uint EFFECT_DEFAULT = 0;

		/// <summary>
		/// The effec t_ environmen t_ mapping.
		/// </summary>
		public const uint EFFECT_ENVIRONMENT_MAPPING = 3;

		/// <summary>
		/// The effec t_ lightma p_ pos t_ process.
		/// </summary>
		public const uint EFFECT_LIGHTMAP_POST_PROCESS = 5;

		/// <summary>
		/// The effec t_ norma l_ mapping.
		/// </summary>
		public const uint EFFECT_NORMAL_MAPPING = 1;

		/// <summary>
		/// The effec t_ norma l_ mappin g_ specular.
		/// </summary>
		public const uint EFFECT_NORMAL_MAPPING_SPECULAR = 6;

		/// <summary>
		/// The effec t_ prese t_ mask.
		/// </summary>
		public const uint EFFECT_PRESET_MASK = 0x01C00000;

		/// <summary>
		/// The effec t_ prese t_ shift.
		/// </summary>
		public const int EFFECT_PRESET_SHIFT = 22;

		/// <summary>
		/// The effec t_ reflectio n_ mapping.
		/// </summary>
		public const uint EFFECT_REFLECTION_MAPPING = 2;

		/// <summary>
		/// The effec t_ textur e 0_ only.
		/// </summary>
		public const uint EFFECT_TEXTURE0_ONLY = 7;

		/// <summary>
		/// The fla t_ f.
		/// </summary>
		public const uint FLAT_F = 1 << 2;

		/// <summary>
		/// The intensit y_ f.
		/// </summary>
		public const uint INTENSITY_F = 1 << 0;

		/// <summary>
		/// The i n_ us e_ f.
		/// </summary>
		public const uint IN_USE_F = 1 << 12;

		/// <summary>
		/// The merg e_ geo m_ f.
		/// </summary>
		public const uint MERGE_GEOM_F = 1 << 7;

		/// <summary>
		/// The modulat e_ mask.
		/// </summary>
		public const uint MODULATE_MASK = 3;

		/// <summary>
		/// The modulat e_ none.
		/// </summary>
		public const uint MODULATE_NONE = 2;

		/// <summary>
		/// The modulat e_ r.
		/// </summary>
		public const uint MODULATE_R = 1;

		/// <summary>
		/// The modulat e_ rgb.
		/// </summary>
		public const uint MODULATE_RGB = 0;

		/// <summary>
		/// The n o_ filterin g_ f.
		/// </summary>
		public const uint NO_FILTERING_F = 1 << 5;

		/// <summary>
		/// The n o_ fo g_ f.
		/// </summary>
		public const uint NO_FOG_F = 1 << 10;

		/// <summary>
		/// The n o_ rende r_ f.
		/// </summary>
		public const uint NO_RENDER_F = 1 << 6;

		/// <summary>
		/// The shad e_ flat.
		/// </summary>
		public const uint SHADE_FLAT = 0;

		/// <summary>
		/// The shad e_ gouraud.
		/// </summary>
		public const uint SHADE_GOURAUD = 1;

		/// <summary>
		/// The two sided f.
		/// </summary>
		public const uint TwoSidedF = 1 << 3;

		/// <summary>
		/// The unmodulat e_ f.
		/// </summary>
		public const uint UNMODULATE_F = 1 << 1;

		private byte alphaTestValue;

		private byte celH;

		private byte celNum;

		private byte celNumU;

		private byte celPeriod;

		private byte celW;

		private Color colAmbient = Color.FromArgb(128, 128, 128, 255);

		private Color colDiffuse = Color.FromArgb(128, 128, 128, 255);

		private Color colEmissive = Color.FromArgb(0, 0, 0, 255);

		private Color colSpecular = Color.FromArgb(0, 255, 255, 255);

		private uint flags;

		private CIwTexture texture0;

		private CIwTexture texture1;

		private short zDepthOfs;

		private short zDepthOfsHW;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets ALPHATEST_MODE.
		/// </summary>
		public uint ALPHATEST_MODE
		{
			get
			{
				return (this.flags & AlphatestModeMask) >> AlphatestModeShift;
			}
		}

		/// <summary>
		/// Gets ALPHA_MODE.
		/// </summary>
		public uint ALPHA_MODE
		{
			get
			{
				return (this.flags & AlphaModeMask) >> AlphaModeShift;
			}
		}

		/// <summary>
		/// Gets BLEND_MODE.
		/// </summary>
		public uint BLEND_MODE
		{
			get
			{
				return (this.flags & BlendModeMask) >> BlendModeShift;
			}
		}

		/// <summary>
		/// Gets a value indicating whether CullFront.
		/// </summary>
		public bool CullFront
		{
			get
			{
				return (this.flags & CullFrontF) != 0;
			}
		}

		/// <summary>
		/// Gets DEPTH_WRITE_MODE.
		/// </summary>
		public uint DEPTH_WRITE_MODE
		{
			get
			{
				return (this.flags & DEPTH_WRITE_MODE_MASK) >> DEPTH_WRITE_MODE_SHIFT;
			}
		}

		/// <summary>
		/// Gets EFFECT_PRESET.
		/// </summary>
		public uint EFFECT_PRESET
		{
			get
			{
				return (this.flags & EFFECT_PRESET_MASK) >> EFFECT_PRESET_SHIFT;
			}
		}

		/// <summary>
		/// Gets a value indicating whether IsFlat.
		/// </summary>
		public bool IsFlat
		{
			get
			{
				return (this.flags & FLAT_F) != 0;
			}
		}

		/// <summary>
		/// Gets MODULATE.
		/// </summary>
		public uint MODULATE
		{
			get
			{
				return this.flags & MODULATE_MASK;
			}
		}

		/// <summary>
		/// Gets or sets SpecularPower.
		/// </summary>
		public byte SpecularPower
		{
			get
			{
				return this.colSpecular.A;
			}

			set
			{
				this.colSpecular = Color.FromArgb(value, this.colSpecular.R, this.colSpecular.G, this.colSpecular.B);
			}
		}

		/// <summary>
		/// Gets a value indicating whether TwoSided.
		/// </summary>
		public bool TwoSided
		{
			get
			{
				return (this.flags & TwoSidedF) != 0;
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The disable.
		/// </summary>
		public void Disable()
		{
			if (this.texture0 != null)
			{
				this.texture0.Disable(0);
			}

			if (this.texture1 != null)
			{
				this.texture1.Disable(1);
			}

			GL.Disable(EnableCap.Blend);
			GL.DepthMask(true);
			GL.Disable(EnableCap.AlphaTest);
			GL.Disable(EnableCap.CullFace);
		}

		/// <summary>
		/// The enable.
		/// </summary>
		public void Enable()
		{
			if (this.texture0 != null)
			{
				this.texture0.Enable(0);
				switch (this.MODULATE)
				{
					case MODULATE_RGB:
					case MODULATE_R:
						GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)All.Modulate);
						break;
					case MODULATE_NONE:
						GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)All.Replace);
						break;
				}
			}

			if (this.TwoSided)
			{
				GL.Disable(EnableCap.CullFace);
			}
			else
			{
				GL.Enable(EnableCap.CullFace);
				if (this.CullFront)
				{
					GL.CullFace(CullFaceMode.Front);
				}
				else
				{
					GL.CullFace(CullFaceMode.Back);
				}
			}

			switch (this.ALPHA_MODE)
			{
				case AlphaNone:

					// Material is opaque (not transparent).
					GL.Disable(EnableCap.Blend);
					break;
				case AlphaAdd:

					// Material is transparent. Source colour (as calculated by the lighting pipeline) and destination colour (the current colour in the backbuffer) are combined as follows: Colour = (Source + Destination)
					GL.Enable(EnableCap.Blend);
					GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);
					break;
				case AlphaHalf:
					GL.Enable(EnableCap.Blend);

					// TODO: fix this formula
					GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);
					break;
				case AlphaSub:

					// Material is transparent. Source colour (as calculated by the lighting pipeline) and destination colour (the current colour in the backbuffer) are combined as follows: Colour = (Destination - Source)
					GL.Enable(EnableCap.Blend);
					GL.BlendFunc(BlendingFactorSrc.OneMinusDstColor, BlendingFactorDest.DstColor);
					break;
				case AlphaBlend:

					// Material is transparent. Source colour (as calculated by the lighting pipeline) and destination colour (the current colour in the backbuffer) are combined as follows: Colour = (Source * SourceAlpha) + (Destination * (1 - SourceAlpha))
					GL.Enable(EnableCap.Blend);
					GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
					break;
				case AlphaDefault:
					GL.Disable(EnableCap.Blend);
					break;
			}

			switch (this.ALPHATEST_MODE)
			{
				case AlphatestDisabled:
					GL.Disable(EnableCap.AlphaTest);
					break;
				case AlphatestLess:
					GL.Enable(EnableCap.AlphaTest);
					GL.AlphaFunc(AlphaFunction.Less, this.alphaTestValue / 255.0f);
					break;
				case AlphatestLequal:
					GL.Enable(EnableCap.AlphaTest);
					GL.AlphaFunc(AlphaFunction.Lequal, this.alphaTestValue / 255.0f);
					break;
				case AlphatestGreater:
					GL.Enable(EnableCap.AlphaTest);
					GL.AlphaFunc(AlphaFunction.Greater, this.alphaTestValue / 255.0f);
					break;
				case AlphatestGequal:
					GL.Enable(EnableCap.AlphaTest);
					GL.AlphaFunc(AlphaFunction.Gequal, this.alphaTestValue / 255.0f);
					break;
				case AlphatestNever:
					GL.Enable(EnableCap.AlphaTest);
					GL.AlphaFunc(AlphaFunction.Never, this.alphaTestValue / 255.0f);
					break;
				case AlphatestAlways:
					GL.Enable(EnableCap.AlphaTest);
					GL.AlphaFunc(AlphaFunction.Always, this.alphaTestValue / 255.0f);
					break;
				case AlphatestEqual:
					GL.Enable(EnableCap.AlphaTest);
					GL.AlphaFunc(AlphaFunction.Equal, this.alphaTestValue / 255.0f);
					break;
				case AlphatestNotequal:
					GL.Enable(EnableCap.AlphaTest);
					GL.AlphaFunc(AlphaFunction.Notequal, this.alphaTestValue / 255.0f);
					break;
			}
			if (this.IsFlat)
			{
				GL.ShadeModel(ShadingModel.Flat);
			}
			else
			{
				GL.ShadeModel(ShadingModel.Smooth);
			}
			if (this.texture1 != null)
			{
				if (this.texture1 != null)
				{
					this.texture1.Enable(1);
				}

				switch (this.BLEND_MODE)
				{
					case BlendModulate:
						GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)All.Modulate);
						break;
					case BlendDecal:
						GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)All.Decal);
						break;
					case BlendAdd:
						GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)All.Add);
						break;
					case BlendReplace:
						GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)All.Replace);
						break;
					case BlendBlend:
						GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)All.Blend);
						break;
					case BlendModulate_2X:
						GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)All.Modulate);
						break;
					case BlendModulate_4X:
						GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)All.Modulate);
						break;
				}
			}

			switch (this.DEPTH_WRITE_MODE)
			{
				case DEPTH_WRITE_DISABLED:
					GL.DepthMask(false);
					break;
				case DEPTH_WRITE_NORMAL:
					GL.DepthMask(true);
					break;
			}

			GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, this.colAmbient);
			GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, this.colDiffuse);
			GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, this.colEmissive);
			GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, this.colSpecular);
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

			bool someFlag = false;
			serialise.Bool(ref someFlag);
			{
				serialise.UInt32(ref this.flags);
			}

			if (!someFlag)
			{
				{
					serialise.Int16(ref this.zDepthOfs);
				}
				{
					serialise.Int16(ref this.zDepthOfsHW);
				}
				{
					serialise.Colour(ref this.colEmissive);
				}
				{
					serialise.Colour(ref this.colAmbient);
				}
				{
					serialise.Colour(ref this.colDiffuse);
				}
				{
					serialise.Colour(ref this.colSpecular);
				}
				{
					uint val = 4;
					serialise.UInt32(ref val);
				}
			}
			{
				// Texture is always presented
				CIwManaged t = this.texture0;
				serialise.ManagedHash("CIwTexture".ToeHash(), ref t);
				this.texture0 = (CIwTexture)t;
			}

	

			if (!someFlag)
			{
				CIwManaged t = this.texture1;
				serialise.ManagedHash("CIwTexture".ToeHash(), ref t);
				this.texture1 = (CIwTexture)t;
				{
					CIwManaged val = null;
					serialise.ManagedHash("?".ToeHash(), ref val);
				}
				{
					CIwManaged val = null;
					serialise.ManagedHash("?".ToeHash(), ref val);
				}

				byte isAnimated = 0;
				serialise.UInt8(ref isAnimated);
				if (isAnimated != 0)
				{
					{
						byte value = 0;
						serialise.UInt8(ref value);
					}

					serialise.UInt8(ref this.celNum);
					serialise.UInt8(ref this.celNumU);
					serialise.UInt8(ref this.celW);
					serialise.UInt8(ref this.celH);
					serialise.UInt8(ref this.celPeriod);
				}

				serialise.UInt8(ref this.alphaTestValue);
				{
					CIwManaged val = null;
					serialise.ManagedHash("?".ToeHash(), ref val);
				}
			}
		}

		#endregion
	}
}