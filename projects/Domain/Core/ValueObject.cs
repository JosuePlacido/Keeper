using System.Collections.Generic;
using System.Linq;

namespace Keeper.Domain.Core
{

	public abstract class ValueObject<T> where T : ValueObject<T>
	{
		public override bool Equals(object obj)
		{
			var valueObject = obj as T;

			return this.GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
		}


		public override int GetHashCode()
		{
			return GetEqualityComponents()
				.Select(x => x != null ? x.GetHashCode() : 0)
				.Aggregate((x, y) => x ^ y);
		}

		public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
		{
			if (a is null && b is null)
				return true;

			if (a is null || b is null)
				return false;

			return a.Equals(b);
		}

		public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
		{
			return !(a == b);
		}

		protected abstract IEnumerable<object> GetEqualityComponents();
	}
}
