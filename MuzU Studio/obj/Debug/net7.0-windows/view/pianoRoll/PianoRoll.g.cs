﻿#pragma checksum "..\..\..\..\..\view\pianoRoll\PianoRoll.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F08C96776A133217ED62591252A8F788D0283456"
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
    /// PianoRoll
    /// </summary>
    public partial class PianoRoll : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 106 "..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid canvasContainer;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer scroller;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZoomAndPan.ZoomAndPanControl zoomAndPanControl;
        
        #line default
        #line hidden
        
        
        #line 127 "..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid content;
        
        #line default
        #line hidden
        
        
        #line 144 "..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle audioPos;
        
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
            System.Uri resourceLocater = new System.Uri("/MuzU Studio;component/view/pianoroll/pianoroll.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
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
            case 2:
            this.canvasContainer = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.scroller = ((System.Windows.Controls.ScrollViewer)(target));
            
            #line 111 "..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            this.scroller.KeyDown += new System.Windows.Input.KeyEventHandler(this.zoomAndPanControl_KeyDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.zoomAndPanControl = ((ZoomAndPan.ZoomAndPanControl)(target));
            
            #line 121 "..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            this.zoomAndPanControl.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.zoomAndPanControl_MouseDown);
            
            #line default
            #line hidden
            
            #line 122 "..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            this.zoomAndPanControl.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.zoomAndPanControl_MouseUp);
            
            #line default
            #line hidden
            
            #line 123 "..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            this.zoomAndPanControl.MouseMove += new System.Windows.Input.MouseEventHandler(this.zoomAndPanControl_MouseMove);
            
            #line default
            #line hidden
            
            #line 124 "..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            this.zoomAndPanControl.MouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.zoomAndPanControl_MouseWheel);
            
            #line default
            #line hidden
            
            #line 125 "..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            this.zoomAndPanControl.KeyDown += new System.Windows.Input.KeyEventHandler(this.zoomAndPanControl_KeyDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.content = ((System.Windows.Controls.Grid)(target));
            
            #line 130 "..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            this.content.SizeChanged += new System.Windows.SizeChangedEventHandler(this.pianoRoll_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.audioPos = ((System.Windows.Shapes.Rectangle)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 27 "..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Note_MouseDown);
            
            #line default
            #line hidden
            
            #line 28 "..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Note_MouseUp);
            
            #line default
            #line hidden
            
            #line 29 "..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.Note_MouseMove);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

