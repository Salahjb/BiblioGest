   M S B A M L   `   `   `  ����E  ABiblioGest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:BiblioGest.ViewsBiblioGest.Views  5      7 clr-namespace:BiblioGest.HelpersBiblioGest.Helpers  5      "    BiblioGest.Views.EmpruntView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      0localclr-namespace:BiblioGest.Views     5      1conv clr-namespace:BiblioGest.Helpers     5        � 	Resources  5      2   ,BiblioGest.Helpers.BoolToVisibilityConverter   BoolToVisibilityConverter��%   &	          5   
   6)   5      .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��10q�6;   .+�5      �� $	��300��6   $��0,0,10,0q�6"   ��64   �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=��� 5      $��
Rechercher��6   $	��100��6L   ��6*   �� *SearchCommand+6L   5      &   System.Windows.Controls.DataGrid  5   
   $��1��6      AutoGenerateColumns$ False��5         
IsReadOnly$
 True��5      $��10q�5         SelectionMode$ Single=�5      ��5      �� *
Emprunts+ �� SelectedItem 5      �� *SelectedEmprunt+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 ID��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 60 6H   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+6H     5       $	 	Adhérent��6%   $	
 150 6X    67   �� *Adherent.Nom+6X     5!      $	 Livre��6%   $
 * 6T    64   �� *Livre.Titre+6T     5"      $	 Date d'emprunt��6%   $	
 120 6v    6=   �� *DateEmprunt+ �� StringFormat$ 
dd/MM/yyyy��6v     5#      $	 Date limite��6%   $	
 120 6x    6:   �� *DateRetourPrevue+$ 
dd/MM/yyyy��6x     5$      $	 	Retourné��6%   $	
 120 6x    67   �� *DateRetourEffective+$ 
dd/MM/yyyy��6x     5%      $	 État��6%   $	
 100 6N    63   �� *Statut+6N   5'      �� 5*   
   $��2��6   $ 
Horizontal=�6"   $��Right=�6;   $��10q�6W   .+�5+      �� $��Ajouter��6   $��0,0,10,0q�6N   $��80��6`   ��6'   �� *ShowAddFormCommand+6`   �� 5,      $��Modifier��6   $��0,0,10,0q�6P   $��80��6b   ��6(   �� *ShowEditFormCommand+6b   �� 5-      $��	Retourner��6   $��0,0,10,0q�6O   $��80��6a   ��6)   �� *ReturnBookCommand+6a   �� 5.      $��	Supprimer��6   $��0,0,10,0q�6K   $��80��6]   ��6)   �� *DeleteCommand+6]   �� 5/      $��Enregistrer��6   $��80��6K   ��6+   �� *SaveCommand+6K   50      �� 53   
   ���	#AA000000$��3��54      y�53   (   �� *IsFormVisible+ �� 	Converter# [  ._�55      �� $��Center=�6   $��Center=�62   
������� �� Padding$ 20q�56   (   $	��450��65    �� CornerRadius$ 8��6A   ._�57      �� .+�58      �� $
��Bold"�6;   $��16(�6M   $��0,0,0,10q�6[   ��6    �� *	FormTitle+6[   � 5:       � ColumnDefinitions 5;      �� 5<      $	��120 �6/   �� 5=      $��* �6/    5?      �� 5@      $
��Auto �6,   �� 5A      $
��Auto �6,   �� 5B      $
��Auto �6,   �� 5C      $
��Auto �6,   �� 5D      $
��Auto �6,   .+�5H      �� $��
Adhérent:��6$   $��0��65   $��0��6B   $��Center=�6R   $��5q�6m   �� 5I      $��0��6#   $��1��60   $��5q�6@    �� DisplayMemberPath$	 Nom��5L   #   ��5J   #   �� *	Adherents+ 5K   #   �� *SelectedAdherent+5L   #   �� 5O      $��Livre:��6$   $��1��62   $��0��6?   $��Center=�6O   $��5q�6j   �� 5P      $��1��6#   $��1��60   $��5q�6@   $ Titre��5S   #   ��5Q   #   �� *LivresDisponibles+ 5R   #   �� *SelectedLivre+5S   #   �� 5V      $��Date d'emprunt:��6$   $��2��6;   $��0��6H   $��Center=�6X   $��5q�6s   (  "System.Windows.Controls.DatePicker  5W      $��2��6%   $��1��62   $��5q�6B      SelectedDate 6M   �� *FormEmprunt.DateEmprunt+�� 5Z      $��Date limite:��6$   $��3��68   $��0��6E   $��Center=�6U   $��5q�6p     5[      $��3��6%   $��1��62   $��5q�6B    6M   �� *FormEmprunt.DateRetourPrevue+�� 5^      $��Date de retour:��6$   $��4��6;   $��0��6H   $��Center=�6X   $��5q�6s   y�5_   $   �� *IsReturnVisible+# [    5`      $��4��6%   $��1��62   $��5q�6B    6M   �� *!FormEmprunt.DateRetourEffective+y�5a   $   �� *IsReturnVisible+# [  5b      �� 5d      $ 
Horizontal=�6!   $��Right=�6:   $��0,20,0,0q�6V   .+�5e      �� $��Annuler��6!   $��0,0,10,0q�6Y   $��80��6k   ��63   �� *CancelFormCommand+6k   �� 5f      $��Valider��6!   $��80��6Y   ��63   �� *SubmitFormCommand+6Y   5g      5h      5i      5j      5k      5l      