   M S B A M L   `   `   `  ????\  XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  ?? Color ?? 
TargetType ?? 
ResourceId ?? TypeInTargetAssembly&     System.Windows.Controls.DataGrid ?? Property ?? Value ?? 
TargetType ??Name J? SnapsToDevicePixels	 ?Name
 J? Opacity ? Points ?? Stretch ?? Triggers P? Property P? Value ?? 
TargetName   DataGridStyle1 ?? Padding ??Name ? ColumnDefinitions ? RowDefinitions    SelectAllCommand7   1System.Windows.Controls.DataGridHeadersVisibility
   All ?? ConverterParameter!    HeadersVisibilityConverter ?? 	Converter ?? RelativeSource ?? AncestorTypeG   ASystem.Windows.Controls.Primitives.DataGridColumnHeadersPresenter  Name   Column ??Name ?? CanContentScroll  ??Name! ?? ScrollableHeight" ? Maximum# ?? Orientation$ ?? ViewportHeight% ?? ViewportSize*& ?? #ComputedVerticalScrollBarVisibility' ? Value( ?? Mode) ?? ScrollableWidth* ?? ViewportWidth,+ ?? %ComputedHorizontalScrollBarVisibility, ?? Triggers- ]? 
Conditions. ?? Property/ ?? Value  DataGridCellStyle1*   $System.Windows.Controls.DataGridCell  DataGridRowStyle1)   #System.Windows.Controls.DataGridRow0 ??Name1 ?? CornerRadius?   9System.Windows.Controls.Primitives.SelectiveScrollingGrid?   9System.Windows.Controls.Primitives.DataGridCellsPresenter2   
ItemsPanelA   ;System.Windows.Controls.Primitives.DataGridDetailsPresenter3   DetailsVisibility$4   SelectiveScrollingOrientation;   5System.Windows.Controls.SelectiveScrollingOrientation5   Vertical#6    RowDetailsScrollingConverter:	   4System.Windows.Controls.Primitives.DataGridRowHeader
7   Row?? 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %G  (??       5      *DataGrid.FocusBorderBrushKey+)(?? #     5      $" DataGridSelectAllButtonStyle??"   )&	  ?    &	 %    &	 ?    ?? 5      $  	#FF000000??6F   ?? 5      " ??6{   .?5   
   ??  ? ?? 5      ?? 5      " ??6"   .R?5      ? .+?5	      ?? $ Border??6$   #??? ??64    . 6u   ? 5
      $	 Arrow??6"   
???   ?$??Right=?6>   $??8,8,3,3q?6Z   $

 0.15Q?6k    ?   ??? ??? ??? ??? $ Uniform=?6?   $??Bottom=?6?   5       5      P? 5       ?@J?IsMouseOver . . ?5      ?? $ Border??67   # ? ??6K    ? ??5      P? 5       ? ?? . . ?5      ?? $ Border??65   # ? ??6I    ? ??5      P? 5       ? {? .  . ?5      ?? $ Arrow??6;    ? y?$ 	Collapsed=?6N   5      5      5      5      ?? 5      "   6#   .?5   
   ??  ? ??
 ???? 6'   ?? 5   
    ? ?? ?#ffffff6'   ?? 5   
    ? ??$ 0q?6,   ?? 5   
     ?@  RowDetailsVisibilityMode$ VisibleWhenSelected=?65   ?? 5    
    ? ?? . 6:   ?? 5!   
    ?@??PanningMode$
 Both=?65   ?? 5"   
    ?@??IsFlicksEnabled .  63   ?? 5#   
    ?@  AutoGenerateColumns .  6@   ?? 5$   
    ?@  HeadersVisibility$
 None=?6.   6=   ?? 5%   
    ?@  SelectionMode$ Single=?6*   6;   ?? 5&   
    ?@  SelectionUnit$ FullRow=?6*   6<   ?? 5'   
    ?@  CanUserAddRows .  6;   ?? 5(   
    ?@  CanUserDeleteRows .  6>   ?? 5)   
    ?@  CanUserReorderColumns .  6B   ?? 5*   
    ?@  CanUserResizeColumns .  6A   ?? 5+   
    ? ??$ 13(?6%   62   ?? 5,   
    ?@  GridLinesVisibility$
 None=?60   6?   ?? 5-   
    ? ?? 5.      ?? 5/      "   6"   .R?50      ?? #??z??6   #??z??6G   #??z??6s   # z??6?    . ._?51      ?? $ DG_ScrollViewer??6'   |?.  ??52      ?? 53   "   " ??62   .R?54   &   ?  55   *   ?? 56   .   $
??Auto ?6?   ?? 57   .   $??* ?6?   ?? 58   .   $
??Auto ?6?    5:   *   ?? 5;   .   $
??Auto ?6<   ?? 5<   .   $??* ?6<   ?? 5=   .   $
??Auto ?6<   .+?5?   *   ?? #??Z 61   |?.  ??6r   C? *?? $" DataGridSelectAllButtonStyle??"   +y?6?   ?? *HeadersVisibility+# Z # Z  ?? "   ??6?  ?? *CellsPanelHorizontalOffset+ ?? "     5@   *   $! PART_ColumnHeadersPresenter??6I   $??1??6n   y?6~   ?? *HeadersVisibility+# Z # Z  ?? "   ?? 5A   *   $! PART_ScrollContentPresenter??6A   # z??6f   $??2??6?   $??1??6?   ?? 5B   *   $  PART_VerticalScrollBar??64   $??2??6T   #" z! 6d   $# Vertical=?6?   $??1??6?   #% z$ 6  #y?z& 6D  ' 6?   ?? *VerticalOffset+$( OneWay=? ?? *TemplatedParent+6D  ? 5C   *   $??1??6/   $??2??6?    5D   .   ?? 5E   2   ??6C   ?? **(NonFrozenColumnsViewportHorizontalOffset+ ?? "   ?? 5F   2   $??* ?6C   .+?5H   .   ?? $  PART_HorizontalScrollBar??68   $??1??6Z   #" z) 6j   $# 
Horizontal=?6?   #% z* 6  #y?z+ 6?  ' 6?   ?? *HorizontalOffset+$( OneWay=? ?? *TemplatedParent+6?  5I   +   5J   '   5K   #   .??5M      ?? # z 6-   5N      5O      5P      5R      , 5S   
   ]? 5T      - 5U      ?? 5V      . ?@??
IsGrouping/ . 66   ?? 5W      ". ?@%?IsVirtualizingWhenGrouping/ .  6X   ./?5Y      ??  ? ?? .  6B   5Z      5\      ?? 5]      "  6'   .?5^   
   ??  ? ??
 ???? 6'   ?? 5_   
    ? ??$ Center=?6.   6?   ?? 5`   
    ?@?FocusVisualStyle 6-   Z? ?? 5a   
    ? ??$
 15 0q?6#   ?? 5b   
    ? ??$ 50??6#   ?? 5c   
    ? ?? 5d      ?? 5e      "  6"   .R?5f      ?? #??z??6   $??0q?6G    . ._?5g      ?? # z 6+   5h      5i      5k      5l      ?? 5m      "  6&   .?5n   
   ??  ? ?? ?#2225286'   ?? 5o   
    ? ??$ 0 4 0 0q?6#   ?? 5p   
    ?@J?SnapsToDevicePixels . 60   ?? 5q   
    ?@=?ErrorTemplate 65   Z? ?? 5r   
    ?@ ValidationErrorTemplate 5s      ?? 5t      .R?5u      ?? 
???  ??$??2,0,0,0q?61   $??!??6B   $??Center=?6K   5v      5x      ?? 5y   
    ? ?? 5z      ?? 5{      "  6"   .R?5|      ?? $0 
DGR_Border??6   $	 0 8q?61   #??z??6?   $??0q?6i   $1 5??6}    . ._?5}         5~      ?? 5   "   $
??Auto ?63   ?? 5?   "   $??* ?63    5?      ?? 5?   "   $??* ?60   ?? 5?   "   $
??Auto ?60   .+?5?        $??1??65   #??z2 6E   # z 6o     5?      $??1??67   $??1??6G   #y?z3 6k  4 6T   ?? *AreRowDetailsFrozen+# Z5 # Z6  ?? "   6k  	  5?      $??2??60   $4 Vertical=?6A   y?6?   ?? *HeadersVisibility+# Z7 # Z  ?? "   5?      5?      5?      5?      , 5?   
   P? 5?       ?@J?IsMouseOver . . ?5?      ??  ? ?? ?#2225406/   ?? 5?       ? ?? ?#1ED7606/   5?      P? 5?       ?@ 
IsSelected . . ?5?      ??  ? ?? ?#1ED7606/   ?? 5?       ? ??
 ?   ?6/   5?      ]? 5?      - 5?      ?? 5?      . ?@ 
IsSelected/ . 6E   ?? 5?      . ?@??IsSelectionActive/ .  6V   ./?5?      ??  ? ?? ?#1ED7606/   ?? 5?       ? ??
 ?   ?6/   5?      5?      5?      