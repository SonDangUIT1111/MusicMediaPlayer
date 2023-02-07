﻿#pragma checksum "..\..\..\View\Discover_GenreSong.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "52531E02915544D55E600DD67C079AAF8CF012D00FA5C0883CFD596597B88A42"
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
using XamlAnimatedGif;


namespace MusicMediaPlayer.View {
    
    
    /// <summary>
    /// Discover_GenreSong
    /// </summary>
    public partial class Discover_GenreSong : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\View\Discover_GenreSong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MusicMediaPlayer.View.Discover_GenreSong GenreSongVM;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\View\Discover_GenreSong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border OutsideBackground;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\View\Discover_GenreSong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton RandomLoop;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\View\Discover_GenreSong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton Play;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\View\Discover_GenreSong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton Pause;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\View\Discover_GenreSong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton SequencecyLoop;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\..\View\Discover_GenreSong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton OneLoop;
        
        #line default
        #line hidden
        
        
        #line 133 "..\..\..\View\Discover_GenreSong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid AlbumBackground;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\..\View\Discover_GenreSong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border GenreFrame;
        
        #line default
        #line hidden
        
        
        #line 152 "..\..\..\View\Discover_GenreSong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock NameGenre;
        
        #line default
        #line hidden
        
        
        #line 513 "..\..\..\View\Discover_GenreSong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid CompositionList;
        
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
            System.Uri resourceLocater = new System.Uri("/MusicMediaPlayer;component/view/discover_genresong.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\Discover_GenreSong.xaml"
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
            this.GenreSongVM = ((MusicMediaPlayer.View.Discover_GenreSong)(target));
            return;
            case 2:
            this.OutsideBackground = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.RandomLoop = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 4:
            this.Play = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 5:
            this.Pause = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 6:
            this.SequencecyLoop = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 7:
            this.OneLoop = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 8:
            this.AlbumBackground = ((System.Windows.Controls.Grid)(target));
            return;
            case 9:
            this.GenreFrame = ((System.Windows.Controls.Border)(target));
            return;
            case 10:
            this.NameGenre = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.CompositionList = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

