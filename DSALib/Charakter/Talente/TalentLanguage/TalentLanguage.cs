﻿using System;


namespace DSAProject.Classes.Charakter.Talente.TalentLanguage
{
    public class TalentLanguage : AbstractTalentLanguage
    {
        public TalentLanguage(Guid id) : base(id) { }

        public override string ToString()
        {
            return base.ToString() + "(Sprache)";
        }
    }
}


