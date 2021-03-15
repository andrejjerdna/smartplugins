using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tekla.Structures.Dialog;

namespace sp_DimensionsForReinforcement
{
    /// <summary>
    /// Interaction logic for DimensionsForReinforcementWindow.xaml
    /// </summary>
    public partial class DimensionsForReinforcementWindow : PluginWindowBase
    {
        private DimensionsForReinforcementViewModel _dataModel;

        public DimensionsForReinforcementWindow(DimensionsForReinforcementViewModel DataModel)
        {
            _dataModel = DataModel;
            InitializeComponent();
        }

        private void WPFOkApplyModifyGetOnOffCancel_OkClicked(object sender, EventArgs e)
        {
            Apply();
            Close();
        }

        private void WPFOkApplyModifyGetOnOffCancel_ApplyClicked(object sender, EventArgs e)
        {
            Apply();
        }

        private void WPFOkApplyModifyGetOnOffCancel_ModifyClicked(object sender, EventArgs e)
        {
            Modify();
        }

        private void WPFOkApplyModifyGetOnOffCancel_GetClicked(object sender, EventArgs e)
        {
            Get();
        }

        private void WPFOkApplyModifyGetOnOffCancel_OnOffClicked(object sender, EventArgs e)
        {
            ToggleSelection();
        }

        private void WPFOkApplyModifyGetOnOffCancel_CancelClicked(object sender, EventArgs e)
        {
            Close();
        }
    }
}
