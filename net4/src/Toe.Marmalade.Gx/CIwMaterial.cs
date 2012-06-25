using System.Drawing;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Gx
{
	/// <summary>
	/// The c iw material.
	/// </summary>
	public class CIwMaterial : CIwResource
	{
		private uint flags = 0;

		private CIwTexture texture0;

		private CIwManaged texture1;

		private short zDepthOfsHW;

		private Color colEmissive;

		private Color colAmbient = Color.FromArgb(255,255,255,255);

		private Color colDiffuse = Color.FromArgb(255, 255, 255, 255);

		private Color colSpecular = Color.FromArgb(0, 255, 255, 255);

		private short zDepthOfs;

		private byte celNum;

		private byte celNumU;

		private byte celW;

		private byte celH;

		private byte celPeriod;

		private byte alphaTestValue;

		public const uint SHADE_FLAT = 0;
		public const uint SHADE_GOURAUD = 1;

		public const uint MODULATE_RGB = 0;

		public const uint MODULATE_R = 1;
		public const uint MODULATE_NONE = 2;
		public const uint MODULATE_MASK = 3;

		public const uint CULL_FRONT = 0;
		public const uint CULL_BACK = 1;
		public const uint CULL_NONE = 2;

		public const uint ALPHA_NONE = 0;
		public const uint ALPHA_HALF = 1;
		public const uint ALPHA_ADD = 2;
		public const uint ALPHA_SUB = 3;
		public const uint ALPHA_BLEND = 4;
		public const uint ALPHA_DEFAULT = 5;

		public const uint ALPHATEST_DISABLED = 0;
		public const uint ALPHATEST_NEVER = 1;
		public const uint ALPHATEST_LESS = 2;
		public const uint ALPHATEST_EQUAL = 3;
		public const uint ALPHATEST_LEQUAL = 4;
		public const uint ALPHATEST_GREATER = 5;
		public const uint ALPHATEST_NOTEQUAL = 6;
		public const uint ALPHATEST_GEQUAL = 7;
		public const uint ALPHATEST_ALWAYS = 8;

		public const uint BLEND_MODULATE = 0;
		public const uint BLEND_DECAL = 1;
		public const uint BLEND_ADD = 2;
		public const uint BLEND_REPLACE = 3;
		public const uint BLEND_BLEND = 4;
		public const uint BLEND_MODULATE_2X = 5;
		public const uint BLEND_MODULATE_4X = 6;

		public const uint EFFECT_DEFAULT = 0;
		public const uint EFFECT_NORMAL_MAPPING = 1;
		public const uint EFFECT_REFLECTION_MAPPING = 2;
		public const uint EFFECT_ENVIRONMENT_MAPPING = 3;
		public const uint EFFECT_CONSTANT_COLOUR_CHANNEL = 4;
		public const uint EFFECT_LIGHTMAP_POST_PROCESS = 5;
		public const uint EFFECT_NORMAL_MAPPING_SPECULAR = 6;
		public const uint EFFECT_TEXTURE0_ONLY = 7;

		public const uint DEPTH_WRITE_NORMAL = 0;
		public const uint DEPTH_WRITE_DISABLED = 0;

		public const uint INTENSITY_F = (1<<0);
		public const uint UNMODULATE_F = (1 << 1);
		public const uint FLAT_F = (1 << 2);
		public const uint TWO_SIDED_F = (1 << 3);
		public const uint CULL_FRONT_F = (1 << 4);
		public const uint NO_FILTERING_F = (1 << 5);
		public const uint NO_RENDER_F = (1 << 6);
		public const uint MERGE_GEOM_F = (1 << 7);
		public const uint CLAMP_UV_F = (1 << 8);
		public const uint ATLAS_MATERIAL_F = (1 << 9);
		public const uint NO_FOG_F = (1 << 10);

		public const uint IN_USE_F = (1 << 12);

		public const uint ALPHA_MODE_SHIFT = 16;
		public const uint ALPHA_MODE_MASK = 0x00070000;
		public const uint BLEND_MODE_SHIFT = 19;
		public const uint BLEND_MODE_MASK = 0x00380000;
		public const uint EFFECT_PRESET_SHIFT = 22;
		public const uint EFFECT_PRESET_MASK = 0x01C00000;
		public const uint ALPHATEST_MODE_SHIFT = 25;
		public const uint ALPHATEST_MODE_MASK = 0x1e000000;
		public const uint DEPTH_WRITE_MODE_SHIFT = 29;
		public const uint DEPTH_WRITE_MODE_MASK = 0x1 << 29;

		public byte SpecularPower
		{
			get
			{
				return colSpecular.A;
			}
			set
			{
				colSpecular = Color.FromArgb(value, colSpecular.R, colSpecular.G, colSpecular.B);
			}
		}

		public override void Serialise(IwSerialise serialise)
		{
			base.Serialise(serialise);

			bool someFlag = false;
			serialise.Bool(ref someFlag);

			{
				serialise.UInt32(ref flags);
			}

			if (!someFlag)
			{
				{
					serialise.Int16(ref zDepthOfs);
				}
				{
					serialise.Int16(ref zDepthOfsHW);
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

			// Texture is always presented
			{	
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

					serialise.UInt8(ref celNum);
					serialise.UInt8(ref celNumU);
					serialise.UInt8(ref celW);
					serialise.UInt8(ref celH);
					serialise.UInt8(ref celPeriod);
				}

				serialise.UInt8(ref alphaTestValue);

				{
					CIwManaged val = null;
					serialise.ManagedHash("?".ToeHash(), ref val);
				}
			}
		}
	}
}