﻿using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;

namespace DSAProject.Classes.Charakter.Talente
{
    public abstract class AbstractTalent : ITalent
    {
        #region Properties
        public Guid ID { private set; get; }
        public abstract int BaseDeduction { get; }
        public string BE { get; set; }
        public string Name { get; set; }
        public string NameExtension { get; set; }
        public List<ITalentDeduction> Deductions { get; private set;}
        #endregion
        public AbstractTalent(Guid id)
        {
            ID = id;
            Deductions = new List<ITalentDeduction>();
        }
        public override string ToString()
        {
            var ret = string.Empty;
            ret = ret + Name;

            if (NameExtension != null && NameExtension != string.Empty)
            {
                ret = ret + "(" + NameExtension.ToUpper() + ")";
            }

            return ret;
        }
    }
}
