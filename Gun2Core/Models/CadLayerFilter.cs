using Gun2Core.Models.Base;
using System.Collections.Generic;

namespace Gun2Core.Models
{
    public class CadLayerFilter : CadNamedEntity
    {
        public string LayerNameMask { get; set; }
        public virtual ICollection<CadLayer> Layers { get; set; }

    }
}
