   M S B A M L   `   `   `  ���� G  CShortestPath, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ShortestPathShortestPath      ShortestPath.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:ShortestPath       � Icon$  Icons\chart.ico�� � Title$! ShortestPath - M.Kozikowski��$	��450��$	��600��.���  � RowDefinitions �� $
��Auto ��� �� $
��Auto �.+�o� -   $��MainMenu�� o� 
IsMainMenu . $
��16px(�$
��auto������  �� Color# � ��.@�m� $
��Plik��.@�m� -   $��Wczytaj�� m� Icon �� $��Icons\window-open.png��m� -   $��Zapisz�� �� $��Icons\content-save.png��m� -   $��Zakończ�� �� $��Icons\exit-to-app.png��m� $��	Ścieżka��.@�m� -   $��Dijkstry��m� -   $	��BFS��m� $��Pomoc��� $��1�� � ColumnDefinitions �� $��3* ��� $
��Auto ��� $��5* �.+��� $��Tablica połączeń��$	��5pxq����� # � ��.��&   System.Windows.Controls.DataGrid  -   $��InputDataGrid��$��0��   RowDetailsVisibilityMode$ Visible=�   CanUserResizeColumns . 	   HeadersVisibility$		 All=�
   RowHeaderWidth$
 20Q�   SelectionMode$ Single=���. $	��5pxq�   SelectionUnit$
 Cell=����� # � ������ # � ��   RowHeaderTemplate �� .R��� ���� *Path.+ �� RelativeSource ��  �� AncestorType$ DataGridRowL�   RowHeaderStyle �� .���  �� Property � �� �� Value
 �����   ColumnHeaderStyle �� .���  � ��
 ������� $��1��$��2��$��Stretch=�$	��5pxq����� # � ������ # � ���� -   $��StringTextBox��$��2��$	��5pxq�.������ # � ��.���� $��2��$	��5pxq�$��Dane wejściowe������ # � ��.��� $	��5pxq� �� $
��Auto ��� �� $
��Auto ��� �� $
��Auto ��� �� $
��Auto ��� .+��� $��0��$��Ilość miast: ��$
��Bold"��� -	   $��CitiesNumberLabel��$��1��$��0��
���  ���� $��2��$��Ilość połączeń: ��$
��Bold"��� -
   $��PathsNumberLabel��$��3��$��0��
���  ���� $��4��$��Początek: ��$
��Bold"��� -   $��SourceNumberLabel��$��5��$��0��
���  ���� $��6��$��Koniec: ��$
��Bold"��� -   $��DestenationNumberLabel��$��7��$��0��
���  ��