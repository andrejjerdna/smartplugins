using SmartTeklaModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Dialog;

namespace Frames
{
    public partial class FramesPluginsForm : PluginFormBase
    {
        public FramesPluginsForm()
        {
            InitializeComponent();

            var profiles = Catalogs.GetProfile(Tekla.Structures.Catalogs.ProfileItem.ProfileItemTypeEnum.PROFILE_I);

            Profile1.Items.AddRange(profiles.ToArray());
        }

        private void okApplyModifyGetOnOffCancel1_ApplyClicked(object sender, EventArgs e)
        {
            this.Apply();
        }

        private void okApplyModifyGetOnOffCancel1_CancelClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okApplyModifyGetOnOffCancel1_GetClicked(object sender, EventArgs e)
        {
            this.Get();
        }

        private void okApplyModifyGetOnOffCancel1_ModifyClicked(object sender, EventArgs e)
        {
            this.Modify();
        }

        private void okApplyModifyGetOnOffCancel1_OkClicked(object sender, EventArgs e)
        {
            this.Apply();
            this.Close();
        }

        private void okApplyModifyGetOnOffCancel1_OnOffClicked(object sender, EventArgs e)
        {
            this.ToggleSelection();
        }

        private Control GetEnableCheckBox(string attributeName, Control parent)
        {
            Control foundCheckBox = null;

            for (int i = 0; i < parent.Controls.Count && foundCheckBox == null; i++)
            {
                Control control = parent.Controls[i];

                if (control.GetType() == typeof(CheckBox))
                {
                    CheckBox checkBox = control as CheckBox;

                    if (attributeName == structuresExtender.GetAttributeName(checkBox))
                    {
                        foundCheckBox = checkBox;
                    }
                }
                else
                {
                    foundCheckBox = GetEnableCheckBox(attributeName, control);
                }
            }

            return foundCheckBox;
        }

        private void SetThisControlEnableCheckBoxChecked(object sender)
        {
            Control thisControl = sender as Control;

            if (thisControl != null)
            {
                Control control = GetEnableCheckBox(structuresExtender.GetAttributeName(thisControl), this);
                CheckBox enableCheckBox = control as CheckBox;

                if (enableCheckBox != null)
                {
                    enableCheckBox.Checked = true;
                }
            }
        }

        private void anyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            SetThisControlEnableCheckBoxChecked(sender);
        }

        private void anyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetThisControlEnableCheckBoxChecked(sender);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
