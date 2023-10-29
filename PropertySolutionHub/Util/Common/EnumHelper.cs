using System;
namespace PropertySolutionHub.Util.Common
{
	public static class EnumHelper
    {
		public static IEnumerable<Tuple<int, string>> GetEnumValues<T>() where T: Enum
		{
			return Enum.GetValues(typeof(T)).Cast<T>().Select(value => new Tuple<int, string>((int)(object)value, Enum.GetName(typeof(T), value)));
		}
	}
}

