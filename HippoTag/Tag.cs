using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace HippoTag
{
	class Tag
	{
		public int FileLineNumber { get; set; }
		public List<string> Categories { get; set; } = new List<string>();
		public string CategoryType { get; set; }
		public string Name { get; set; }

		override
		public string ToString()
		{
			return $"{Name} ({Categories.Count})";
		}
	}

	class TagComparer : IEqualityComparer<Tag>
	{
		public bool Equals([AllowNull] Tag x, [AllowNull] Tag y)
		{
			return x.Name == y.Name;
		}

		public int GetHashCode([DisallowNull] Tag obj)
		{
			return obj.Name.GetHashCode();
		}
	}

}