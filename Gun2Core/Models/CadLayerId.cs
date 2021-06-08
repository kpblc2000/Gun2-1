using Gun2Core.Models.Base;
using System.Collections.Generic;

namespace Gun2Core.Models
{
    public class CadLayerId : CadNamedEntity
    {
        public virtual ICollection<CadLayer> Layers { get; set; }
    }
}
