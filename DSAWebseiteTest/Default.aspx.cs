using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DSADatabaseCreationTool.Util;
using DSALib;
using DSALib.Classes.JSON;
using DSAProject.Classes;
using DSAProject.Classes.Charakter;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Interfaces;
using DSAProject.util;

namespace DSAWebseiteTest
{
    public partial class _Default : Page
    {
        private static ICharakter Charakter;
        private static List<DSAProject.Classes.Interfaces.ITalent> talents = new List<ITalent>();
        private static List<InnerAbstractTalentGeneral> innerItems;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var jsonConent = EmbeddedResources.LoadTextResource(EmbedddedRes.JSONTalentFiles, "Talente_01092019.json");
                var jsonFile = JSON_TalentSaveFile.DeSerializeJson(jsonConent, out string error);
                talents = TalentHelper.LoadTalent(jsonFile.Talente_DSA, new System.Collections.ObjectModel.ObservableCollection<DSAProject.Classes.Interfaces.ITalent>()).ToList();

                Charakter = new CharakterDSA(Guid.NewGuid())
                {

                };
            }
        }
        public IQueryable<InnerAbstractTalentGeneral> DSATalente_GetData()
        {
            if(innerItems == null)
            {
                innerItems = new List<InnerAbstractTalentGeneral>();
                var abstractTalents = talents.OfType<AbstractTalentGeneral>().ToList();
                foreach(var item in abstractTalents)
                {
                    innerItems.Add(new InnerAbstractTalentGeneral
                    {
                        OrginalTalent = item,
                        Charakter = Charakter                        
                    });
                }
            }
            return innerItems.AsQueryable();
        }

        protected void ButtonMutPlus_Click(object sender, EventArgs e)
        {
            LabelMutValue.Text = AddValue(CharakterAttribut.Mut).ToString();
        }
        protected void ButtonMutMinus_Click(object sender, EventArgs e)
        {
            LabelMutValue.Text = ReduceValue(CharakterAttribut.Mut).ToString();
        }
        protected void ButtonKlugheitPlus_Click(object sender, EventArgs e)
        {
            LabelKlugheitValue.Text = AddValue(CharakterAttribut.Klugheit).ToString();
        }
        protected void ButtonKlugheitMinus_Click(object sender, EventArgs e)
        {
            LabelKlugheitValue.Text = ReduceValue(CharakterAttribut.Klugheit).ToString();
        }
        protected void ButonIntuitionPlus_Click(object sender, EventArgs e)
        {
            LabelIntuitionValue.Text = AddValue(CharakterAttribut.Intuition).ToString();
        }
        protected void ButonIntuitionMinus_Click(object sender, EventArgs e)
        {
            LabelIntuitionValue.Text = ReduceValue(CharakterAttribut.Intuition).ToString();
        }
        protected void ButtonCharismaPlus_Click(object sender, EventArgs e)
        {
            LabelValueCharisma.Text = AddValue(CharakterAttribut.Charisma).ToString();
        }
        protected void ButtonCharismaMinus_Click(object sender, EventArgs e)
        {
            LabelValueCharisma.Text = ReduceValue(CharakterAttribut.Charisma).ToString();
        }
        protected void ButtonFingerfertigkeitPlus_Click(object sender, EventArgs e)
        {
            LabelFingerfertigkeit.Text = AddValue(CharakterAttribut.Fingerfertigkeit).ToString();
        }
        protected void ButtonFingerfertigkeitMinus_Click(object sender, EventArgs e)
        {
            LabelValueFingerfertigkeit.Text = ReduceValue(CharakterAttribut.Fingerfertigkeit).ToString();
        }
        protected void ButtonGewandheitPlus_Click(object sender, EventArgs e)
        {
            LabelValueGewandheit.Text = AddValue(CharakterAttribut.Gewandheit).ToString();
        }
        protected void ButtonGewandheitMinus_Click(object sender, EventArgs e)
        {
            LabelValueGewandheit.Text = ReduceValue(CharakterAttribut.Gewandheit).ToString();
        }
        protected void ButtonKonstitutionPlus_Click(object sender, EventArgs e)
        {
            Label1KonstitutionValue.Text = AddValue(CharakterAttribut.Konstitution).ToString();
        }
        protected void ButtonKonstitutionMinus_Click(object sender, EventArgs e)
        {
            Label1KonstitutionValue.Text = ReduceValue(CharakterAttribut.Konstitution).ToString();
        }
        protected void ButtonKörperkraftPlus_Click(object sender, EventArgs e)
        {
           LabelKörperkraftValue.Text = AddValue(CharakterAttribut.Körperkraft).ToString();
        }
        protected void ButtonKörperkraftMinus_Click(object sender, EventArgs e)
        {
            LabelKörperkraftValue.Text = ReduceValue(CharakterAttribut.Körperkraft).ToString();
        }
        protected void ButtonSozialstatusPlus_Click(object sender, EventArgs e)
        {
            LabelSozialstatusValue.Text = AddValue(CharakterAttribut.Sozialstatus).ToString();
        }
        protected void ButtonSozialstatusMinus_Click(object sender, EventArgs e)
        {
            LabelSozialstatusValue.Text = ReduceValue(CharakterAttribut.Sozialstatus).ToString();
        }

        private int AddValue(CharakterAttribut attribut)
        {
            return ChangeValue(attribut, 1);
        }
        private int ReduceValue(CharakterAttribut attribut)
        {
            return ChangeValue(attribut, -1);
        }
        private int ChangeValue(CharakterAttribut attribut, int value)
        {
            var currentValue = Charakter.Attribute.GetAttributAKTValue(attribut, out DSALib.Utils.Error error);
            currentValue = currentValue + value;
            Charakter.Attribute.SetAKTValue(attribut, currentValue, out error);
            DSATalente.DataBind();

            return currentValue;
        }
    }
    public class InnerAbstractTalentGeneral : AbstractPropertyChanged
    {
        public AbstractTalentGeneral OrginalTalent { get; set; }
        public ICharakter Charakter { get; set; }
        public string Name
        {
            get => OrginalTalent.Name;
        }
        public string ProbeString
        {
            get => OrginalTalent.GetProbeText();
        }
        public string ProbeValueString
        {
            get => Charakter.Talente.GetProbeString(OrginalTalent);
        }

        public void ChangeValue()
        {
            OnPropertyChanged(nameof(ProbeValueString));
        }
    }
}