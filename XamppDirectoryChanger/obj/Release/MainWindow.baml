   M S B A M L   `   `   `  ���� P  LXamppDirectoryChanger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:XamppDirectoryChangerXamppDirectoryChanger  &     XamppDirectoryChanger.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5local#clr-namespace:XamppDirectoryChanger       � Title$  Xampp Directory Changer��$��537.5��$��349.5�� � Icon$% icons/Xampp Website Changer.png��.��� .+��� .@��� $��Website Changer��.��� ���	#FFE5E5E5.+��� .���� -   $��main_website_list��$��Stretch=�$��0,0,0,0q��� $��Add Directory��.��� ���	#FFE5E5E5.+��� $��Selected Website��$��105,49,579,313q�� -   $��current_data_grid��$��
10,10,10,0q�
�������$	��203��$	��Top=� � ColumnDefinitions ��  � RowDefinitions �� $��0* ��� .+���  �� TextAlignment$ Center=�$��10,10,10,166q�$
��Wrap=�$��Add Website Folder��$��24(�$��1��$
��Bold"��� ���	#FF3C3C3C$��1q�$	��Top=�$	��118��$��
10,53,10,0q����	#FF080808$��1��._�� ���	#FFECECEC$��0,0,0,0q�.+��� -    ��Name$ 
name_title��$ Center=�$
��Wrap=�$
��Name��$	��Top=�$��0,3,0,0q�$��14(�$
��Bold"��� -    ��Name$
 name�� �� TextAlignment$ Center=� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$��0,23,0,0q�
��������� -   $ 	url_title��$ Center=�$
��Wrap=�$��File URL��$	��Top=�$��0,44,0,0q�$��14(�$
��Bold"��� -   $	 url��$ Center=�$
 Wrap=�$�� ��$	��Top=�$��0,63,0,0q�
��������� -   	 ��Name$	 add_website_folder��$��Add To List��$��10,102,10,-16q��� 
���   �$��1q�$��10,218,10,10q�._�� 
�������.+��� $ Center=�$��8,0,10,0q�$
��Wrap=�$��Remove Website Folder��$��24(�$
��Bold"�$��27��$	��Top=��� $��10,42,10,10q�
�������.���� -   
 ��Name$
 remove_websites_list�� �� Orientation$ Vertical=�$��Stretch=�$��0,0,0,0q��� $��Options��.��� ���	#FFE5E5E5 �� $��3* ��� $��4* �.+��� $ Center=�$��	0,0,0,437q�$
��Wrap=�$��Bottom=�$ ��Select the httpd.conf file��$��2���� -	   $��stored_path��$��Center=�$��Center=�$��Center=�
�������$��10,47,10,404q�$��28��$��2���� -
   $��Select_File��$��Select file��$��	10,80,0,0q�$	��Top=�$��42��$
��Left=�$	��139��$��2���� -   $��
Reset_Path��$��Reset to default��$��Right=�$��	0,80,10,0q�$	��Top=�$	��139��$��42��$��1���� $ Center=�$��	0,0,0,319q�$
��Wrap=�$��Bottom=�$%��Select the xampp base file path��$��2���� -    ��Name$ 	base_path��$��Center=�$��Center=�$��Center=�
�������$��10,165,10,286q�$��28��$��2���� -   $	 Select_base_path��$��Select file��$��
10,198,0,0q�$	��Top=�$��42��$
��Left=�$	��139��$��2���� -   $	 Reset_base_Path��$��Reset to default��$��Right=�$��
0,198,10,0q�$	��Top=�$	��139��$��42��$��1��