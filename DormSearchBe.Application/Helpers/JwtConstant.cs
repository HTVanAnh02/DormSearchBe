using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.Helpers
{
    public static class JwtConstant
    {
        public static int expiresIn { get; } = 1000;
        public static int refresh_expiresIn { get; } = 1200;
    }
   
}
