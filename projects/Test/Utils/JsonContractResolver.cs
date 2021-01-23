using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Test.Utils
{
	/*
	* Exemplo:
	* var personCopy = JsonConvert.DeserializeObject<Person>(jsonString, new JsonSerializerSettings()
	*	{
	*		ContractResolver = new PrivateResolver(),
	*		ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
	*	});
	*/
	public class PrivateResolver : DefaultContractResolver
	{
		protected override JsonProperty CreateProperty(MemberInfo member,
			MemberSerialization memberSerialization)
		{
			var prop = base.CreateProperty(member, memberSerialization);
			if (!prop.Writable)
			{
				var property = member as PropertyInfo;
				var hasPrivateSetter = property?.GetSetMethod(true) != null;
				prop.Writable = hasPrivateSetter;
			}
			return prop;
		}
	}
}
