using Gun2Core.Models.Base;
using System.Collections.Generic;

namespace Gun2Core.Models
{
    public class CadLayer : CadNamedEntity
    {
        public virtual ICollection<CadLayerId> LayerIds { get; set; }
        public virtual ICollection<CadLayerFilter> LayerFilters { get; set; }
        public ICollection<string> CadLayerIdNames { get; set; }
        public string Description { get; set; }
        public string Linetype { get; set; } = "Continuous";
        public string LinetypeFileName { get; set; } = "acadiso.lin";
        public int Lineweight { get; set; } = 9;
        public byte IndexColor { get; set; }
    }
}
