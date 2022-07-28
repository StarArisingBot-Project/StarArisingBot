using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarArisingBotFramework.Attributes.Commands
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CategoryAttribute : Attribute
    {
        public string Name { get; }

        public CategoryAttribute(string name)
        {
            Name = name;
        }
    }
}
