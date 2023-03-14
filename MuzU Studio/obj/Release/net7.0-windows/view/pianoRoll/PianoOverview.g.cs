﻿#pragma checksum "..\..\..\..\..\view\pianoRoll\PianoOverview.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9E3CE2435AEAE7134A1CE84976442C8DD253BE24"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MuzU_Studio.model;
using MuzU_Studio.util;
using MuzU_Studio.viewmodel;
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
using ZoomAndPan;


namespace MuzU_Studio.view {
    
    
    /// <summary>
    /// PianoOverview
    /// </summary>
    public partial class PianoOverview : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 52 "..\..\..\..\..\view\pianoRoll\PianoOverview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZoomAndPan.ZoomAndPanControl overview;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\..\view\pianoRoll\PianoOverview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridContent;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\..\view\pianoRoll\PianoOverview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl content;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\..\..\view\pianoRoll\PianoOverview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle audioPos;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\..\..\view\pianoRoll\PianoOverview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Thumb overviewZoomRectThumb;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MuzU Studio;component/view/pianoroll/pianooverview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\view\pianoRoll\PianoOverview.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 13 "..\..\..\..\..\view\pianoRoll\PianoOverview.xaml"
            ((MuzU_Studio.view.PianoOverview)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.UserControl_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.overview = ((ZoomAndPan.ZoomAndPanControl)(target));
            
            #line 53 "..\..\..\..\..\view\pianoRoll\PianoOverview.xaml"
            this.overview.SizeChanged += new System.Windows.SizeChangedEventHandler(this.pianoRoll_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.gridContent = ((System.Windows.Controls.Grid)(target));
            
            #line 63 "..\..\..\..\..\view\pianoRoll\PianoOverview.xaml"
            this.gridContent.SizeChanged += new System.Windows.SizeChangedEventHandler(this.pianoRoll_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.content = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 5:
            this.audioPos = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 6:
            this.overviewZoomRectThumb = ((System.Windows.Controls.Primitives.Thumb)(target));
            
            #line 110 "..\..\..\..\..\view\pianoRoll\PianoOverview.xaml"
            this.overviewZoomRectThumb.DragDelta += new System.Windows.Controls.Primitives.DragDeltaEventHandler(this.overviewZoomRectThumb_DragDelta);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

