   M S B A M L   `   `   `  ����  WpfPresentationLayer,    &WpfPresentationLayer.frmChangePassword   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Change Password��5   	   $	��200��6!   $	��350��6.   .��5      � ���#DDD � ColumnDefinitions 5   
   �� 5      �� 5       � RowDefinitions 5
   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   G� -   5         G�Name$ txtOldPassword��5      $��1��6.   $��3 3 35 3q�6;   $��1��6M   G� 5   
   -   5        $ txtNewPassword��5      $��2��6.   $��3 3 35 3q�6;   $��1��6M   G� 5   
   -   5        $ txtConfirmPassword��5      $��3��62   $��3 3 35 3q�6?   $��1��6Q   �� 5   
   $��1��6   $��0��6   $��Old Password: ��6-   $��Right=�6F   �� 5   
   $��2��6   $��0��6   $��New Password: ��6-   $��Right=�6F   �� 5   
   $��3��6   $��0��6   $��Confirm Password: ��6-   $��Right=�6J   �� 5   
   -   5         ��Name$ 	btnSubmit��5      $��4��6$   $��0��61   $��Submit��6A   $
��Auto��6R   $��50 5 5 0q�6_    �� 	IsDefault . 6�   �� 5   
   -   5        $ 	btnCancel��5      $��4��6$   $��1��61   $��Cancel��6A   $
��Auto��6R   $��5 5 50 0q�6_    �� IsCancel . 6�   5      5      