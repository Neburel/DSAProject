using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Game;
using DSAProject.Classes.Interfaces;
using DSAProject.util;

namespace DSAProject.Layout.Wrapper
{
    public class TalentWrapper : AbstractPropertyChanged
    {
        public int TaW
        {
            get => Get<int>();
            set => Set(value);
        }
        public string Probe
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ProbeText
        {
            get => Get<string>();
            set => Set(value);
        }
        public string TaWToolTipText
        {
            get => Get<string>();
            set => Set(value);
        }
        public ITalent Talent
        {
            get => Get<ITalent>();
            set => Set(value);
        }
        public TalentWrapper()
        {
            PropertyChanged += (sender, args) =>
            {
                var talent = Talent;

                if (args.PropertyName == nameof(Talent))
                {
                    Probe = Game.Charakter.Talente.GetProbeString(talent, 0, 0);
                    TaW = Game.Charakter.Talente.GetMaxTaw(talent); //TAW wird beidseitig gesetzt, aufgrund dessen wird die Propertie genutzt um eine änderung festzusstellen und weiterzugeben

                    if (typeof(AbstractTalentGeneral).IsAssignableFrom(talent.GetType()))
                    {
                        var atg = (AbstractTalentGeneral)talent;
                        ProbeText = atg.GetProbeText();
                    }
                }
                else if (args.PropertyName == nameof(TaW))
                {
                    //Der Setter wird auch verwendet damit änderungen durch +/- und beidseitige Bindung ermöglicht werden
                    //daher ist ein  recht Komplexer mechanismus von nöten der erkennen kann wann nur der Max wert gesetz worden ist
                    var taw = TaW;
                    var currentMaxTaW = Game.Charakter.Talente.GetMaxTaw(talent);

                    if (currentMaxTaW != taw)
                    {
                        var currentAKTaw = Game.Charakter.Talente.GetTAW(talent);
                        var changeValue = TaW - currentMaxTaW;
                        var newTaw = currentAKTaw + changeValue;
                        Game.Charakter.Talente.SetTAW(Talent, newTaw);

                        var currentModTaw = Game.Charakter.Talente.GetModTaW(talent);

                        TaWToolTipText = newTaw + "(" + currentModTaw + ")";

                        if (Game.Charakter.Talente.GetMaxTaw(Talent) != taw)
                        {
                            throw new System.Exception("Fehler Implementuerung Bereitstellen");
                        }
                    }
                }

            };
        }
    }
}