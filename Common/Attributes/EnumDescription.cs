using System;

namespace Common.Attributes
{
	public class EnumDescription : Attribute
	{
		public string Description { get; set; }
		internal EnumDescription(string description)
		{
			Description = description;
		}
	}
}
