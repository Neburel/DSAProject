using DSALib.Charakter.Other;
using System.Runtime.Serialization;

namespace DSALib2.Classes.JSONSave
{
    [DataContract]
    public class JSONMoney
    {
        [DataMember]
        public int D { get; set; }
        [DataMember]
        public int S { get; set; }
        [DataMember]
        public int H { get; set; }
        [DataMember]
        public int K { get; set; }
        [DataMember]
        public int Bank { get; set; }

        public JSONMoney(){}
        public JSONMoney(Money money)
        {
            if (money == null) return;

            D = money.D;
            S = money.S;
            H = money.H;
            K = money.K;
            Bank = money.Bank;
        }
    }
}
