   M S B A M L   `   `   `  ����  FitnessReservatie.UI%    FitnessReservatie.UI.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   c _MaterialDesignThemes.MahApps, Version=0.2.2.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a_ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5   	   U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  5   	   :d2http://schemas.microsoft.com/expression/blend/2008  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	   	      MaterialDesignPaper#���   5   	     MaterialDesignBody#���  5   	     � Title$  Login Panel��5   	   $	��500��6   $
��1000��6*    � WindowStartupLocation$ CenterScreen=�67    � 
ResizeMode$ NoResize=�6\   .��5      � ���	#FFF3CA00.+�5   
   �� $
��Left=�6   $	��525��6+   $	��Top=�68   $	��525��6P   $��/Images/Background.png��6\   $��	0,-50,0,0q�6|   �� 5   
   $
��Left=�6   $	��525��6+   $	��Top=�68   $	��525��6P   $��/Images/Background.png��6\   $��478,-54,0,0q�6|   �� 5   
   -   5        $��ImageBackgroundKlant��5      $
��Left=�6,   $	��525��6G   $��516,-59,0,0q�6T   $	��Top=�6i   $	��525��6�   "  WpfAnimatedGif.ImageBehavior   AnimatedSource$ /Images/loopband.gif��6�   $y�Visible=�6�   �� 5   
   -   5        $��ImageBackgroundAdmin��5      $
��Left=�6,   $	��456��6G   $��567,-22,0,0q�6T   $	��Top=�6i   $	��500��6�   $ /Images/Fiets.gif��6�   $y�Hidden=�6�   �� 5   
   -   5        $��ButtonAdmin��5      $
��Left=�6$   $��438,285,0,0q�6?   $	��Top=�6T   $��35��6l   $��35��6w   $y�Hidden=�6�   $��20(�6�    & "MaterialDesignFloatingActionButton#��[ 6�   ���	#FF323232
���������6  Z� .��5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ KeyboardReturn=�6&   5      �� 5   
   -   5        $��LabelAdminTitel��5      $��	HI THERE,��6'   $
��Left=�6;   $��
77,153,0,0q�6V   $	��Top=�6j   ${�False��6�   $��Unispace)�6�   $��54(�6�   ���	#FFF4F4F4$	��329��6�   $y�Hidden=�6�   ��6�   Z� 6�   �� 5   
   -   5        $��TextBoxAdmin��5       �� TextWrapping$
 Wrap=�6&   $��Trebuchet MS)�6:    � 
InputScope$ Number��6T   $
��Left=�6h   $	��Top=�6�   $	��383��6�   $��
87,224,0,0q�6�   $��16(�6�   $y�Hidden=�6�    % !MaterialDesignFloatingHintTextBox#��[ 6  ���	#DDFFFFFF �� 
CaretBrush
 ��������	#89FFFFFF)  #MaterialDesignThemes.Wpf.HintAssist   
Foreground
 �����.  (MaterialDesignThemes.Wpf.TextFieldAssist	   UnderlineBrush
	 �����
 �� SelectionBrush
 6i  Z� .��6     Hint 5      �� 5       �� Orientation$ 
Horizontal=�6   $��-2 0 0 0q�66   .+�5        $ AccountCogOutline=�6.   �� 5      $�� Admin Login��6    5      .��5   .   5       �� 5!   
   -   5        $��ButtonAdminLogin��5!      $��LOGIN��6)   $��35��69   $	��350��6E   $��Unispace)�6Q   $
��Left=�6g   $	��Top=�6�   $��16(�6�   $��
77,285,0,0q�6�    �� 	IsDefault$
 True��6�   $y�Hidden=�6�   $��Normal"�6     MaterialDesignRaisedButton#��[ 6  ${�false��6H  ���	#FF323232
�������+  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 10��6�  ��6�  Z� 6�  �� 5#   
   -   5        $��ButtonKlant��5#      $
��Left=�6$   $��438,285,0,0q�6?   $	��Top=�6T   $��35��6l   $��35��6w   $y�Visible=�6�   $��20(�6�   #��[ 6�   ���	#FF323232
���������6  Z� .��5$        $ HammerScrewdriver=�6&   5%      �� 5'   
   -   5        $��LabelKlantTitel��5'      $��
HEY BUDDY,��6'   $
��Left=�6<   $��
77,108,0,0q�6W   $	��Top=�6k   ${�False��6�   $��Unispace)�6�   $��65(�6�   
�������$	��396��6�   $y�Visible=�6�   ��6�   Z� 6�   �� 5(   
   -	   5        $��TextBoxKlantnummer��5(      $
 Wrap=�6,   $��Trebuchet MS)�6V   $ Number��6p   $
��Left=�6�   $	��Top=�6�   $	��383��6�   $��
87,185,0,0q�6�   $��16(�6�   $y�Visible=�6  #��[ 6'  ���	#DDFFFFFF
 ��������	#89FFFFFF
 �����
	 �������6@   Z� 
 6�  Z� .��6+   5)      �� 5*      $ 
Horizontal=�6   $��-2 0 0 0q�66   .+�5+        $ AccountOutline=�6.   �� 5,      $�� Klantnummer��6    5-      .��5.   .   5/      �� 50   
   -
   5        $��TextBoxEmailadres��50      $
 Wrap=�6+   $	��383��6?   $��Trebuchet MS)�6a   $��
87,228,0,0q�6{   $	��Top=�6�   $
��Left=�6�   $��16(�6�   $y�Visible=�6�   #��[ 6  ���	#DDFFFFFF
 ��������	#89FFFFFF
 �����
	 �������6K   Z� 
 6v  Z� .��6   51      �� 52      $ 
Horizontal=�6   $��-2 0 0 0q�66   .+�53        $ At=�6.   �� 54      $��
 Mailadres��6    55      .��56   .   57      �� 58   
   -   5        $��ButtonKlantLogin��58      $��35��6)   $	��350��65   $��Unispace)�6A   $
��Left=�6W   $	��Top=�6r   $��16(�6�   $��
77,285,0,0q�6�   $
 True��6�   $y�Visible=�6�   $��LOGIN��6�   $��Normal"�6  #��[ 6  ${�false��6I  ���	#FF323232
�������$ 10��6�  ��6�  Z� 6�  59      5:      