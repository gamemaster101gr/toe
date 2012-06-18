using System;
using System.Collections.Generic;

namespace Toe.Core
{
	public class ClassRegistry
	{
		Dictionary<uint, Type> hashMap = new Dictionary<uint,Type>();

		public void RegisterType(Type t)
		{
			var hash = t.Name.ToeHash();
			if (hashMap.ContainsKey(hash))
				throw new ArgumentException("Duplicate hash");
			hashMap[hash] = t;
		}
		public void RegisterType<T>()
		{
			RegisterType(typeof(T));
		}

		public object CreateInstance(uint destinationComponent)
		{
			return Activator.CreateInstance(hashMap[destinationComponent]);
		}
	}
}