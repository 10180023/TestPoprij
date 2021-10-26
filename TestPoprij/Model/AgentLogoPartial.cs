using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPoprij.Model
{
    public partial class Agent
    {
        public string LogoAgent => Logo == null ? "../../Resources/picture.png" : Logo;
    }
}
