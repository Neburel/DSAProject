using DSALib2.Classes.Charakter.Values.Attribute;
using DSALib2.Interfaces.Charakter;
using DSALib2.Interfaces.Charakter.Repository;
using System;

namespace DSALib2.Classes.Charakter.Values.Extended
{
    public class DeathThresholdTime : IValue
    {
        public int Value { get => Calculate(); }
        public string Name => DSALib2.Resources.DeathThresholdTime;
        public string ShortName { get => Name; }
        public string InfoText => "MR + IN";

        private IAttributeRepository attributeRepository;
        private IValueRepository valueRepository;
        private WoundSwell woundSwell;
        public DeathThresholdTime(IAttributeRepository attributeRepository)
        {
            this.attributeRepository = attributeRepository;
        }
        public void SetValue(IValueRepository valueRepository)
        {
            this.valueRepository = valueRepository;
            woundSwell = (WoundSwell)valueRepository.GetItemByType(typeof(WoundSwell));
            if (woundSwell == null) throw new ArgumentNullException();
        }
        protected int Calculate()
        {
            if (woundSwell == null) throw new ArgumentNullException();

            var mut = attributeRepository.GetMAX(Utils.CharakterAttribut.Mut);
            var ws = valueRepository.GetMAX(this.woundSwell);
            var value = Convert.ToDecimal((mut + ws) / 4);
            return Convert.ToInt32(Math.Ceiling(value));
        }
    }
}
