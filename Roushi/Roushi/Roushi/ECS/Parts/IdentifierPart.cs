using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roushi
{
    class IdentifierPart : Part
    {
        private string alliance;

        public string GetAlliance
        {
            get { return alliance; }
            set { alliance = value; }
        }


    }
}
