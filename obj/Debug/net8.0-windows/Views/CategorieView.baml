   M S B A M L   `   `   `  ����E  ABiblioGest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:BiblioGest.HelpersBiblioGest.Helpers  5      $    BiblioGest.Views.CategorieView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      1conv clr-namespace:BiblioGest.Helpers     5        � 	Resources  5      2   ,BiblioGest.Helpers.BoolToVisibilityConverter   BoolToVisibilityConverter��%   &	          5   
   6)   5      .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��10q�6;   .+�5      �� $	��300��6   $��0,0,10,0q�6"   ��64   �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=��� 5      $��
Rechercher��6   $	��100��6L   ��6*   �� *SearchCommand+6L   5      &   System.Windows.Controls.DataGrid  5   
   $��1��6      AutoGenerateColumns$ False��5         
IsReadOnly$
 True��5      $��10q�5         SelectionMode$ Single=�5      ��5      �� *
Categories+ �� SelectedItem 5      �� *SelectedCategorie+   Columns 5       0  *System.Windows.Controls.DataGridTextColumn  5!      ,  &System.Windows.Controls.DataGridColumn	   Header$		 Nom��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6J   1  +System.Windows.Controls.DataGridBoundColumn   Binding 62   �� *Nom+6J     5"      $	 Description��6%   $
 2* 6Z    6:   �� *Description+6Z   5$      �� 5'   
   $��2��6   $ 
Horizontal=�6"   $��Right=�6;   $��10q�6W   .+�5(      �� $��Ajouter��6   $��0,0,10,0q�6N   $��80��6`   ��6'   �� *ShowAddFormCommand+6`   �� 5)      $��Modifier��6   $��0,0,10,0q�6P   $��80��6b   ��6(   �� *ShowEditFormCommand+6b   �� 5*      $��	Supprimer��6   $��0,0,10,0q�6K   $��80��6]   ��6)   �� *DeleteCommand+6]   5+      �� 5.   
   ���	#AA000000$��3��5/      y�5.   (   �� *IsFormVisible+ �� 	Converter# [  ._�50      �� $��Center=�6   $��Center=�62   
������� �� Padding$ 20q�51   (   $	��400��65    �� CornerRadius$ 8��6A   ._�52      �� .+�53      �� $
��Bold"�6;   $��16(�6M   $��0,0,0,10q�6[   ��6    �� *	FormTitle+6[   � 55       56      �� 57      $
��Auto �6,   �� 58      $
��Auto �6,    � ColumnDefinitions 5:      �� 5;      $	��120 �6/   �� 5<      $��* �6/   .+�5@      �� $
��Nom:��6$   $��0��60   $��0��6=   $��Center=�6M   $��5q�6h   �� 5A      $��0��6"   $��1��6/   $��5q�6?   ��6J   �� *FormCategorie.Nom+�� 5D      $��Description:��6$   $��1��68   $��0��6E   $��Center=�6U   $��5q�6p   �� 5E      $��1��6"   $��1��6/   $��5q�6?    �� AcceptsReturn$
 True��5F   "    �� TextWrapping$
 Wrap=�67   $��60��6K   ��5E   J   �� *FormCategorie.Description+5F   K   5G      �� 5I      $ 
Horizontal=�6!   $��Right=�6:   $��0,20,0,0q�6V   .+�5J      �� $��Annuler��6!   $��0,0,10,0q�6Y   $��80��6k   ��63   �� *CancelFormCommand+6k   �� 5K      $��Valider��6!   $��80��6Y   ��63   �� *SubmitFormCommand+6Y   5L      5M      5N      5O      5P      5Q      