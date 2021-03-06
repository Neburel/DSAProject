﻿
using System;
using System.Collections.Generic;


namespace DSAProjektKomponententest.Classes.Charakter.util
{
    public static class Generator
    {
        public static Dictionary<CharakterAttribut, int> GenerateCharakterAttributDictionary()
        {
            var ran = new Random();
            var dic = new Dictionary<CharakterAttribut, int>();

            foreach(CharakterAttribut item in Enum.GetValues(typeof(CharakterAttribut)))
            {
                dic.Add(item, ran.Next());
            }
            return dic;
        }
        public static Dictionary<CharakterAttribut, int> GenerateCharakterAttributDictionary(List<CharakterAttribut> attributs)
        {
            var ran = new Random();
            var dic = new Dictionary<CharakterAttribut, int>();

            foreach (CharakterAttribut item in attributs)
            {
                dic.Add(item, ran.Next());
            }
            return dic;
        }
    }
}
