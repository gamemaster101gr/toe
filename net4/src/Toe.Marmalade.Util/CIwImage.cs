using System;
using System.Drawing;
using System.Globalization;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Toe.Marmalade.Util
{
	/// <summary>
	/// CIwImage is a storage, loading and manipulation class for a range of image formats.
	/// </summary>
	public class CIwImage
	{
public const uint PALETTISED_4BIT_F   = (1 << 0);
public const uint         PALETTISED_5BIT_F   = (1 << 1);
        public const uint PALETTISED_6BIT_F   = (1 << 2);
        public const uint PALETTISED_7BIT_F   = (1 << 3);
        public const uint PALETTISED_8BIT_F   = (1 << 4);

		public const uint PALETTISED_MASK =
			PALETTISED_4BIT_F | PALETTISED_5BIT_F | PALETTISED_6BIT_F | PALETTISED_7BIT_F | PALETTISED_8BIT_F;

        public const uint ALPHA_F             = (1 << 5); // if set, has alpha (i.e. RGBA or ABGR).
        public const uint REVERSE_F           = (1 << 6); // if set, is reverse order (i.e. BGR or ABGR).

        public const uint SIZE_8_F            = (1 << 7); // Size of texel (for unpalettised) or palette entry (for palettised)
        public const uint SIZE_16_F           = (2 << 7); // Size of texel (for unpalettised) or palette entry (for palettised)
        public const uint SIZE_24_F           = (3 << 7); // Size of texel (for unpalettised) or palette entry (for palettised)
        public const uint SIZE_32_F           = (4 << 7); // Size of texel (for unpalettised) or palette entry (for palettised)
        public const uint SIZE_MASK           = (0x7 << 7);

        public const uint ALPHA_FLIP_F        = (1 << 10);

        public const uint NON_PALETTE_ALPHA_F = (1 << 11); // if set, has alpha but stored in texels not in palette (for DS A5I3 and A3I5)

		public const byte FORMAT_UNDEFINED = 0;       //!< Format is undefined.

        public const byte RGB_332 = 1;   //!< Unpalettised 8-bit.
        public const byte BGR_332 = 2;   //!< Unpalettised 8-bit.


        public const byte RGB_565 = 3;   //!< Unpalettised 16-bit = ;no alpha.
        public const byte BGR_565 = 4;   //!< Unpalettised 16-bit = ;no alpha.


        public const byte RGBA_4444 = 5; //!< Unpalettised 16-bit = ;alpha.
        public const byte ABGR_4444 = 6; //!< Unpalettised 16-bit = ;alpha.
        public const byte RGBA_5551 = 7; //!< Unpalettised 16-bit = ;alpha.
        public const byte ABGR_1555 = 8; //!< Unpalettised 16-bit = ;alpha.


        public const byte RGB_888 = 9;   //!< Unpalettised 24-bit = ;no alpha.
        public const byte BGR_888 = 0xA;   //!< Unpalettised 24-bit = 0x;no alpha.


        public const byte RGBA_6666 = 0xB; //!< Unpalettised 24-bit = 0x;alpha.
        public const byte ABGR_6666 = 0xC; //!< Unpalettised 24-bit = 0x;alpha.


        public const byte RGBA_8888 = 0xD; //!< Unpalettised 32-bit = 0x;alpha.
        public const byte ABGR_8888 = 0xE; //!< Unpalettised 32-bit = 0x;alpha.
        public const byte RGBA_AAA2 = 0xF; //!< Unpalettised 32-bit = 0x;alpha.
        public const byte ABGR_2AAA = 0x10; //!< Unpalettised 32-bit = 0x;alpha.


        public const byte PALETTE4_RGB_888 = 0x11;  //!< 16-colour palettised.
        public const byte PALETTE4_RGBA_8888 = 0x12;//!< 16-colour palettised.
        public const byte PALETTE4_RGB_565 = 0x13;  //!< 16-colour palettised.
        public const byte PALETTE4_RGBA_4444 = 0x14;//!< 16-colour palettised.
        public const byte PALETTE4_RGBA_5551 = 0x15;//!< 16-colour palettised.

		public const byte PALETTE4_ABGR_1555 = 0x16;

        public const byte PALETTE8_RGB_888 = 0x17;  //!< 256-colour palettised.
        public const byte PALETTE8_RGBA_8888 = 0x18;//!< 256-colour palettised.
        public const byte PALETTE8_RGB_565 = 0x19;  //!< 256-colour palettised.
        public const byte PALETTE8_RGBA_4444 = 0x1A;//!< 256-colour palettised.
        public const byte PALETTE8_RGBA_5551 = 0x1B;//!< 256-colour palettised.

		public const byte PALETTE8_ABGR_1555 = 0x1C;

        public const byte PALETTE7_ABGR_1555 = 0x1D;//!< 32-colour palettised.
        public const byte PALETTE6_ABGR_1555 = 0x1E;//!< 64-colour palettised.
        public const byte PALETTE5_ABGR_1555 = 0x1F;//!< 128-colour palettised.

        // PVRTC formats
        public const byte PVRTC_2 = 0x20;           //!< PowerVR compressed format.
        public const byte PVRTC_4 = 0x21;           //!< PowerVR compressed format.
        public const byte ATITC = 0x22;             //!< ATI compressed format.
        public const byte COMPRESSED = 0x23;        //!< gfx specific compressed format

        public const byte PALETTE4_ABGR_4444 = 0x24;//!< 16-colour palettised.
        public const byte PALETTE8_ABGR_4444 = 0x25;//!< 256-colour palettised.

        public const byte A_8 = 0x26;               //!< Unpalettised 8-bit alpha.

        public const byte ETC = 0x27;               //!< Ericsson compressed format
        public const byte ARGB_8888 = 0x28;         //!< Unpalettised 32-bit = 0x;alpha.

        public const byte PALETTE4_ARGB_8888 = 0x29;//!< 16-colour palettised.
        public const byte PALETTE8_ARGB_8888 = 0x2A;//!< 256-colour palettised.

        public const byte DXT3 = 0x2B;              //!< DXT3 compressed format

        public const byte PALETTE4_BGR555 = 0x2C;       //!< 16-colour palettised.
        public const byte PALETTE8_BGR555 = 0x2D;       //!< 16-colour palettised.
        public const byte A5_PALETTE3_BGR_555 = 0x2E;   //!< 8BPP = 0x;of which 5 are alpha and 3 are palette index
        public const byte A3_PALETTE5_BGR_555 = 0x2F;   //!< 8BPP = 0x;of which 3 are alpha and 5 are palette index

        public const byte PALETTE4_BGR_565 = 0x30;  //!< 16-colour palettised.
        public const byte PALETTE4_ABGR_8888 = 0x31;//!< 16-colour palettised.
        public const byte PALETTE8_BGR_565 = 0x32;  //!< 256-colour palettised.
        public const byte PALETTE8_ABGR_8888 = 0x33;//!< 256-colour palettised.

        public const byte DXT1 = 0x34;              //!< DXT1 compressed format
        public const byte DXT5 = 0x35;              //!< DXT5 compressed format

        public const byte FORMAT_MAX = 0x36;        // (Terminator)


		ushort width;
		ushort height;
		ushort pitch;
		private ushort flags;
		private byte format = 0xA;

		private byte[] data;

		public int Width
		{
			get
			{
				return width;
			}
		}
		public int Height
		{
			get
			{
				return height;
			}
		}
		public byte[] Pixels
		{
			get
			{
				return this.data;
			}
		}
		public void Serialise(IwSerialise serialise)
		{
			serialise.UInt8(ref format);

			serialise.UInt16(ref flags);

			serialise.UInt16(ref width);
			serialise.UInt16(ref height);
			serialise.UInt16(ref pitch);
			uint palette = 0;
			serialise.UInt32(ref palette);
			switch (format)
			{
				case ABGR_8888:
				case BGR_888:
					LoadUncompressed(serialise);
					break;
				case PALETTE8_RGB_888:
					Load256ColourPalettised(serialise);
					break;
				default:
					throw new FormatException(string.Format(CultureInfo.CurrentCulture, "Unknown image format 0x{0:x}", this.format));
			}
		}

		private Color[] palette;
		private void Load256ColourPalettised(IwSerialise serialise)
		{
			
			data = new byte[height * pitch];
			serialise.Serialise(ref data);

			palette = new Color[256];
			for (int i = 0; i < palette.Length; ++i)
			{
				byte r = 0;
				byte g = 0;
				byte b = 0;
				serialise.UInt8(ref r);
				serialise.UInt8(ref g);
				serialise.UInt8(ref b);
				palette[i] = Color.FromArgb(r, g, b);
			}

			byte[] d = new byte[height*width*3];
			int j = 0;
			for (int y=0;y<height;++y)
				for (int x = 0; x < width; ++x)
				{
					d[j] = palette[data[x + y * pitch]].R;
					++j;
					d[j] = palette[data[x + y * pitch]].G;
					++j;
					d[j] = palette[data[x + y * pitch]].B;
					++j;
				}
			data = d;

			////using (var b = new Bitmap(width,height))
			////{
			////        for (int i = 0; i < height;++i)
			////            for (int x = 0; x < width; ++x)
			////            {
			////                b.SetPixel(x,i,palette[data[x+i*pitch]]);
			////            }
			////    b.Save("res.bmp");
			////}
		}

		private void LoadUncompressed(IwSerialise serialise)
		{
			data = new byte[height * pitch];
			serialise.Serialise(ref data);
		}

		public void Upload()
		{
			GL.TexImage2D<byte>(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, Width, Height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, Pixels);
			S3E.CheckOpenGLStatus();
		}
	}
}