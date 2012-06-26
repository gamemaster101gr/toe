using Toe.Marmalade.Geom;
using Toe.Marmalade.Gx;
using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model.
	/// </summary>
	public class CIwModel: CIwResource
	{
		CIwManagedList list = new CIwManagedList();

		private CIwMaterial[] materials;
		private CIwSphere sphere = new CIwSphere();
		public override void Serialise(IwSerialise serialise)
		{
			base.Serialise(serialise);

			{
				uint val = 0x00020000;
				serialise.UInt32(ref val);
			}
			{
				ushort val = 4;
				serialise.UInt16(ref val);
			}
			{
				ushort val = 4;
				serialise.UInt16(ref val);
			}
			sphere.Serialise(serialise);
			list.Serialise(serialise);

			if (serialise.IsReading())
			{
				uint numMaterials = 0;
				serialise.UInt32(ref numMaterials);
				materials = new CIwMaterial[numMaterials];
				for (int i = 0; i < materials.Length; ++i)
				{
					CIwManaged m = null;
					serialise.ManagedHash("CIwMaterial".ToeHash(), ref m);
					materials[i] = (CIwMaterial)m;
				}
			}
			else
			{
				uint numMaterials = (uint)materials.Length;
				serialise.UInt32(ref numMaterials);
				for (int i = 0; i < materials.Length; ++i)
				{
					CIwManaged m = materials[i];
					serialise.ManagedHash("CIwMaterial".ToeHash(), ref m);
				}
			}
		}

		public void Render()
		{
			uint flags = 0;
			for (int i = 0; i < list.Size; ++i) ((CIwModelBlock)list[i]).Render(this, flags);
		}

		public T ResolveBlock<T>() where T : class
		{
			for (int i = 0; i < list.Size; ++i)
			{
				var b = list[i] as T;
				if (b != null) return b;
			}

			return null;
		}

		public CIwMaterial GetMaterial(uint index)
		{
			if (materials == null) 
				return null;
			if (index >= materials.Length) 
				return null;
			return materials[index];
		}
	}
}