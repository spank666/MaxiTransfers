   M S B A M L   `   `   `  ���� C  ?WpfVista, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:WpfVista.Views.ModalsWpfVista.Views.Modals  /    )WpfVista.Views.Modals.EditBeneficiaryView   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5local#clr-namespace:WpfVista.Views.Modals     $	��460��$��20q�.���   � RowDefinitions  �� $
��auto ��� $
��auto �.+��� $��0�����#FFF580 �� CornerRadius$ 	10,10,0,0�� �� Padding$ 20q�._���  �� Orientation$ 
Horizontal=�$	��Top=�.+��� $2��,/WpfVista;component/Assets/Images/sender.png��$��40��$
��Left=�$��Center=��� $��Editar Beneficiario��$��18(�$
��Bold"�$��Center=�$
��Left=�$��10,0,0,0q��� -   $��bordesin��$��1��
�������$ 	0,0,10,10��$ 20q�._��� .+��� $��Nombres��$��0q��� $��13(� �� 
CaretBrush �#4C4C4C���� *beneficiary.Nombres+ �� ValidatesOnDataErrors$
 True���� $��	Apellidos��$��0q�$��0,10,0,0q��� $��13(� �#4C4C4C���� *beneficiary.Apellidos+$
 True���� $��Fecha de Nacimiento��$��0q�$��0,10,0,0q�(  "System.Windows.Controls.DatePicker     SelectedDate �� *beneficiary.Fecha+$
 True���� $
��CURP��$��0q�$��0,10,0,0q��� $��13(� �#4C4C4C���� *beneficiary.CURP+$
 True���� $	��SSN��$��0q�$��0,10,0,0q��� $��13(� �#4C4C4C���� *beneficiary.SSN+$
 True���� $��Telefono��$��0q�$��0,10,0,0q��� $��13(� �#4C4C4C���� *beneficiary.Telefono+$
 True���� $��Nacionalidad��$��0q�$��0,10,0,0q��� $��13(� �#4C4C4C���� *beneficiary.Nacionalidad+$
 True���� $ 
Horizontal=�$��0,20,0,0q�$��Right=�.+��� $��Cancelar��   	buttonRed#��[  ���� *CancelCommand+�� $��Guardar��$��10,0,0,0q�  buttonBlack#��[ ���� *SaveCommand+