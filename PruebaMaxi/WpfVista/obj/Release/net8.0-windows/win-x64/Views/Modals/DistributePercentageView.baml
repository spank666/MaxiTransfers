   M S B A M L   `   `   `  ���� C  ?WpfVista, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:WpfVista.Views.ModalsWpfVista.Views.Modals  9!clr-namespace:WpfVista.ViewModelsWpfVista.ViewModels  4    .WpfVista.Views.Modals.DistributePercentageView   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5local#clr-namespace:WpfVista.Views.Modals     0vm!clr-namespace:WpfVista.ViewModels     $	��460��$��20q�.���   � RowDefinitions  �� $
��auto ��� $
��auto �.+��� $��0�����#FFF580 �� CornerRadius$ 	10,10,0,0�� �� Padding$ 20q�._���  �� Orientation$ 
Horizontal=�$	��Top=�.+��� $2��,/WpfVista;component/Assets/Images/sender.png��$��40��$
��Left=�$��Center=��� $��Distribuir Porcentaje��$��18(�$
��Bold"�$��Center=�$
��Left=�$��10,0,0,0q��� -   $��bordesin��$��1��
�������$ 	0,0,10,10��$ 20q�._��� .+��� ���� *items+���� .R�� $��0,0,0,5q� � ColumnDefinitions �� $	��120 ��� $��* �.+��� $��0������ *	Nombres+�� $��1�� � Minimum$ 0Q� � Maximum$	 100Q� �� IsSnapToTickEnabled$
 True�� �� TickPlacement$ BottomRight=�	 �� AutoToolTipPlacement$	 TopLeft=�
 �� AutoToolTipPrecision$
 1�� � Value �� *
Porcentaje+�� $ 
Horizontal=�$��0,20,0,0q�$��Right=�.+��� $��Cancelar��   	buttonRed#��[  ���� *CancelCommand+�� $��Guardar��$��10,0,0,0q�  buttonBlack#��[ ���� *DistributePercentageCommand+