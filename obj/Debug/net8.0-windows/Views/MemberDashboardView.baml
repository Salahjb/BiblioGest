   M S B A M L   `   `   `  ����  
BiblioGest*    $BiblioGest.Views.MemberDashboardView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      � $��20q�6     � RowDefinitions  5	   
   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��Catalogue de Livres��5      $��24(�5      $
��Bold"�5      $��0,0,0,20q�5      � 5   
   $��1��6   $��0,0,0,20q�6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5      �� $��0��6   $��10q�5      $��0,0,10,0q�5      ��5      �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=�5      �� 5       $
��🔍��6   $��1��5!      $��16(�5"      $��Center=�5#      5$      &   System.Windows.Controls.DataGrid  5'   
   $��2��6      AutoGenerateColumns$ False��5)         
IsReadOnly$
 True��5*         SelectionMode$ Single=�5+         GridLinesVisibility$ 
Horizontal=�5,         RowHeaderWidth$ 0Q�5-      
�������   AlternatingRowBackground �#F5F5F5$��1q�50      ���#DDDDDD��5(      �� *Livres+	   Columns	 52      0  *System.Windows.Controls.DataGridTextColumn  53      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Titre��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6N   1  +System.Windows.Controls.DataGridBoundColumn   Binding 64   �� *Titre+6N     54      $
 Auteur��6%   $ * 6P    65   �� *Auteur+6P     55      $

 ISBN��6%   $
 Auto 6L    63   �� *ISBN+6L     56      $
 Année��6%   $
 Auto 6Y    64   �� *AnneePublication+6Y   58      59      5:      