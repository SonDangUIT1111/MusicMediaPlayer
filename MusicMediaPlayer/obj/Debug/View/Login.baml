   M S B A M L   `   `   `  ????K  GMusicMediaPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MusicMediaPlayerMusicMediaPlayer  5   	   U/clr-namespace:MusicMediaPlayer.UserControlMusic!MusicMediaPlayer.UserControlMusic  5   	   !    MusicMediaPlayer.View.Login   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:MusicMediaPlayer     5   	   >uc/clr-namespace:MusicMediaPlayer.UserControlMusic     5   	   _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   -   5        $??LoginWindow??5   	      MaterialDesignBody#???   5
   	   $??Regular"?5   	   $??13(?5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=?5   	      TextRenderingMode$
 Auto=?5   	     MaterialDesignFont#???  5   	    ? Title$ Login??5   	   $	??450??6   $	??750??6$    ? AllowsTransparency . 
??????  ? 
ResizeMode$ NoResize=?5   	    ? WindowStyle$
 None=?6    ? WindowStartupLocation$ CenterScreen=?5   	     LoginVM ? DataContext# [ 5   	    ? Icon$ IconApp.ico??5   	   	 ? 	Resources	 5        ImageTransition1
 ?? 	UriSource  ImageTransition2  ImageTransition3?? 5   
   %?   &	       &	 7     &	 p     ?? 5      $
 /Image/Streaming2017.jpg??63   6Z   ?? 5      $ 
 /Image/streamingimage2.jpg??63   6\   ?? 5      $2
 ,/Image/talk-about-faverte-music-1024x682.jpg??63   6n   5      .??5       ?  ? ColumnDefinitions 5!   
   ?? 5"      6"   ?? 5#      $
??auto ?6   6.   .+?5%   
   ??  ?? CornerRadius$ 	20 0 0 20??6   $??0??6*    K? ZIndex$ 1??6:   $	??0.1q?5&      
?????? $??Center=?5(      ._?5)      ? .+?5*      ?? -   5         ??Name$
 mask??5-      
???????$ 	20 0 0 20??5+      $	??0.1q?5,      5-   )   ?? 5.      #??[ 6   $??UniformToFill=?5/       ? Triggers 51      :? 52       :? RoutedEvent$ Loaded??6'   .[?53      ?? .??54   "   ?? .??55   &   W?  ?? TargetProperty$ Opacity?56   %    g? 
SpeedRatio$ .5Q?57   %    W? From$ 1X?58   %   	 W? To$	 0.5X?59   %    g? RepeatBehavior$ Forever??5:   %   W? 5<   &   $ Opacity?5=   %   $	 0.5Q?5>   %   $	 0.5X?5?   %   $ 1X?5@   %   $ Forever??5A   %   T? 5C   &   $ Source?5D   %    g? Duration$ 0:0:6D?5E   %   $ Forever??5F   %   ..?5G   *   i?  R? Value# [ 5H   )    R? KeyTime$ 0:0:2??5I   )   i? 5J   *   # [ 5K   )   $ 0:0:4??5L   )   5M   '   5N   #   5O      5P       J? OpacityMask 5R      !? 5S       !? Visual 6&   ??  ?? ElementName$
 mask??6L   5U      5V      5W      ? 5X   
   $??1??6   .+?5Y      ? $	??450??6   $	??300??5Z       ? RowDefinitions 5[      ?? 5\      $	??200 ?6$   ?? 5]      $
??229* ?6$   ?? 5^      $	??16* ?6$   ?? 5_      $??5* ?6$   .+?5b      ?? $??4??6   $ 	0 20 20 0??6*   $ 0??6C   ??5c      ?? 5i       ?? Color$ #07BF96??6*   5m      A? 5n      $	??200??6   $	??200??5o      ???#FFFFFF J? Opacity$	 0.1Q?5q      
?????? $??219,140,-119,-90q?5s      $??1??5t      $ 0??5u      $??3??6+   A? 5v      $	??200??6   $	??200??5w      ???#FFFFFF$	 0.2Q?5y      
?????? $??-50,127,150,-77q?5{      $??1??5|      $ 0??5}      $??3??6+   A? 5~      $	??100??6   $	??100??5      ???#FFFFFF$	 0.2Q?5?      
?????? $??-34,190,234,0q?5?      $??1??5?      $ 0??5?      $	??Top=?6+   $??3??6C   A? 5?      $	??300??6   $	??300??5?      ???#FFFFFF$	 0.2Q?5?      
?????? $??100,54,-100,-104q?5?      $??1??5?      $ 0??5?      $??3??6+   ?? 5?      .+?5?      @   :MusicMediaPlayer.UserControlMusic.UserControlLoginRegister  ?? 5?      $??75??6   $??75??5?      $??Center=?5?      $??0,20,0,0q?5?      
???????  NormalButton#??[ 5?       J? RenderTransformOrigin$ 0.5,0.5&?5?      .??5?      ?? $??/Image/logomusicapp.png??6    6C   z?5?      ?? 5?      -   5        MyAnimatedTransform??  ?? Angle$  0Q?5?   K    5?      :? 5?      $ Loaded??6+   .[?5?   "   ?? .??5?   &   ?? .??5?   *   W? ! ?? 
TargetName$! MyAnimatedTransform??5?      $ (RotateTransform.Angle)?5?      $	 0.0X?5?      $	 360X?6   $ 0:0:3D?6%   $ Forever??66   5?   '   5?   #   5?      5?      ?? 5?      $??Music Universe??6    $??20(?5?      $
??Bold"?5?      $??Center=?5?      
???????$??Candara)?5?      $??0,10,0,0q?5?      60   5?      ?? 5?      $??1??6   $??3??6*   .+?5?      ?? .+?5?      ?? " ?? Orientation$" 
Horizontal=?6%   .+?5?      ?? -   5        # ??Name$# Account??5?   &   )  #MaterialDesignThemes.Wpf.HintAssist$   Hint$$ 	User Name??5?      $??15(?5?       % !MaterialDesignFloatingHintTextBox#??[ 5?      
?????? $??0q?5?      $
??auto??5?      $	??210??5?      $??Center=?5?      $??30,0,0,0q?5?      ??5?      ?? *
Username+'  !MaterialDesignThemes.Wpf.PackIcon  5?      %   Kind$% AccountOutline=?66   $??25??5?   2   $??25??5?   2   
???????$??0,10,0,0q?5?   2   $	 0.8Q?5?   2   5?      ?? 5?      
???????$??1??5?      $	??240??5?      $??10q?5?      5?      5?      ?? 5?      $??0,10,0,0q?6!   .+?5?      ?? $" 
Horizontal=?6%   .+?5?      G? -   5        & G?Name$& Password??5?   *   $$ Password??5?      $??15(?5?       ) %MaterialDesignFloatingHintPasswordBox#??[ 5?      
?????? $??0q?5?      $
??auto??5?      $	??210??5?      $??Center=?5?      $??30,0,0,0q?5?      .  (System.Windows.Interactivity.Interaction'   Triggers' 5?   "   /  )System.Windows.Interactivity.EventTrigger  5?   &   (   	EventName$( PasswordChanged??65   .  (System.Windows.Interactivity.TriggerBase)   Actions.) 5?   *   6  0System.Windows.Interactivity.InvokeCommandAction  *   Command* 6@   ?? *PasswordChangedCommand++   CommandParameter+ 5?   $   ?? $ Password??5?   '   5?        5?      $% 
KeyVariant=?66   $??25??5?   2   $??25??5?   2   
???????$??0,10,0,0q?5?   2   $	 0.8Q?5?   2   5?      ?? 5?      
???????$??1??5?      $	??240??5?      $??10q?5?      5?      5?      ?? 5?      $??0,10,0,0q?6!   .+?5?      ?? $??Forgot password???5?      $	??Top=?5?      $??0q?5?      $??Right=?5?      $??Center=?5?      $??12(?5?      $??Medium"?5?      , ? Cursor$
, Hand??5?       	 	Secondary#??? 	 5?      $??
150,0,30,0q?5?      ??.  $
??auto??5?      $	??100??5?      - ? FocusVisualStyle- 5?      Z? ??5?      Z? ??5?      Z? ??5      ?? *ToForgotPassword+??5     ?? $ LoginWindow??5     ?? 5     $	??100??6!   $??40??6-   $??Login??5     . ?? 	IsDefault. . $??Center=?5     $??Center=?5      
 ContactButton#??[
 5     $??0,10,0,0q?5	     ??5
     ?? *LoginSuccess+??5     ?? $ LoginWindow???? 5     $" 
Horizontal=?6%   $??0,10,0,0q?5  !   .+?5     ?? $??Don't have an accout ???6(   
???????$??70,0,0,0q?5  $   $??Light"?5  $   $??Candara)?5  $   6;   ?? 5     $??Sign up??5  %   $	??Top=?5  %   $??0q?5  %   $??Right=?5  %   $	??Top=?5  %   $??12(?5  %   $??Medium"?5  %   $
, Hand??5  %   #??? 	 5  %   $??5,0,30,0q?5   %   ??.  - 5  %   Z? ??5  %   Z? ??5  %   Z? ??5"  %   ?? *LogintoRegister+??5#  %   ?? $ LoginWindow??5%     5&     5'     5(     5)     5*     5+     5,     