﻿#pragma checksum "..\..\..\KlantWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "783B5AFE019E8835908B4C2EC9F8BEAC954EA9B9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FitnessReservatie.UI;
using FitnessReservatieBL.Managers;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace FitnessReservatie.UI {
    
    
    /// <summary>
    /// KlantWindow
    /// </summary>
    public partial class KlantWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\..\KlantWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LabelWelkomKlant;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\KlantWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DatePickerDatumSelector;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\KlantWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxToesteltypeSelector;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\KlantWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxTijdslotSelector;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\KlantWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListViewReservations;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\KlantWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FitnessReservatie.UI;component/klantwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\KlantWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.LabelWelkomKlant = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.DatePickerDatumSelector = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 3:
            this.ComboBoxToesteltypeSelector = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.ComboBoxTijdslotSelector = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.ListViewReservations = ((System.Windows.Controls.ListView)(target));
            return;
            case 6:
            this.Button = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

