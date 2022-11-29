﻿#pragma checksum "..\..\..\View\MySong.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "281355CCD391D36C525B45DB1D3CFFA99348BB121CEA1B962E3E784311546578"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using MusicMediaPlayer.View;
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
using System.Windows.Interactivity;
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


namespace MusicMediaPlayer.View {
    
    
    /// <summary>
    /// MySong
    /// </summary>
    public partial class MySong : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\View\MySong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MusicMediaPlayer.View.MySong SongWD;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\View\MySong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SongFilter;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\View\MySong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListSong;
        
        #line default
        #line hidden
        
        
        #line 162 "..\..\..\View\MySong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sliProgress;
        
        #line default
        #line hidden
        
        
        #line 180 "..\..\..\View\MySong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label InTime;
        
        #line default
        #line hidden
        
        
        #line 184 "..\..\..\View\MySong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label TotalTime;
        
        #line default
        #line hidden
        
        
        #line 190 "..\..\..\View\MySong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SpeakerHigh;
        
        #line default
        #line hidden
        
        
        #line 204 "..\..\..\View\MySong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SpeakerMedium;
        
        #line default
        #line hidden
        
        
        #line 220 "..\..\..\View\MySong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SpeakerLow;
        
        #line default
        #line hidden
        
        
        #line 236 "..\..\..\View\MySong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SpeakerOff;
        
        #line default
        #line hidden
        
        
        #line 248 "..\..\..\View\MySong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider Volume;
        
        #line default
        #line hidden
        
        
        #line 275 "..\..\..\View\MySong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton RandomLoop;
        
        #line default
        #line hidden
        
        
        #line 294 "..\..\..\View\MySong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Play;
        
        #line default
        #line hidden
        
        
        #line 300 "..\..\..\View\MySong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Pause;
        
        #line default
        #line hidden
        
        
        #line 316 "..\..\..\View\MySong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton SequencecyLoop;
        
        #line default
        #line hidden
        
        
        #line 330 "..\..\..\View\MySong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton OneLoop;
        
        #line default
        #line hidden
        
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
            System.Uri resourceLocater = new System.Uri("/MusicMediaPlayer;component/view/mysong.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\MySong.xaml"
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
            this.SongWD = ((MusicMediaPlayer.View.MySong)(target));
            return;
            case 2:
            this.SongFilter = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.ListSong = ((System.Windows.Controls.ListView)(target));
            return;
            case 4:
            this.sliProgress = ((System.Windows.Controls.Slider)(target));
            return;
            case 5:
            this.InTime = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.TotalTime = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.SpeakerHigh = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.SpeakerMedium = ((System.Windows.Controls.Button)(target));
            return;
            case 9:
            this.SpeakerLow = ((System.Windows.Controls.Button)(target));
            return;
            case 10:
            this.SpeakerOff = ((System.Windows.Controls.Button)(target));
            return;
            case 11:
            this.Volume = ((System.Windows.Controls.Slider)(target));
            return;
            case 12:
            this.RandomLoop = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 13:
            this.Play = ((System.Windows.Controls.Button)(target));
            return;
            case 14:
            this.Pause = ((System.Windows.Controls.Button)(target));
            return;
            case 15:
            this.SequencecyLoop = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 16:
            this.OneLoop = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

