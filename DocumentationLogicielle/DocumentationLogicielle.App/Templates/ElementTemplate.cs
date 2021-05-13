using System;
using System.Collections.Generic;
using System.Windows.Media;

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

        /// <summary>
        /// The element is available until this date
        /// </summary>
        public string AvailableUntil { get; set; }

        public Brush ColorDate { get; set; }
    }
}
