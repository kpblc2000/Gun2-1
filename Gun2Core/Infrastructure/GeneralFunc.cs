using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gun2Core.Infrastructure
{
public     class GeneralFunc
    {
        public static string GetDialogTitle(string WindowTitle)
        {
            return $"Gun2 v.{typeof(GeneralFunc).Assembly.GetName().Version} : {WindowTitle.Trim()}";
        }
    }
}
