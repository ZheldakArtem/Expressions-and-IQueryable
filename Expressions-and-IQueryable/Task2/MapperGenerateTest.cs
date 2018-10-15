using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Task2
{
	public class VkUser
	{
		public string Name { get; set; }
		public int Age { get; set; }
		public string Country { get; set; }
	}

	public class ViberUser
	{
		public string Name { get; set; }
		public int Age { get; set; }
		public string Country { get; set; }

		public override string ToString()
		{
			return $"Name = {Name}, Age = {Age} Country = {Country}";
		}
	}

	//Задание 2.
	//Используя возможность конструировать Expression Tree и выполнять его код, создайте собственный механизм
	//маппинга(копирующего поля (свойства) одного класса в другой).
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var mappingGenerator = new MappingGenerator();
			var mapping = mappingGenerator.Generate<VkUser, ViberUser>();

			VkUser source = new VkUser()
			{
				Name = "Artem",
				Age = 23,
				Country = "Belarus"
			};

			ViberUser result = mapping.Map(source);

			Assert.AreEqual(source.Name, result.Name);
			Assert.AreEqual(source.Age, result.Age);
			Assert.AreEqual(source.Country, result.Country);

		}
	}
}
