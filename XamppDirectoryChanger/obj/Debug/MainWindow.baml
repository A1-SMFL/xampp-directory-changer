   M S B A M L   `   `   `  ����P  LXamppDirectoryChanger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:XamppDirectoryChangerXamppDirectoryChanger  5   	   &     XamppDirectoryChanger.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:XamppDirectoryChanger     5   	     � Title$  Xampp Directory Changer��5   	   $��537.5��6)   $��349.5��68    � Icon$% icons/Xampp Website Changer.png��6F   .��5	      � .+�5
   
   �� .@�5      �� $��Website Changer��6   .��5      � ���	#FFE5E5E5.+�5      �� .��5      �� -   5        $��main_website_list��5   %   $��Stretch=�6>   $��0,0,0,0q�6\   5      5      5      �� 5      $��Add Directory��6   .��5      � ���	#FFE5E5E5.+�5      �� $��Selected Website��6   $��105,49,579,313q�67   � 5      -   5        $��current_data_grid��5      $��
10,10,10,0q�64   
�������$	��203��6[   $	��Top=�6h    � ColumnDefinitions 5      �� 5       � RowDefinitions 5      �� 5      $��0* �6,   �� 5      .+�5       ��  �� TextAlignment$ Center=�6$   $��10,10,10,166q�6;   $
��Wrap=�6Q   $��Add Website Folder��6e   $��24(�6   $��1��6�   $
��Bold"�6�   �� 5!      ���	#FF3C3C3C$��1q�69   $	��Top=�6N   $	��118��6f   $��
10,53,10,0q�6s   ���	#FF080808$��1��6�   ._�5"      � ���	#FFECECEC$��0,0,0,0q�6:   .+�5#   "   �� -   5         ��Name$ 
name_title��5#   -   $ Center=�6A   $
��Wrap=�6X   $
��Name��6l   $	��Top=�6x   $��0,3,0,0q�6�   $��14(�6�   $
��Bold"�6�   �� 5$   "   -   5         ��Name$
 name��5$   *    �� TextAlignment$ Center=�68    �� TextWrapping$
 Wrap=�6O   $�� ��6c   $	��Top=�6k   $��0,23,0,0q�6�   
�������6�   �� 5%   "   -   5        $ 	url_title��5%   -   $ Center=�6@   $
��Wrap=�6W   $��File URL��6k   $	��Top=�6{   $��0,44,0,0q�6�   $��14(�6�   $
��Bold"�6�   �� 5&   "   -   5        $	 url��5&   *   $ Center=�67   $
 Wrap=�6N   $�� ��6b   $	��Top=�6j   $��0,63,0,0q�6�   
�������6�   �� 5'   "   -   5        	 ��Name$	 add_website_folder��5'   )   $��Add To List��6E   $��10,102,10,-16q�6[   6r   5(      5)      5+      �� 5/      
���   �$��1q�61   $��10,218,10,10q�6E   ._�50      � 
�������.+�51      �� $ Center=�6(   $��8,0,10,0q�6?   $
��Wrap=�6Q   $��Remove Website Folder��6e   $��24(�6�   $
��Bold"�6�   $��27��6�   $	��Top=�6�   �� 52      $��10,42,10,10q�6,   
�������.��53   "   �� -   5        
 ��Name$
 remove_websites_list��53   -    �� Orientation$ Vertical=�6K   $��Stretch=�6b   $��0,0,0,0q�6�   55   #   57      58      59      5<      5=      �� 5>      $��Options��6   .��5?      � ���	#FFE5E5E5 5@      �� 5A      $��3* �6+   �� 5B      $��4* �6+   .+�5D      �� $ Center=�6    $��	0,0,0,437q�67   $
��Wrap=�6J   $��Bottom=�6^   $ ��Select the httpd.conf file��6y   $��2��6�   �� 5E      -	   5        $��stored_path��5E   b   $��Center=�6   $��Center=�6@   $��Center=�6w   
�������$��10,47,10,404q�6�   $��28��6�   $��2��6�   �� 5F      -
   5        $��Select_File��5F      $��Select file��60   $��	10,80,0,0q�6F   $	��Top=�6Y   $��42��6q   $
��Left=�6�   $	��139��6�   $��2��6�   �� 5G      -   5        $��
Reset_Path��5G      $��Reset to default��6/   $��Right=�6J   $��	0,80,10,0q�6f   $	��Top=�6y   $	��139��6�   $��42��6�   $��1��6�   �� 5I      $ Center=�6    $��	0,0,0,319q�67   $
��Wrap=�6J   $��Bottom=�6^   $%��Select the xampp base file path��6y   $��2��6�   �� 5J      -   5         ��Name$ 	base_path��5J   b   $��Center=�6   $��Center=�6@   $��Center=�6w   
�������$��10,165,10,286q�6�   $��28��6�   $��2��6�   �� 5K      -   5        $	 Select_base_path��5K      $��Select file��67   $��
10,198,0,0q�6M   $	��Top=�6a   $��42��6y   $
��Left=�6�   $	��139��6�   $��2��6�   �� 5L      -   5        $	 Reset_base_Path��5L      $��Reset to default��66   $��Right=�6Q   $��
0,198,10,0q�6m   $	��Top=�6�   $	��139��6�   $��42��6�   $��1��6�   5N      5O      5R      5T      5U      