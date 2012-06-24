using Toe.Marmalade.Util;

namespace Toe.Marmalade.Gx
{
	/// <summary>
	/// The c iw material.
	/// </summary>
	public class CIwMaterial : CIwResource
	{
		private uint flags = 0;

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

		public override void Serialise(IwSerialise serialise)
		{
			base.Serialise(serialise);

			{
				bool val = false;
				serialise.Bool(ref val);
			}
			{
				serialise.UInt32(ref flags);
			}

////            0x00010000 
////        m_ZDepthOfs	0x0000	short

////    Toe.App!IwSerialiseInt16()  + 0x10c bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x2ac bytes	

////0x80010001
////        m_ZDepthOfsHW	0x0001	short

////    Toe.App!IwSerialiseInt16()  + 0x10c bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x2ca bytes	

////+		m_ColEmissive	{r='' g='Ђ' b='я' ...}	CIwColour

////1
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x2dc bytes	

////0x80
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x2eb bytes	
////ff
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x2fa bytes	
////ff
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x2fa bytes	

////2
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x2dc bytes	

////0x80
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x2eb bytes	
////ff
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x2fa bytes	
////ff
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x2fa bytes	

////3 CULL_NONE
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x2dc bytes	

////0x80
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x2eb bytes	
////ff
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x2fa bytes	
////ff
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x2fa bytes	


////4
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x2dc bytes	

////0xff
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x2eb bytes	
////ff
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x2fa bytes	
////ff
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x2fa bytes	


////0x0a
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x2dc bytes	

////4
////    Toe.App!IwSerialiseUInt32()  + 0x110 bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x3fe bytes	

////0xd9794596
////    Toe.App!IwSerialiseUInt32()  + 0x110 bytes	
////    Toe.App!IwSerialiseManagedHash()  + 0x10d bytes	
////0x1ce10fc5
////    Toe.App!IwSerialiseUInt32()  + 0x110 bytes	
////    Toe.App!IwSerialiseManagedHash()  + 0x10d bytes	
////0x00000000
////    Toe.App!IwSerialiseUInt32()  + 0x110 bytes	
////    Toe.App!IwSerialiseManagedHash()  + 0x10d bytes	
////0x00000000
////    Toe.App!IwSerialiseUInt32()  + 0x110 bytes	
////    Toe.App!IwSerialiseManagedHash()  + 0x10d bytes	
////1
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x546 bytes	
////0
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMatAnim::Serialise()  + 0x23 bytes	
////20
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMatAnim::Serialise()  + 0x32 bytes	
////8
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMatAnim::Serialise()  + 0x41 bytes	
////4
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMatAnim::Serialise()  + 0x50 bytes	
////10
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMatAnim::Serialise()  + 0x62 bytes	
////1
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMatAnim::Serialise()  + 0x71 bytes	
////64
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwMaterial::Serialise()  + 0x570 bytes	
////0
////    Toe.App!IwSerialiseUInt32()  + 0x110 bytes	
////    Toe.App!IwSerialiseManagedHash()  + 0x10d bytes	

			//{
			//    CIwManaged val = null;
			//    serialise.ManagedHash(ref val);
			//}
		}
	}
}