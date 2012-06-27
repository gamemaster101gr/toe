using System;
using System.Diagnostics;

using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model ext sel set face.
	/// </summary>
	public class CIwModelExtSelSetFace : CIwModelExtSelSet
	{
		public override void Serialise(Util.IwSerialise serialise)
		{
			base.Serialise(serialise);

			serialise.DebugWrite(256);

			//serialise.UInt8(ref m_Flags);
			serialise.Bool(ref m_WorldSet);
			serialise.UInt8(ref m_FlagsSW);
			serialise.UInt8(ref m_FlagsHW);
			serialise.UInt32(ref m_NumFaces);
			serialise.Int8(ref m_OTZOfsSW);
			m_FaceIDs.SerialiseHeader(serialise);
			m_FaceIDs.ForEach(
				(ref ushort v)=> { serialise.Serialise(ref v);
				                 	return true;
				}
			);

			serialise.DebugWrite(256);
		}

		
		     byte               m_Flags;        //!< CIwFace flags - general.
			byte               m_FlagsSW;      //!< CIwFace flags - SW.
			byte               m_FlagsHW;      //!< CIwFace flags - HW.
			sbyte                m_OTZOfsSW;     //!< OTZ offset for SW.
			uint              m_NumFaces;     //!< Number of faces in model this set applies to
			CIwArray<ushort>    m_FaceIDs = new CIwArray<ushort>();      //!< IDs into model builder face array.
			bool                m_WorldSet;     //!< True if this set is a world file only set

		 
	}
}