﻿#pragma checksum "DimensionsForReinforcementWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "89268014C98B5CCD4235ABF513A0245042F78381B905B14282CA61D2017CAF3E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Tekla.Structures.Dialog;
using Tekla.Structures.Dialog.UIControls;
using sp_DimensionsForReinforcement;


namespace sp_DimensionsForReinforcement {
    
    
    /// <summary>
    /// DimensionsForReinforcementWindow
    /// </summary>
    public partial class DimensionsForReinforcementWindow : Tekla.Structures.Dialog.PluginWindowBase, System.Windows.Markup.IComponentConnector {
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/sp_DimensionsForReinforcement;component/dimensionsforreinforcementwindow.xaml", System.UriKind.Relative);
            
            #line 1 "DimensionsForReinforcementWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 23 "DimensionsForReinforcementWindow.xaml"
            ((Tekla.Structures.Dialog.UIControls.WpfOkApplyModifyGetOnOffCancel)(target)).ApplyClicked += new System.EventHandler(this.WPFOkApplyModifyGetOnOffCancel_ApplyClicked);
            
            #line default
            #line hidden
            
            #line 24 "DimensionsForReinforcementWindow.xaml"
            ((Tekla.Structures.Dialog.UIControls.WpfOkApplyModifyGetOnOffCancel)(target)).CancelClicked += new System.EventHandler(this.WPFOkApplyModifyGetOnOffCancel_CancelClicked);
            
            #line default
            #line hidden
            
            #line 25 "DimensionsForReinforcementWindow.xaml"
            ((Tekla.Structures.Dialog.UIControls.WpfOkApplyModifyGetOnOffCancel)(target)).GetClicked += new System.EventHandler(this.WPFOkApplyModifyGetOnOffCancel_GetClicked);
            
            #line default
            #line hidden
            
            #line 26 "DimensionsForReinforcementWindow.xaml"
            ((Tekla.Structures.Dialog.UIControls.WpfOkApplyModifyGetOnOffCancel)(target)).OkClicked += new System.EventHandler(this.WPFOkApplyModifyGetOnOffCancel_OkClicked);
            
            #line default
            #line hidden
            
            #line 27 "DimensionsForReinforcementWindow.xaml"
            ((Tekla.Structures.Dialog.UIControls.WpfOkApplyModifyGetOnOffCancel)(target)).OnOffClicked += new System.EventHandler(this.WPFOkApplyModifyGetOnOffCancel_OnOffClicked);
            
            #line default
            #line hidden
            
            #line 28 "DimensionsForReinforcementWindow.xaml"
            ((Tekla.Structures.Dialog.UIControls.WpfOkApplyModifyGetOnOffCancel)(target)).ModifyClicked += new System.EventHandler(this.WPFOkApplyModifyGetOnOffCancel_ModifyClicked);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

