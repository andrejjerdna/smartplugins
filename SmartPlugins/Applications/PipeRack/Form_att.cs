using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tekla.Structures.Dialog;

namespace PipeRack
{
    public partial class Form_att : ApplicationFormBase
    {
        Attributes _attributes { get; set; }
        List<Attributes> _attributesFrame { get; set; }
        public int selectY { get; set; }
       
        public Form_att(Attributes attributes)
        {
            _attributes = attributes;
            InitializeComponent();

            GetParams();

        }
        public Form_att(List<Attributes> attributesFrame, int Count)
        {
            _attributesFrame = attributesFrame;
            selectY = Count;
           InitializeComponent();
            GetParams2();

        }

        private void GetParams()
        {
            if (_attributes == null)
               return;

            Namen.Text = _attributes.Name;
            Profile.Text = _attributes.Profile;
            Material.Text = _attributes.Material;
            Class.Text = _attributes.Class;
            PrefixSborki.Text = _attributes.PrefixSborki;
            NomerSborki.Text = _attributes.NomerSborki.ToString();
            DepthCB.SelectedIndex = _attributes.PolojenieVertikalno;
            RotationCB.SelectedIndex = _attributes.PolojeniePovorot;
            PlaneCB.SelectedIndex = _attributes.PolojenieGorizontalno;
            SelectYarusCB.SelectedIndex = selectY;
        }
        private void GetParams2()
        {
            _attributes = _attributesFrame[selectY];
            if (_attributes == null)
                return;

            Namen.Text = _attributes.Name;
            Profile.Text = _attributes.Profile;
            Material.Text = _attributes.Material;
            Class.Text = _attributes.Class;
            PrefixSborki.Text = _attributes.PrefixSborki;
            NomerSborki.Text = _attributes.NomerSborki.ToString();
            DepthCB.SelectedIndex = _attributes.PolojenieVertikalno;
            RotationCB.SelectedIndex = _attributes.PolojeniePovorot;
            PlaneCB.SelectedIndex = _attributes.PolojenieGorizontalno;
            SelectYarusCB.SelectedIndex = selectY;
        }


        private void LoadButton_Click(object sender, EventArgs e)
        {
            
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            string namen = Namen.Text;
            string profile = Profile.Text;
            string material = Material.Text;
            string class1 = Class.Text;
            string prefixSborki = PrefixSborki.Text;
            string nomerSborki = NomerSborki.Text;
            int polojenieVertikalno = DepthCB.SelectedIndex;
            int polojeniePovorot = RotationCB.SelectedIndex;
            int polojenieGorizontalno = PlaneCB.SelectedIndex;
            selectY = SelectYarusCB.SelectedIndex;
            


            _attributes = new Attributes()

            {
                Name = namen,
                Profile = profile,
                Material = material,
                Class = class1,
                PrefixSborki = prefixSborki,
                NomerSborki = nomerSborki,
                PolojenieVertikalno = polojenieVertikalno,
                PolojeniePovorot = polojeniePovorot,
                PolojenieGorizontalno = polojenieGorizontalno,
            };

            Form_att.ActiveForm.Close();
        }

        public Attributes GetAttributes()
        {
            return _attributes;
        }

        private void profileCatalog1_SelectClicked(object sender, EventArgs e)
        {
            profileCatalog1.SelectedProfile = Profile.Text;
        }

        private void profileCatalog1_SelectionDone(object sender, EventArgs e)
        {
            Profile.Text = profileCatalog1.SelectedProfile;
        }

        private void materialCatalog1_SelectClicked(object sender, EventArgs e)
        {
            materialCatalog1.SelectedMaterial = Material.Text;
        }

        private void materialCatalog1_SelectionDone(object sender, EventArgs e)
        {
            Material.Text = materialCatalog1.SelectedMaterial;
        }

        private void ProfileCatalog1_Load(object sender, EventArgs e)
        {

        }

        private void ImageComboBox1_Load(object sender, EventArgs e)
        {

        }

        private void ImageListComboBox1_Load(object sender, EventArgs e)
        {


        }

        private void SelectYarusCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }


    }
}
