   M S B A M L   `   `   `  ????K  GMusicMediaPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MusicMediaPlayerMusicMediaPlayer  5      +    %MusicMediaPlayer.View.PlayList_Inside   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:MusicMediaPlayer     5      _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      -   5        $??PlayList_Inside_Page??5        N? Title$  PlayList_Inside??5
         PlayList_InsideVM ? DataContext# [  5      .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded??6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   ?? *Loaded+   CommandParameter 5   $   ??  ?? ElementName$ PlayList_Inside_Page??5      .??5      ? $	??450??6   $	??800??6    ? RowDefinitions 5   
   ?? 5      $	??150 ?6   5      ?? 5      5      .+?5"   
   ? $??0??6   
???????.+?5$      ?? 	 ?? Orientation$	 
Horizontal=?6   .+?5%      A? $	??100??6   $	??100??6'   $??40 0 0 0q?5&      ??5'      ?? 5(      
 ?? ImageSource$
 /Image/anh3.jpg??6%   5*      ? 5+      $	??500??6   $??
30 20 0 20q?5,       5-      ?? 5.      6*   ?? 5/      6*   .+?52      ?? $??0??6    $??35(?54       $
??Bold"?55       $??Italic$?56        ?? TextAlignment$
 Left=?57       ??53       ?? *PLName+59      ?? 5;      $??1??6!   $	 
Horizontal=?5<   !   .+?5=      ?? $??Rename??6!   ??5>   !   ?? *Rename+5B      ?? 5C      $??Delete PlayList??6!   $??	10 0 0 0 q?5D   "   ??5E   "   ?? *DeletePlayList+??5F   !   ?? $ PlayList_Inside_Page??5H      ?? 5I      $??	Add Songs??6!   $??	10 0 0 0 q?5J   "   ??5K   !   ?? *
AddSongs+5M      ?? 5N      $??	10 0 0 0 q?5O   %   $??15(?5P   %   $
??Bold"?5Q   %   $
??Wrap=?5R   %   $??Italic$?5S   %   $	??150??5T   %   $??Center=?5U   %   $??Center=?5V   %   ??5N   %   ?? *	SongCount+5V   %   5X      5Y      5Z      5[      ? 5\   
   $??1??6   
???????.+?5^      ?? -   5        $??listview??5`      ??5_      ?? *List+ ?? Mode$ TwoWay=???5b      ?? 5c       ?? 
TargetType" ??6    ?? 	Resources 5d       ?? Property ?? Value??%g   '??       ?? 5e      " ??6$   .?5f   "   ??  ?@??CornerRadius$ 5??6A   5g   #   5h      5i      5j      ??5m      ?? 5n      .R?5o      ? $??70??6    $	??580??5p   $   .+?5q      ?? $??
15 10 0 10q?6)   $	 
Horizontal=?5r   -   .+?5t   "   A? $??48??6*   $??48??66   $??Center=?6A   ??5u   &   ?? 5v   *   $
 /Image/anh3.jpg??65   5x   #   ?? 5z   "   $??15 0 0 0q?6-   $	??440??6?   .+?5|   &   ??  J? Opacity$ 1Q?60   
???   ?$
??Bold"?6j   $??20(?6|   ??6<   ?? *	SongTitle+6|   ?? 5}   &   $ 1Q?60   
???   ?$
??Bold"?6g   $??17(?6y   ??6<   ?? *Artist+6?   5~   #   '  !MaterialDesignThemes.Wpf.PopupBox  5?   "   $??20 10 10 10q?5?   =   $??2 0 2 0q?5?   =      PopupContent. 5?   &   ?? .+?5?   *   ?? $??Delete??61   ??5?   5   ??  ?? Source# [   ?? Path$ Delete_One_Song???5?   5   ?? 5?   +   5?   '   5?   #   5?      5?      5?      5?      5?      5?      5?      