   M S B A M L   `   `   `  ????K  GMusicMediaPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MusicMediaPlayerMusicMediaPlayer  5   	   *    $MusicMediaPlayer.View.RenamePlayList   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:MusicMediaPlayer     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   -   5        $??RenamePL??5   	     ? Title$  RenamePlayList??5	   	   $	??150??6    $	??300??6-      RenamePlayListVM ? DataContext# [  5
   	    ? 
ResizeMode$ NoResize=?5   	    ? AllowsTransparency .  ? WindowStartupLocation$ CenterScreen=?5   	    ? WindowStyle$
 None=?5   	   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded??6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction  	   Command	 6$   ?? *Load+
   CommandParameter
 5   $   ??  ?? ElementName$ RenamePL??5      .??5      ? 
??????? ? RowDefinitions 5   
   ?? 5      $??65 ?6   5      ?? 5      $??65 ?6   5      .+?5"   
   ?? -   5        $??NamePL??5%      $??0??5"      $??15(?5#      $??
20 30 20 0q?5$      .??5%        5&        5'      $ TextChanged??6!   . 5(        	 6,   ?? *TextChanged+
 5)   ,   ?? $ NamePL??5*      .??5+   &   5-      ?? 5/   
    ?? Orientation$ 
Horizontal=?6   $??1??50      $??
10 30 10 0q?51      .+?52      ?? $??	10 0 10 0q?6   $	??120??53      $
??Lưu??54      ??55      ?? *Luu+??56      ?? $ RenamePL??58      ?? 59      $??	10 0 10 0q?6   $	??120??5:      $??Cancel??5;      ??5<      ?? *Cancel+??5=      ?? $ RenamePL??5?      5@      5A      5B      