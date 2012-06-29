using System;
using System.Text;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Gx
{
	/// <summary>
	/// A shader technique.
	/// </summary>
	public class CIwGxShaderTechnique : CIwResource
	{
		#region Constants and Fields

		private readonly CIwArray<CIwGxShaderUniform> shaderParams = new CIwArray<CIwGxShaderUniform>();

		private string fragmentShader;

		private string vertexShader;

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The serialise len str z.
		/// </summary>
		/// <param name="serialise">
		/// The serialise.
		/// </param>
		/// <param name="str">
		/// The str.
		/// </param>
		/// <exception cref="NotImplementedException">
		/// </exception>
		public static void SerialiseLenStrZ(IwSerialise serialise, ref string str)
		{
			if (serialise.IsReading())
			{
				int len = 0;
				serialise.Int32(ref len);
				byte[] b = new byte[len + 1];
				serialise.Serialise(ref b);
				str = Encoding.ASCII.GetString(b);
			}
			else
			{
				throw new NotImplementedException();
			}
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
			SerialiseLenStrZ(serialise, ref this.vertexShader);
			SerialiseLenStrZ(serialise, ref this.fragmentShader);

			this.shaderParams.SerialiseHeader(serialise);

			for (int i = 0; i < this.shaderParams.Size; ++i)
			{
				if (this.shaderParams[i] == null)
				{
					this.shaderParams[i] = new CIwGxShaderUniform();
				}

				this.shaderParams[i].Serialise(serialise);
			}
		}

		#endregion

		// char* m_ShaderSource[2];
		// int32 m_ShaderSourceLen[2];

		// CIwGxPlatformShader * m_ProgramID;
		// CIwGxDefaultUniformParams* m_pUniformLocations;
		// bool m_Enabled;
	}

	/// <summary>
	/// The c iw gx shader uniform.
	/// </summary>
	public class CIwGxShaderUniform
	{
		#region Constants and Fields

		/// <summary>
		/// The componen t_1.
		/// </summary>
		public const uint COMPONENT_1 = 0;

		/// <summary>
		/// The componen t_2.
		/// </summary>
		public const uint COMPONENT_2 = 1;

		/// <summary>
		/// The componen t_3.
		/// </summary>
		public const uint COMPONENT_3 = 2;

		/// <summary>
		/// The componen t_4.
		/// </summary>
		public const uint COMPONENT_4 = 3;

		/// <summary>
		/// The float.
		/// </summary>
		public const uint FLOAT = TYPE_FLOAT | COMPONENT_1; // !< 'float' GLSL type (or equivalent)

		/// <summary>
		/// The int.
		/// </summary>
		public const uint INT = TYPE_INT | COMPONENT_1; // !< 'int' GLSL type (or equivalent)

		/// <summary>
		/// The ive c 2.
		/// </summary>
		public const uint IVEC2 = TYPE_INT | COMPONENT_2; // !< 'ivec2' GLSL type (or equivalent)

		/// <summary>
		/// The ive c 3.
		/// </summary>
		public const uint IVEC3 = TYPE_INT | COMPONENT_3; // !< 'ivec3' GLSL type (or equivalent)

		/// <summary>
		/// The ive c 4.
		/// </summary>
		public const uint IVEC4 = TYPE_INT | COMPONENT_4; // !< 'ivec4' GLSL type (or equivalent)

		/// <summary>
		/// The ma t 2.
		/// </summary>
		public const uint MAT2 = TYPE_MAT | COMPONENT_2; // !< 'mat2' GLSL type (or equivalent)

		/// <summary>
		/// The ma t 3.
		/// </summary>
		public const uint MAT3 = TYPE_MAT | COMPONENT_3; // !< 'mat3' GLSL type (or equivalent)

		/// <summary>
		/// The ma t 4.
		/// </summary>
		public const uint MAT4 = TYPE_MAT | COMPONENT_4; // !< 'mat4' GLSL type (or equivalent)

		/// <summary>
		/// The typ e_ float.
		/// </summary>
		public const uint TYPE_FLOAT = 1 << 2;

		/// <summary>
		/// The typ e_ int.
		/// </summary>
		public const uint TYPE_INT = 2 << 2;

		/// <summary>
		/// The typ e_ mask.
		/// </summary>
		public const uint TYPE_MASK = 0xffff << 2;

		/// <summary>
		/// The typ e_ mat.
		/// </summary>
		public const uint TYPE_MAT = 3 << 2;

		/// <summary>
		/// The ve c 2.
		/// </summary>
		public const uint VEC2 = TYPE_FLOAT | COMPONENT_2; // !< 'vec2' GLSL type (or equivalent)

		/// <summary>
		/// The ve c 3.
		/// </summary>
		public const uint VEC3 = TYPE_FLOAT | COMPONENT_3; // !< 'vec3' GLSL type (or equivalent)

		/// <summary>
		/// The ve c 4.
		/// </summary>
		public const uint VEC4 = TYPE_FLOAT | COMPONENT_4; // !< 'vec4' GLSL type (or equivalent)

		private int arraySize;

		private uint flags;

		private float[] floatData;

		private string name;

		private uint type;

		#endregion

		#region Properties

		private bool IsFloat
		{
			get
			{
				return (this.type & TYPE_MASK) == TYPE_FLOAT;
			}
		}

		private bool IsInt
		{
			get
			{
				return (this.type & TYPE_MASK) == TYPE_INT;
			}
		}

		private bool IsMat
		{
			get
			{
				return (this.type & TYPE_MASK) == TYPE_MAT;
			}
		}

		private uint NumberOfComponents
		{
			get
			{
				return (this.type & ~TYPE_MASK) + 1;
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The serialise len str.
		/// </summary>
		/// <param name="serialise">
		/// The serialise.
		/// </param>
		/// <param name="str">
		/// The str.
		/// </param>
		/// <exception cref="NotImplementedException">
		/// </exception>
		public static void SerialiseLenStr(IwSerialise serialise, ref string str)
		{
			if (serialise.IsReading())
			{
				int len = 0;
				serialise.Int32(ref len);
				byte[] b = new byte[len];
				serialise.Serialise(ref b);
				str = Encoding.ASCII.GetString(b);
			}
			else
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// The serialise.
		/// </summary>
		/// <param name="serialise">
		/// The serialise.
		/// </param>
		/// <exception cref="NotImplementedException">
		/// </exception>
		/// <exception cref="NotImplementedException">
		/// </exception>
		public void Serialise(IwSerialise serialise)
		{
			SerialiseLenStr(serialise, ref this.name);
			serialise.UInt32(ref this.type);
			serialise.Int32(ref this.arraySize);
			serialise.UInt32(ref this.flags);
			if (serialise.IsReading())
			{
				if (this.IsFloat)
				{
					this.floatData = new float[this.arraySize * this.NumberOfComponents];
					serialise.Serialise(ref this.floatData);
				}
				else
				{
					throw new NotImplementedException();
				}
			}
			else
			{
				throw new NotImplementedException();
			}
		}

		#endregion
	}
}

////0x00000006
////    Toe.App!IwSerialiseUInt32()  + 0x110 bytes	
////    Toe.App!CIwGxShaderUniform::Serialise()  + 0x77 bytes	
////    Toe.App!CIwGxShaderTechnique::Serialise()  + 0x18c bytes	
////0x00000001
////    Toe.App!IwSerialiseInt32()  + 0x110 bytes	
////    Toe.App!CIwGxShaderUniform::Serialise()  + 0x86 bytes	
////    Toe.App!CIwGxShaderTechnique::Serialise()  + 0x18c bytes	
////0x00000001
////    Toe.App!IwSerialiseUInt32()  + 0x110 bytes	
////    Toe.App!CIwGxShaderUniform::Serialise()  + 0x95 bytes	
////    Toe.App!CIwGxShaderTechnique::Serialise()  + 0x18c bytes
////0x00000000 4,3
////    Toe.App!IwSerialiseFloat()  + 0x110 bytes	
////    Toe.App!CIwGxShaderUniform::Serialise()  + 0x11f bytes	
////    Toe.App!CIwGxShaderTechnique::Serialise()  + 0x18c bytes	

////0x0000000b
////    Toe.App!IwSerialiseInt32()  + 0x110 bytes	
////    Toe.App!CIwGxShaderUniform::Serialise()  + 0x34 bytes	
////    Toe.App!CIwGxShaderTechnique::Serialise()  + 0x18c bytes	
////0x56546e69 - 1,b
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwGxShaderUniform::Serialise()  + 0x65 bytes	
////    Toe.App!CIwGxShaderTechnique::Serialise()  + 0x18c bytes	
////0x00000006
////    Toe.App!IwSerialiseUInt32()  + 0x110 bytes	
////    Toe.App!CIwGxShaderUniform::Serialise()  + 0x77 bytes	
////    Toe.App!CIwGxShaderTechnique::Serialise()  + 0x18c bytes	
////0x00000003
////    Toe.App!IwSerialiseInt32()  + 0x110 bytes	
////    Toe.App!CIwGxShaderUniform::Serialise()  + 0x86 bytes	
////    Toe.App!CIwGxShaderTechnique::Serialise()  + 0x18c bytes	
////1
////    Toe.App!IwSerialiseUInt32()  + 0x110 bytes	
////    Toe.App!CIwGxShaderUniform::Serialise()  + 0x95 bytes	
////    Toe.App!CIwGxShaderTechnique::Serialise()  + 0x18c bytes	
////0x00000000 4,9
////    Toe.App!IwSerialiseFloat()  + 0x110 bytes	
////    Toe.App!CIwGxShaderUniform::Serialise()  + 0x11f bytes	
////    Toe.App!CIwGxShaderTechnique::Serialise()  + 0x18c bytes	

////0x0000000a
////    Toe.App!IwSerialiseInt32()  + 0x110 bytes	
////    Toe.App!CIwGxShaderUniform::Serialise()  + 0x34 bytes	
////    Toe.App!CIwGxShaderTechnique::Serialise()  + 0x18c bytes	
////0x56546e69 - 1,a
////    Toe.App!IwSerialiseUInt8()  + 0x109 bytes	
////    Toe.App!CIwGxShaderUniform::Serialise()  + 0x65 bytes	
////    Toe.App!CIwGxShaderTechnique::Serialise()  + 0x18c bytes	
////4
////    Toe.App!IwSerialiseUInt32()  + 0x110 bytes	
////    Toe.App!CIwGxShaderUniform::Serialise()  + 0x77 bytes	
////    Toe.App!CIwGxShaderTechnique::Serialise()  + 0x18c bytes	
////1
////    Toe.App!IwSerialiseInt32()  + 0x110 bytes	
////    Toe.App!CIwGxShaderUniform::Serialise()  + 0x86 bytes	
////    Toe.App!CIwGxShaderTechnique::Serialise()  + 0x18c bytes	
////1
////    Toe.App!IwSerialiseUInt32()  + 0x110 bytes	
////    Toe.App!CIwGxShaderUniform::Serialise()  + 0x95 bytes	
////    Toe.App!CIwGxShaderTechnique::Serialise()  + 0x18c bytes
////0x00000000	
////    Toe.App!IwSerialiseFloat()  + 0x110 bytes	
////    Toe.App!CIwGxShaderUniform::Serialise()  + 0x11f bytes	
////    Toe.App!CIwGxShaderTechnique::Serialise()  + 0x18c bytes	