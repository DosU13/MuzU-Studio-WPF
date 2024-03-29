﻿#pragma checksum "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2E25917FEDE2ACA3BC6720BA466435AC75367593"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
        
        
        #line 169 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainContainer;
        
        #line default
        #line hidden
        
        
        #line 182 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZoomAndPan.ZoomAndPanControl TimelineZoomAndPan;
        
        #line default
        #line hidden
        
        
        #line 210 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Thumb playheadThumb;
        
        #line default
        #line hidden
        
        
        #line 230 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle playhead;
        
        #line default
        #line hidden
        
        
        #line 242 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZoomAndPan.ZoomAndPanControl PianoKeysZoomAndPan;
        
        #line default
        #line hidden
        
        
        #line 266 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer scroller;
        
        #line default
        #line hidden
        
        
        #line 273 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZoomAndPan.ZoomAndPanControl zoomAndPanControl;
        
        #line default
        #line hidden
        
        
        #line 285 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid content;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MuzU Studio;component/view/pianoroll/pianoroll.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 16 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            ((MuzU_Studio.view.PianoRoll)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 4:
            this.mainContainer = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.TimelineZoomAndPan = ((ZoomAndPan.ZoomAndPanControl)(target));
            return;
            case 6:
            
            #line 207 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Timeline_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.playheadThumb = ((System.Windows.Controls.Primitives.Thumb)(target));
            
            #line 215 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            this.playheadThumb.DragDelta += new System.Windows.Controls.Primitives.DragDeltaEventHandler(this.PlayheadThumb_DragDelta);
            
            #line default
            #line hidden
            return;
            case 8:
            this.playhead = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 9:
            this.PianoKeysZoomAndPan = ((ZoomAndPan.ZoomAndPanControl)(target));
            return;
            case 10:
            this.scroller = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 11:
            this.zoomAndPanControl = ((ZoomAndPan.ZoomAndPanControl)(target));
            
            #line 280 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            this.zoomAndPanControl.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.zoomAndPanControl_MouseDown);
            
            #line default
            #line hidden
            
            #line 281 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            this.zoomAndPanControl.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.zoomAndPanControl_MouseUp);
            
            #line default
            #line hidden
            
            #line 282 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            this.zoomAndPanControl.MouseMove += new System.Windows.Input.MouseEventHandler(this.zoomAndPanControl_MouseMove);
            
            #line default
            #line hidden
            
            #line 283 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            this.zoomAndPanControl.MouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.zoomAndPanControl_MouseWheel);
            
            #line default
            #line hidden
            return;
            case 12:
            this.content = ((System.Windows.Controls.Grid)(target));
            
            #line 288 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            this.content.SizeChanged += new System.Windows.SizeChangedEventHandler(this.pianoRoll_SizeChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 2:
            
            #line 34 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Note_MouseDown);
            
            #line default
            #line hidden
            
            #line 35 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Note_MouseUp);
            
            #line default
            #line hidden
            
            #line 36 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.Note_MouseMove);
            
            #line default
            #line hidden
            break;
            case 3:
            
            #line 59 "..\..\..\..\..\..\view\pianoRoll\PianoRoll.xaml"
            ((System.Windows.Controls.Primitives.Thumb)(target)).DragDelta += new System.Windows.Controls.Primitives.DragDeltaEventHandler(this.ResizeThumb_DragDelta);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

