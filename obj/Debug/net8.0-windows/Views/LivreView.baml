   M S B A M L   `   `   `  ����E  ABiblioGest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:BiblioGest.ViewsBiblioGest.Views  5      7 clr-namespace:BiblioGest.HelpersBiblioGest.Helpers  5           BiblioGest.Views.LivreView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      0localclr-namespace:BiblioGest.Views     5      1conv clr-namespace:BiblioGest.Helpers     5        � 	Resources  5      2   ,BiblioGest.Helpers.BoolToVisibilityConverter   BoolToVisibilityConverter��%   &	          5   
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
 True��5      $��10q�5         SelectionMode$ Single=�5      ��5      �� *Livres+ �� SelectedItem 5      �� *SelectedLivre+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$
	 ISBN��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 120 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *ISBN+6L     5       $	 Titre��6%   $
 * 6N    64   �� *Titre+6N     5!      $	 Auteur��6%   $	
 150 6P    65   �� *Auteur+6P     5"      $	 Éditeur��6%   $	
 120 6R    66   �� *	Editeur+6R     5#      $	 Année��6%   $
 70 6Y    64   �� *AnneePublication+6Y     5$      $	 
Catégorie��6%   $	
 120 6Z    68   �� *Categorie.Nom+6Z     5%      $	 Exemplaires��6%   $
 90 6`    6:   �� *NombreExemplaires+6`   5'      �� 5*   
   $��2��6   $ 
Horizontal=�6"   $��Right=�6;   $��10q�6W   .+�5+      �� $��Ajouter��6   $��0,0,10,0q�6N   $��80��6`   ��6'   �� *ShowAddFormCommand+6`   �� 5,      $��Modifier��6   $��0,0,10,0q�6P   $��80��6b   ��6(   �� *ShowEditFormCommand+6b   �� 5-      $��	Supprimer��6   $��0,0,10,0q�6K   $��80��6]   ��6)   �� *DeleteCommand+6]   �� 5.      $��Enregistrer��6   $��80��6K   ��6+   �� *SaveCommand+6K   5/      �� 52   
   ���	#AA000000$��3��53      y�52   (   �� *IsFormVisible+ �� 	Converter# [  ._�54      �� $��Center=�6   $��Center=�62   
������� �� Padding$ 20q�55   (   $	��450��65    �� CornerRadius$ 8��6A   ._�56      �� .+�57      �� $
��Bold"�6;   $��16(�6M   $��0,0,0,10q�6[   ��6    �� *	FormTitle+6[   � 59       � ColumnDefinitions 5:      �� 5;      $	��120 �6/   �� 5<      $��* �6/    5>      �� 5?      $
��Auto �6,   �� 5@      $
��Auto �6,   �� 5A      $
��Auto �6,   �� 5B      $
��Auto �6,   �� 5C      $
��Auto �6,   �� 5D      $
��Auto �6,   �� 5E      $
��Auto �6,   .+�5I      �� $��ISBN:��6$   $��0��61   $��0��6>   $��Center=�6N   $��5q�6i   �� 5J      $��0��6"   $��1��6/   $��5q�6?   ��6J   �� *FormLivre.ISBN+�� 5M      $��Titre:��6$   $��1��62   $��0��6?   $��Center=�6O   $��5q�6j   �� 5N      $��1��6"   $��1��6/   $��5q�6?   ��6J   �� *FormLivre.Titre+�� 5Q      $��Auteur:��6$   $��2��63   $��0��6@   $��Center=�6P   $��5q�6k   �� 5R      $��2��6"   $��1��6/   $��5q�6?   ��6J   �� *FormLivre.Auteur+�� 5U      $��	Éditeur:��6$   $��3��64   $��0��6A   $��Center=�6Q   $��5q�6l   �� 5V      $��3��6"   $��1��6/   $��5q�6?   ��6J   �� *FormLivre.Editeur+�� 5Y      $��Année:��6$   $��4��62   $��0��6?   $��Center=�6O   $��5q�6j   �� 5Z      $��4��6"   $��1��6/   $��5q�6?   ��6J   �� *FormLivre.AnneePublication+�� 5]      $��Catégorie:��6$   $��5��66   $��0��6C   $��Center=�6S   $��5q�6n   �� 5^      $��5��6#   $��1��60   $��5q�6@    �� DisplayMemberPath$	 Nom��5a   #   ��5_   #   �� *
Categories+ 5`   #   �� *SelectedCategorie+5a   #   �� 5d      $��Exemplaires:��6$   $��6��68   $��0��6E   $��Center=�6U   $��5q�6p   �� 5e      $��6��6"   $��1��6/   $��5q�6?   ��6J   �� *FormLivre.NombreExemplaires+5f      �� 5h      $ 
Horizontal=�6!   $��Right=�6:   $��0,20,0,0q�6V   .+�5i      �� $��Annuler��6!   $��0,0,10,0q�6Y   $��80��6k   ��63   �� *CancelFormCommand+6k   �� 5j      $��Valider��6!   $��80��6Y   ��63   �� *SubmitFormCommand+6Y   5k      5l      5m      5n      5o      5p      