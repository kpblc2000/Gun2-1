using Gun2Core.Models.Base;
using System.Collections.Generic;

namespace Gun2Core.Models
{
    public class CadLayer : CadNamedEntity
    {
        public virtual CadLayerId LayerId { get; set; }
        public virtual ICollection<CadLayerFilter> LayerFilters { get; set; }
    }
}
