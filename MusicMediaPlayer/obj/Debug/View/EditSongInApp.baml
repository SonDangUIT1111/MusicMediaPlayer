   M S B A M L   `   `   `  ????K  GMusicMediaPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:MusicMediaPlayer.ViewMusicMediaPlayer.View  5   	   )    #MusicMediaPlayer.View.EditSongInApp   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:MusicMediaPlayer.View     5   	   V RXamlAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=20a987d8023d9690<gif2https://github.com/XamlAnimatedGif/XamlAnimatedGif  5	   	   _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
   	   -   5        $??EditSongApp??5   	     ? Title$  	Edit Song??5   	      MaterialDesignBody#???   5   	   $??Regular"?5   	   $??13(?5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=?5   	      TextRenderingMode$
 Auto=?5   	     MaterialDesignFont#???  5   	   
?????? $	??350??5   	   $	??600??6    ? WindowStartupLocation$ CenterScreen=?5   	    ? WindowStyle$
 None=?6.    ? AllowsTransparency .  
 SongVM ? DataContext# [ 5   	   .??5      ? 
?????? $??5q?5      .+?5   
   ??  ?? CornerRadius$ 20??6   
???????$??1q?5      
???   ?$??1??5      ?? 5   
   .+?5      ??  ?? Orientation$ 
Horizontal=?6   $??Center=?5      $??Center=?5       $??0,20,0,0q?5!      .+?5"      ?? '  !XamlAnimatedGif.AnimationBehavior	   	SourceUri$$	 \Image\MusicNoteBackground.gif??6   {?. $??60??5$      $??60??5%      $??0,-10,0,-10q?5&      5'      ?? 5(      $!??New change - New experience??6   $??Center=?5)      $??26(?5*      $??Bahnschrift SemiBold)?5+      $??Light"?5,      $??LeftToRight=?5-       " MaterialDesignElevationShadow4
 J? Effect#
 ?  5.      $??	0,5,0,-10q?5/      ??50      ?? 51      .??52      ?  ? Color$ #2af598??6+    ? Offset$ 1Q?6;   ? 53      $ #009efd??6+   $	 0.3Q?6;   ? 54      $ #ba55d3??6+   $
 0.05Q?6;   55      57      ?? 58      $$	 \Image\MusicNoteBackground.gif??6   {?. $??60??5:      $??60??5;      $??0,-10,0,-10q?5<      5=      5>      ?? 5?      $ 
Horizontal=?6   .+?5@      ? -   5         ?Name$ ChangegrdSelectImg??5A      $??	50,30,0,0q?5B      $	??Top=?5C      $	??150??5D      $	??150??5E      $
??Left=?5F      .+?5G      ?? -   5         ??Name$ ChangerecImage??5H   !    ?? StrokeDashArray$	 4 2R?5I   !   ???#ffffff$??4??5K   !    ?? RadiusX$ 4??5L   !    ?? RadiusY$ 4??5M   !   $	??150??5N   !   $	??150??5O   !   $
??Left=?5P   !   $	??Top=?5Q   !   ?? 5R      -   5         ??Name$ btnSelectImage??5S      $??35??5V      $	??100??5W      $??Select Image??5X      $??14(?5Y      ???#FFFFFF???#1ED760
?????? $??SemiBold"?5]       ? ToolTip$ Chọn ảnh??5^      $??0,0,0,12q?5_      $??Center=?5`      $??Bottom=?5a      $??0q?5c      ??5T      ?? *ChangeImage+??5U      ??  ?? ElementName$ ChangegrdSelectImg?? ? FocusVisualStyle 5b      Z? 5c      '  !MaterialDesignThemes.Wpf.PackIcon  5d      -   5          Name$ 	iconImage??5e         Kind$ FileImageOutline=?5f      ???#ffffff$??64??5h      $??54??6(   $??Center=?5i      $	??Top=?5j      $??0,25,0,0q?5k      5l      ?? 5m      $??0,50,0,0q?6   .+?5n      ?? -   5         ??Name$ ChangeTitleSong??5n      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 
Song Title??5o      $	??300??5p      $??50,0,0,0q?5q      
???????
??????? % !MaterialDesignFloatingHintTextBox#??[ 5u      ??5r      ?? *TitleToChange+5u      ?? 5v      -   5        $ ChangeArtistSong??5v      $ Artist??5w      $	??300??5x      $??	50,20,0,0q?5y      
???????
???????#??[ 5}      ??5|      ?? *ArtistToChange+5}      5~      5      ?? 5?      $??
460,20,0,0q?6   $ 
Horizontal=?5?      .+?5?      ?? $??40??5?      $??0q?5?      $??0q?5?      
??????????#1ED760$	 Add??5?      ??5?      ?? *
Changing+??5?      ?? $ EditSongApp????5?      0  *MaterialDesignThemes.Wpf.PackIconExtension  *Check+5?      ?? 5?      $??10,0,0,0q?5?      $??40??5?      $??0q?5?      $??0q?5?      
??????????#B3B3B3$ Cancel??5?      ??5?      ?? *CancelChanging+??5?      ?? $ EditSongApp????5?        *Close+5?      5?      5?      5?      5?      