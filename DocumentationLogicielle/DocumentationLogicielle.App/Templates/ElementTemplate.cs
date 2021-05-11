using System.Collections.Generic;
using DocumentationLogicielle.Models;

namespace DocumentationLogicielle.App.Templates
{
    /// <summary>
    /// Templace classe for an element
    /// </summary>
    public class ElementTemplate
    {
        /// <summary>
        /// Label for the element
        /// </summary>
        public string Label { get; set; }
        
        /// <summary>
        /// Price of the element
        /// </summary>
        public string Price { get; set; }
        
        /// <summary>
        /// Quantity of the element
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// What the element if made of
        /// </summary>
        public List<NeededProductTemplate> MadeOf { get; set; }
    }
}
