   M S B A M L   `   `   `  ����E  ABiblioGest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:BiblioGest.ViewModelsBiblioGest.ViewModels  5   	   3clr-namespace:BiblioGest.ViewsBiblioGest.Views  5   	   7 clr-namespace:BiblioGest.HelpersBiblioGest.Helpers  5   	   clr-namespace:   5	   	       BiblioGest.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:BiblioGest.ViewModels     5   	   0viewsclr-namespace:BiblioGest.Views     5   	   1conv clr-namespace:BiblioGest.Helpers     5   	   !globalclr-namespace:     5	   	     � Title$+  %BiblioGest - Gestion de Bibliothèque��5   	   $	��600��66   $
��1000��6C    � 	Resources 5      2   ,BiblioGest.Helpers.BoolToVisibilityConverter   BoolToVisibilityConverter2   ,BiblioGest.Helpers.EnumToVisibilityConverter  EnumToVisibilityConverter��%:   &	        &	        5   
   6)     5   
   6)   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5      $��* �6   .+�5   
   �� $��0��6   ���#2c3e50.+�5      �� $��
BiblioGest��6   $��24(�6*   
�������$��20q�6K   $��Center=�6W   �� 5      -   5         ��Name$ WelcomeText��5      
�������$��	10,0,10,5q�5       �� TextAlignment$ Center=�5      ��5      �� *LoggedInAdherent.Nom+ �� StringFormat$ Bienvenue, {0}��y�5      �� *LoggedInAdherent+ �� 	Converter# [  �� 5       ���#3498db$
��Bold"�5"      $��
10,0,10,15q�5#      $ Center=�5$      ��5       �� *
UserRole+$ 	Role: {0}��y�5%      �� *LoggedInAdherent+# [  �� 5(      $��Livres��6   $��40��5*      $
��10,5q�5+      ���#3498db
�������$��0q�5.      ��5)      �� *ShowBooksCommand+y�5/      �� *LoggedInAdherent+# [  �� 52      $��
Adhérents��6   $��40��54      $
��10,5q�55      ���#3498db
�������$��0q�58      ��53      �� *ShowMembersCommand+��59      �� 5:       �� 
TargetType" ��6   .�5;      ��  �� Property � y�	 �� Value$	 	Collapsed=�67   
 �� Triggers
 5<      �� 5=       �� Binding 6*   �� *
UserRole+ �� Value	 Admin6G   .a�5>   "   ��  � y�$	 Visible=�6?   5?      5A      5C      �� 5E      $��Emprunts��6   $��40��5G      $
��10,5q�5H      ���#3498db
�������$��0q�5K      ��5F      �� *ShowLoansCommand+��5L      �� 5M      " ��6   .�5N      ��  � y�$	 	Collapsed=�67   
 5O      �� 5P       6*   �� *
UserRole+	 Admin6G   .a�5Q   "   ��  � y�$	 Visible=�6?   5R      5T      5V      �� 5X      $��
Categories��6   $��40��5Z      $
��10,5q�5[      ���#3498db
�������$��0q�5^      ��5Y      �� *ShowCategoriesCommand+��5_      �� 5`      " ��6   .�5a      ��  � y�$	 	Collapsed=�67   
 5b      �� 5c       6*   �� *
UserRole+	 Admin6G   .a�5d   "   ��  � y�$	 Visible=�6?   5e      5g      5i      �� 5k      $��	Dashboard��6   $��40��5m      $
��10,5q�5n      ���#3498db
�������$��0q�5q      ��5l      �� *ShowDashboardCommand+��5r      �� 5s      " ��6   .�5t      ��  � y�$	 	Collapsed=�67   
 5u      �� 5v       6*   �� *
UserRole+	 Admin6G   .a�5w   "   ��  � y�$	 Visible=�6?   5x      5z      5}      5~      �� 5�   
   $��1��6   ��6)   �� *CurrentViewModel+ 5�       �� DataType*   $BiblioGest.ViewModels.LoginViewModel    BiblioGest.Views.LoginView*   $BiblioGest.ViewModels.LivreViewModel    BiblioGest.Views.LivreView-   'BiblioGest.ViewModels.AdherentViewModel#   BiblioGest.Views.AdherentView,	   &BiblioGest.ViewModels.EmpruntViewModel"
   BiblioGest.Views.EmpruntView4   .BiblioGest.ViewModels.MemberDashboardViewModel*   $BiblioGest.Views.MemberDashboardView.   (BiblioGest.ViewModels.CategorieViewModel$   BiblioGest.Views.CategorieView.   (BiblioGest.ViewModels.DashboardViewModel$   BiblioGest.Views.DashboardView��%  (��       5�      *, +)(�� 2     5�      *, +)(�� d     5�      *, +)(�� �     5�      *,	 +)(�� �     5�      *, +)(�� �     5�      *, +)(�� ,    5�      *, +)�� 5�      "  6   .R�5�        5�      �� 5�      "  6   .R�5�        5�      �� 5�      "  6   .R�5�        5�      �� 5�      " 	 6   .R�5�      
  5�      �� 5�      "  6   .R�5�        5�      �� 5�      "  6   .R�5�        5�      �� 5�      "  6   .R�5�        5�      5�      5�      5�      5�      