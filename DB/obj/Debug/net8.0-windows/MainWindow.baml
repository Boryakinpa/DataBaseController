   M S B A M L   `   `   `  ����  DB    DB.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$%  Database Management Application��5   	   $	��600��61   $	��800��6>   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   o� .@�5      m� $
��File��6   .@�5      m� -   5        $��Connect��5      6,   m� 5      -   5        $
��Exit��5      6)   5      m� 5      -   5        $
��Help��5      6%   5      �� 5   
   -   5         ��Name$ MainTabControl��5   "   $��1��6   .@�5      �� $��Connection Info��6   .��5      �� .+�5      �� $��Connection Status:��6    $
��Bold"�6:   �� 5      -   5         ��Name$ ConnectionStatusText��5       $��Not Connected��6>   �� 5      $��Select Database:��6    $
��Bold"�68   �� 5      -   5         ��Name$ DatabaseComboBox��5      69   �� 5      -   5         ��Name$ DatabaseName_TextBox��5       �� TextWrapping$
 Wrap=�6<   $��BoryakinPA_LAB1��6P   $	��120��6g   $
��Left=�6s   5      5      �� 5      $��Tables��6   .��5       �� .+�5!      �� -   5        $��Load Tables��5!      63   �� 5"      -	   5         ��Name$ TablesListBox��5"      65   5#      5$      �� 5%      $��
Table Info��6   .��5&      �� .+�5'      �� $��Table Details:��6    $
��Bold"�66   �� 5(      -
   5        $ TableDetailsText��5(       �� 5)      $��Primary Key:��6    $
��Bold"�64   �� 5*      -   5        $ PrimaryKeyListBox��5*      �� 5+      $��Indexed Fields:��6    $
��Bold"�67   �� 5,      -   5        $ IndexedFieldsListBox��5,      �� 5-      -   5        $��Show Table Data��5-      67   5.      5/      �� 50      $��
Table Data��6   .��51      &   System.Windows.Controls.DataGrid  -   5          Name$ TableDataGrid��51      	   AutoGenerateColumns$
	 True��62   52      53      54      55      