using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using T3D = Tekla.Structures.Geometry3d;

namespace PipeRack
{
    public partial class Form_att : Form
    {
        Attributes _attributes { get; set; }

        public Form_att(Attributes attributes)
        {
            _attributes = attributes;
            InitializeComponent();
            GetParams();
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
            PolojenieVertikalno.Text = _attributes.PolojenieVertikalno;
            PolojeniePovorot.Text = _attributes.PolojeniePovorot;
            PolojenieGorizontalno.Text = _attributes.PolojenieGorizontalno.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string namen = Namen.Text;
            string profile = Profile.Text;
            string material = Material.Text;
            string class1 = Class.Text;
            string prefixSborki = PrefixSborki.Text;
            string nomerSborki = NomerSborki.Text;
            string polojenieVertikalno = PolojenieVertikalno.Text;
            string polojeniePovorot = PolojeniePovorot.Text;
            string polojenieGorizontalno = PolojenieGorizontalno.Text;


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




    }
}
