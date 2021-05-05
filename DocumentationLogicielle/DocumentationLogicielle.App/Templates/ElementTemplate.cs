using System.Collections.Generic;
using DocumentationLogicielle.Models;

namespace DocumentationLogicielle.App.Templates
{
    /// <summary>
    /// TODO
    /// </summary>
    public class ElementTemplate
    {
        public string Label { get; set; }

        public string Price { get; set; }

        public int Quantity { get; set; }

        public List<NeededProductTemplate> MadeOf { get; set; }
    }
}
