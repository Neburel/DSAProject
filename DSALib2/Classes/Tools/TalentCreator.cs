using DSALib2.Charakter.Talente.TalentLanguage;
using DSALib2.Classes.Charakter.Talente;
using DSALib2.Classes.Charakter.Talente.TalentFighting;
using DSALib2.Classes.Charakter.Talente.TalentGeneral;
using DSALib2.Classes.Charakter.Talente.TalentLanguage;
using DSALib2.Interfaces.Charakter;
using DSALib2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSALib2.Classes.Tools
{
    public static class TalentCreator
    {
        public static T CreateTalent<T>(Guid guid = new Guid(), List<CharakterAttribut> probe = null, int orginalPos = -1, string name = null, string nameExtension = null, 
            string be = null) where T : ITalent
        {
            T newTalent;
            if (typeof(AbstractTalentGeneral).IsAssignableFrom(typeof(T)))
            {
                newTalent = (T)Activator.CreateInstance(typeof(T), guid, probe);
            }
            else
            {
                newTalent = (T)Activator.CreateInstance(typeof(T), guid);
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Talent Name is Null");
            }
            newTalent.Name = name;
            newTalent.NameExtension = nameExtension;
            newTalent.BE = be;
            newTalent.OrginalPosition = orginalPos;

            return newTalent;
        }
        public static ITalent CreateTalent(string contentType, List<CharakterAttribut> probe, string be, string name, string nameExtension, Guid talentGuid = new Guid(), int orginalPos = -1)
        {
            ITalent talent;
            if (contentType == nameof(TalentWeaponless))
            {
                talent = CreateTalent<TalentWeaponless>(talentGuid, probe, orginalPos, name, nameExtension, be);
            }
            else if (contentType == nameof(TalentClose))
            {
                talent = CreateTalent<TalentClose>(talentGuid, probe, orginalPos, name, nameExtension, be);
            }
            else if (contentType == nameof(TalentRange))
            {
                talent = CreateTalent<TalentRange>(talentGuid, probe, orginalPos, name, nameExtension, be);
            }
            else if (contentType == nameof(TalentCrafting))
            {
                talent = CreateTalent<TalentCrafting>(talentGuid, probe, orginalPos, name, nameExtension, be);
            }
            else if (contentType == nameof(TalentKnowldage))
            {
                talent = CreateTalent<TalentKnowldage>(talentGuid, probe, orginalPos, name, nameExtension, be);
            }
            else if (contentType == nameof(TalentNature))
            {
                talent = CreateTalent<TalentNature>(talentGuid, probe, orginalPos, name, nameExtension, be);
            }
            else if (contentType == nameof(TalentPhysical))
            {
                talent = CreateTalent<TalentPhysical>(talentGuid, probe, orginalPos, name, nameExtension, be);
            }
            else if (contentType == nameof(TalentSocial))
            {
                talent = CreateTalent<TalentSocial>(talentGuid, probe, orginalPos, name, nameExtension, be);
            }
            else if (contentType == nameof(TalentSpeaking) || contentType == "TalentLanguage")
            {
                talent = CreateTalent<TalentSpeaking>(talentGuid, probe, orginalPos, name, nameExtension, be);
            }
            else if (contentType == nameof(TalentWriting))
            {
                talent = CreateTalent<TalentWriting>(talentGuid, probe, orginalPos, name, nameExtension, be);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Talent Not Implemented");
            }
            return talent;
        }
        public static ITalent CreateTalent(List<ITalent> currentTalentList, string contentType, List<CharakterAttribut> probe, string be, string name, string nameExtension, Guid talentGuid = new Guid(), int orginalPos = -1)
        {
            if (talentGuid == new Guid() || talentGuid == null)
            {
                talentGuid = GenerateNextGuid(currentTalentList);
            }

            return CreateTalent(contentType, probe, be, name, nameExtension, talentGuid, orginalPos);
        }
        private static Guid GenerateNextGuid(List<ITalent> currentTalentList)
        {  
            var newGuid = Guid.NewGuid();

            while (currentTalentList.Where(x => x.ID == newGuid) != null)
            {
                newGuid = Guid.NewGuid();
            }
            return newGuid;
        }

    }
}
