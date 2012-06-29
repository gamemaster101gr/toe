using Toe.Marmalade.Geom;
using Toe.Marmalade.Gx;
using Toe.Marmalade.Util;

namespace Toe.Marmalade.Graphics
{
	/// <summary>
	/// The c iw model.
	/// </summary>
	public class CIwModel : CIwResource
	{
		#region Constants and Fields

		private readonly CIwManagedList ext = new CIwManagedList();

		private readonly CIwManagedList list = new CIwManagedList();

		private readonly CIwSphere sphere = new CIwSphere();

		private CIwMaterial[] materials;

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The get material.
		/// </summary>
		/// <param name="index">
		/// The index.
		/// </param>
		/// <returns>
		/// </returns>
		public CIwMaterial GetMaterial(uint index)
		{
			if (this.materials == null)
			{
				return null;
			}

			if (index >= this.materials.Length)
			{
				return null;
			}

			return this.materials[index];
		}

		/// <summary>
		/// The render.
		/// </summary>
		public void Render()
		{
			uint flags = 0;
			for (int i = 0; i < this.list.Size; ++i)
			{
				((CIwModelBlock)this.list[i]).Render(this, flags);
			}
		}

		/// <summary>
		/// The resolve block.
		/// </summary>
		/// <typeparam name="T">
		/// </typeparam>
		/// <returns>
		/// </returns>
		public T ResolveBlock<T>() where T : class
		{
			for (int i = 0; i < this.list.Size; ++i)
			{
				var b = this.list[i] as T;
				if (b != null)
				{
					return b;
				}
			}

			return null;
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

			this.sphere.Serialise(serialise);
			this.list.Serialise(serialise);
			this.ext.Serialise(serialise);

			if (serialise.IsReading())
			{
				uint numMaterials = 0;
				serialise.UInt32(ref numMaterials);
				this.materials = new CIwMaterial[numMaterials];
				for (int i = 0; i < this.materials.Length; ++i)
				{
					CIwManaged m = null;
					serialise.ManagedHash("CIwMaterial".ToeHash(), ref m);
					this.materials[i] = (CIwMaterial)m;
				}
			}
			else
			{
				uint numMaterials = (uint)this.materials.Length;
				serialise.UInt32(ref numMaterials);
				for (int i = 0; i < this.materials.Length; ++i)
				{
					CIwManaged m = this.materials[i];
					serialise.ManagedHash("CIwMaterial".ToeHash(), ref m);
				}
			}
		}

		#endregion
	}
}