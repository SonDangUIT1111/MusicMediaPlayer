   M S B A M L   `   `   `  ????K  GMusicMediaPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:MusicMediaPlayer.ViewMusicMediaPlayer.View  5   	   U/clr-namespace:MusicMediaPlayer.UserControlMusic!MusicMediaPlayer.UserControlMusic  5   	   *    $MusicMediaPlayer.View.ForgotPassword   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:MusicMediaPlayer.View     5   	   _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   8uc/clr-namespace:MusicMediaPlayer.UserControlMusic  5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   -   5        $??
ForgotPass??5   	      MaterialDesignBody#???   5	   	   $??Regular"?5
   	   $??13(?5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=?5   	      TextRenderingMode$
 Auto=?5   	     MaterialDesignFont#???  5   	   $	??450??5   	   $	??300??6    ? AllowsTransparency . 
??????  ? 
ResizeMode$ NoResize=?5   	    ? WindowStyle$
 None=?6    ? WindowStartupLocation$ CenterScreen=?5   	    ? Icon$ IconApp.ico??5   	    ? Title$ ForgotPassword??5   	     
RegisterVM ? DataContext# [ 5   	   .??5      ? .+?5   
   ? 	 ? RowDefinitions	 5      ?? 5      $
??auto ?6    ?? 5      .+?5       ?? 
 ?? CornerRadius$
 20??6   $??2??6'   ??5!      ?? 5"      .??5%      ?  ? Color$ #2af598??6'    ? Offset$ 1Q?67   ? 5&      $ #009efd??6'   $	 0.6Q?67   ? 5'      $ #ba55d3??6'   $
 0.05Q?67   5*      ._?5,      ?? $??/image/universe2.jpg??6    J? Opacity$	 0.1Q?66   $??UniformToFill=?5-      62   5.      ?? 50      .+?51      @   :MusicMediaPlayer.UserControlMusic.UserControlLoginRegister  52      ?? 53      $??1??6   .+?54      ?? .+?55      ?? -   5         ??Name$ EmailProtect??55      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Enter Email Protected??56      $??15(?57       % !MaterialDesignFloatingHintTextBox#??[ 58      
?????? $??0q?5:      $
??auto??5;      $	??250??5<      $??Center=?5=      ??5>      ?? *EmailProtected+?? 5@      
???????$??1??5A      $	??240??5B      $??10q?5C      5D      ?? 5E        ContactButton#??[ 6   $??45??5F      $	??120??5G      $??0,15,0,0q?5H      $??	Send code??5I      $??Heavy"?5J      ??5K      ?? *SendCodeCommand+??5L      ??  ?? ElementName$ 
ForgotPass??5M      ?? 5N      .+?5O      ?? -   5        $ CodeVerified??5O      $ 
Enter code??5P      $??15(?5Q      #??[ 5R      
?????? $??0q?5T      $
??auto??5U      $	??250??5V      $??Center=?5W      .??5X      .  (System.Windows.Interactivity.Interaction   Triggers 5Y      /  )System.Windows.Interactivity.EventTrigger  5Z         	EventName$ TextChanged??6-   .  (System.Windows.Interactivity.TriggerBase   Actions. 5[   "   6  0System.Windows.Interactivity.InvokeCommandAction     Command 68   ?? *	CheckCode+   CommandParameter 5\   $   ?? $ 
ForgotPass??5]      .??5^   2   5_      ?? 5`      
???????$??1??5a      $	??240??5b      $??10q?5c      5d      ?? 5e      -   5         ??Name$ VerifiedButton??5e      #??[ 5f      $??45??5g      $	??120??5h      $??0,15,0,0q?5i      $??Verified??5j      $??Heavy"?5k      {?.  ??5m      ?? *VerifiedCommand+??5n      ?? $ 
ForgotPass???? 5p      $??0,10,0,0q?6!   
???????$	 0.5Q?5r   !   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth5=?5s   !   5t      ?? 5u      $??0,0,0,0q?6   .+?5v      G? -   5         G?Name$ NewPassword??5v   "   $ Enter new password??5w      $??15(?5x       ) %MaterialDesignFloatingHintPasswordBox#??[ 5y      
?????? $??0q?5{      $
??auto??5|      $	??250??5}      $??Center=?5~       5?        5?      $ PasswordChanged??6-   . 5?   "      68   ?? *NewPasswordChangedCommand+ 5?   $   ?? $ NewPassword??5?      5?      ?? 5?      
???????$??1??5?      $	??240??5?      $??10q?5?      5?      5?      ?? 5?      $??0,10,0,0q?6   .+?5?      G? -   5        $ ConfirmNewPassword??5?   "   $ Confirm new assword??5?      $??15(?5?      #??[ 5?      
?????? $??0q?5?      $
??auto??5?      $	??250??5?      $??Center=?5?       5?        5?      $ PasswordChanged??6-   . 5?   "      68   ?? *" ConfirmNewPasswordChangedCommand+ 5?   $   ?? $ ConfirmNewPassword??5?      5?      ?? 5?      
???????$??1??5?      $	??240??5?      $??10q?5?      5?      5?      ?? 5?      $??0,15,0,0q?6!   .+?5?      ?? $	??150??6   $??40??6)   $??Change Password??5?      $??Center=?5?      $??Center=?5?      #??[ 5?       ?? 	IsDefault . $??Heavy"?5?      ??5?      ?? *ChangePasswordCommand+??5?      ?? $ 
ForgotPass???? 5?       ?? Orientation$ 
Horizontal=?6!   $??0,10,0,0q?5?   !   .+?5?      ?? $??Have an accout already ???6$   
???????$??65,0,0,0q?5?   $   $??Light"?5?   $   $??Candara)?5?   $   6;   ?? 5?      $??Log in??5?   !   $	??Top=?5?   !   $??0q?5?   !   $??Right=?5?   !   $	??Top=?5?   !   $??12(?5?   !   $??Medium"?5?   !    ? Cursor$
 Hand??5?   !     	Secondary#???  5?   !   $??5,0,30,0q?5?   !   ??.   ? FocusVisualStyle 5?   !   Z? ??5?   !   Z? ??5?   !   Z? ??5?   !   ?? *	ToLogin+??5?   !   ?? $ 
ForgotPass??5?      5?      5?      5?      5?      5?      5?      