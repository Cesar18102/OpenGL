using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPGL13
{
    public class CNTRL_ID
    {
        public int ID { get; private set; }

        private CNTRL_ID(int id) { ID = id; }

        public static CNTRL_ID GetFirstId() { return new CNTRL_ID(1); }
        public static CNTRL_ID GetSecondId() { return new CNTRL_ID(2); }
        public static CNTRL_ID GetThirdId() { return new CNTRL_ID(3); }
        public static CNTRL_ID GetFourthId() { return new CNTRL_ID(4); }
        public static CNTRL_ID GetFifthId() { return new CNTRL_ID(5); }

        public override bool Equals(object obj) {
            
            return obj is CNTRL_ID? (obj as CNTRL_ID).ID == ID : false;
        }
    }
}
