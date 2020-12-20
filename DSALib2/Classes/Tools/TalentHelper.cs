using DSALib2.Interfaces.Charakter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSALib2.Classes.Tools
{
    public static class TalentHelper
    {
        public static ITalent SearchTalent(Guid talentGuid, List<ITalent> talentList)
        {
            return talentList.Where(x => x.ID == talentGuid).FirstOrDefault();
        }
        public static ITalent SearchTalent(string name, List<ITalent> talentList, Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            var innerTalent = talentList.Where(x => x.Name == name).FirstOrDefault();
            if (innerTalent != null && type.IsAssignableFrom(innerTalent.GetType()))
            {
                return innerTalent;
            }
            return null;
        }
        public static T SearchTalentGeneric<T>(Guid talentGuid, List<ITalent> talentList)
        {
            var talent = SearchTalent(talentGuid, talentList);
            if (talent != null && typeof(T).IsAssignableFrom(talent.GetType()))
            {
                return (T)talent;
            }
            if (talent == null)
            {
                throw new ArgumentNullException("Das Talent mit der GUID " + talentGuid + " konnte nicht gefunden werden. Erwarteter Talent Typ: " + typeof(T));
            }
            else
            {
                throw new ArgumentNullException("Das Talent mit der GUID " + talentGuid + " konnte nicht mit dem Angegebenen Typen gefunden werden. Erwarteter Talent Typ: " + typeof(T) + " eigentlicher Typ: " + talent.GetType());
            }
        }
    }
}
