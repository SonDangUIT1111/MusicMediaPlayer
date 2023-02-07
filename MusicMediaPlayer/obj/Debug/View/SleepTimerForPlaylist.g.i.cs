// Updated by XamlIntelliSenseFileGenerator 2/7/2023 3:45:52 PM
#pragma checksum "..\..\..\View\SleepTimerForPlaylist.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5AB675E27028D28FCE640ABF853D603AE12090FEBBF55E5A434305673465E858"
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


namespace MusicMediaPlayer.View
{


    /// <summary>
    /// SleepTimerForPlaylist
    /// </summary>
    public partial class SleepTimerForPlaylist : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector
    {

#line default
#line hidden


#line 41 "..\..\..\View\SleepTimerForPlaylist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button knobclock;

#line default
#line hidden


#line 52 "..\..\..\View\SleepTimerForPlaylist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider knob;

#line default
#line hidden


#line 121 "..\..\..\View\SleepTimerForPlaylist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton startClock;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MusicMediaPlayer;component/view/sleeptimerforplaylist.xaml", System.UriKind.Relative);

#line 1 "..\..\..\View\SleepTimerForPlaylist.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.SleepTimerWD = ((MusicMediaPlayer.View.SleepTimerForPlaylist)(target));
                    return;
                case 2:
                    this.knobclock = ((System.Windows.Controls.Button)(target));
                    return;
                case 3:
                    this.knob = ((System.Windows.Controls.Slider)(target));
                    return;
                case 6:
                    this.startClock = ((System.Windows.Controls.Primitives.ToggleButton)(target));
                    return;
            }
            this._contentLoaded = true;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 4:

#line 69 "..\..\..\View\SleepTimerForPlaylist.xaml"
                    ((System.Windows.Shapes.Ellipse)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.Ellipse_MouseMove);

#line default
#line hidden

#line 70 "..\..\..\View\SleepTimerForPlaylist.xaml"
                    ((System.Windows.Shapes.Ellipse)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Ellipse_MouseLeftButtonUp);

#line default
#line hidden
                    break;
                case 5:

#line 100 "..\..\..\View\SleepTimerForPlaylist.xaml"
                    ((System.Windows.Shapes.Ellipse)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Ellipse_MouseLeftButtonDown);

#line default
#line hidden

#line 101 "..\..\..\View\SleepTimerForPlaylist.xaml"
                    ((System.Windows.Shapes.Ellipse)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Ellipse_MouseLeftButtonUp);

#line default
#line hidden
                    break;
            }
        }

        internal System.Windows.Window SleepTimerWD;
    }
}

