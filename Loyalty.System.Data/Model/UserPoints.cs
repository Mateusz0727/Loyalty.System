using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyalty.System.Data.Model
{
    public class UserPoints
    {
        public string Id { get; set; }
        public ushort Points { get; set; }
        public ushort CountOfPrize { get; set ; }

        public string QrCodeToken { get; set; }
    }
}
