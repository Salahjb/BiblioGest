   M S B A M L   `   `   `  ����E  ABiblioGest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:BiblioGest.ViewsBiblioGest.Views  5      7 clr-namespace:BiblioGest.HelpersBiblioGest.Helpers  5           BiblioGest.Views.LoginView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      0localclr-namespace:BiblioGest.Views     5      1conv clr-namespace:BiblioGest.Helpers     5        � 	Resources  5       �� 
TargetType �� Property �� Value2   ,BiblioGest.Helpers.BoolToVisibilityConverter   BoolToVisibilityConverter��%�   '��       &	  �     �� 5   
   " ��6   .�5      ��  � ��$
 10,5q�6(   �� 5	       � ��$ 5q�6'   �� 5
       � ��$
 Bold"�6+   5        5   
   6)   5      .��5      � ���#f0f2f5.+�5   
   �� 
������� �� Padding$ 30q�6$   $	��400��61   $��Center=�6=   $��Center=�6Z    �� CornerRadius$ 10��6u   ._�5      �� .+�5      �� $��Connexion à BiblioGest��6   $��20(�6:   $
��Bold"�6H   $��0,0,0,20q�6Z   $��Center=�6l   �� 5      $��Email :��6   $��0,0,0,5q�6+   �� 5      $��0,0,0,10q�6V   ��6   �� *Email+ �� UpdateSourceTrigger$ PropertyChanged=�6V   �� 5      $��Mot de passe :��6   $��0,0,0,5q�62   G� 5      -   5         G�Name$ PasswordBox��5      $��0,0,0,20q�63   5      �� 5      $��Se connecter��6   ���#3498db
���   ���60   �� *LoginCommand+6f   �� 5"      
���  ��$��0,10,0,0q�6K   ��6   �� *ErrorMessage+y�5#      �� *
HasError+ �� 	Converter# [  5$      5%      5&      5'      