   M S B A M L   `   `   `  ����H  DCalculatorApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:CalculatorApp.ViewModelCalculatorApp.ViewModel  5   	   #    CalculatorApp.View.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   7local%clr-namespace:CalculatorApp.ViewModel     5   	     � Title$  CalculatorApp��5   	   $	��600��5   	   $	��400��5   	   
���   � � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	    � Icon$ ../Images/Icon.png��5   	    � DataContext 5      $   CalculatorApp.ViewModel.MainVM  5   
    J� InputBindings 5      �� 5   
   
 �� Key$ D0��6    �� CommandParameter$ 0��6J    �� Command 6   �� *Calculator.DigitCommand+6J   �� 5   
   $ NumPad0��6   $ 0��6O    6#   �� *Calculator.DigitCommand+6O   �� 5   
   $ D1��6   $ 1��6J    6   �� *Calculator.DigitCommand+6J   �� 5   
   $ NumPad1��6   $ 1��6O    6#   �� *Calculator.DigitCommand+6O   �� 5   
   $ D2��6   $ 2��6J    6   �� *Calculator.DigitCommand+6J   �� 5   
   $ NumPad2��6   $ 2��6O    6#   �� *Calculator.DigitCommand+6O   �� 5   
   $ D3��6   $ 3��6J    6   �� *Calculator.DigitCommand+6J   �� 5   
   $ NumPad3��6   $ 3��6O    6#   �� *Calculator.DigitCommand+6O   �� 5   
   $ D4��6   $ 4��6J    6   �� *Calculator.DigitCommand+6J   �� 5   
   $ NumPad4��6   $ 4��6O    6#   �� *Calculator.DigitCommand+6O   �� 5   
   $ D5��6   $ 5��6J    6   �� *Calculator.DigitCommand+6J   �� 5   
   $ NumPad5��6   $ 5��6O    6#   �� *Calculator.DigitCommand+6O   �� 5   
   $ D6��6   $ 6��6J    6   �� *Calculator.DigitCommand+6J   �� 5   
   $ NumPad6��6   $ 6��6O    6#   �� *Calculator.DigitCommand+6O   �� 5    
   $ D7��6   $ 7��6J    6   �� *Calculator.DigitCommand+6J   �� 5!   
   $ NumPad7��6   $ 7��6O    6#   �� *Calculator.DigitCommand+6O   �� 5"   
   $ D8��6   $ 8��6J    6   �� *Calculator.DigitCommand+6J   �� 5#   
   $ NumPad8��6   $ 8��6O    6#   �� *Calculator.DigitCommand+6O   �� 5$   
   $ D9��6   $ 9��6J    6   �� *Calculator.DigitCommand+6J   �� 5%   
   $ NumPad9��6   $ 9��6O    6#   �� *Calculator.DigitCommand+6O   �� 5&   
   $ 	OemPeriod��6    6%   �� *Calculator.DigitCommand+ 6Q   �� *Calculator.decimalSeparator+�� 5'   
   $ Decimal��6    6#   �� *Calculator.DigitCommand+ 6O   �� *Calculator.decimalSeparator+�� 5)   
   $	 Add��6   $ +��6N    6   �� *Calculator.OperatorCommand+6N   �� 5*   
   $ Subtract��6   $ -��6S    6$   �� *Calculator.OperatorCommand+6S   �� 5+   
   $ OemPlus��6   	 �� 	Modifiers$	 Shiftf�6#   $ +��6d    65   �� *Calculator.OperatorCommand+6d   �� 5,   
   $ OemMinus��6   $ -��6S    6$   �� *Calculator.OperatorCommand+6S   �� 5-   
   $ Multiply��6   $ ×��6S    6$   �� *Calculator.OperatorCommand+6S   �� 5.   
   $ D8��6   $	 Shiftf�6   $ ×��6_    60   �� *Calculator.OperatorCommand+6_   �� 5/   
   $ Divide��6   $ /��6Q    6"   �� *Calculator.OperatorCommand+6Q   �� 50   
   $ OemQuestion��6   $ /��6V    6'   �� *Calculator.OperatorCommand+6V   �� 52   
   $ Return��6    6"   �� *Calculator.EqualCommand+�� 53   
   $ Enter��6    6!   �� *Calculator.EqualCommand+�� 54   
   $ Escape��6   $ C��6N    6"   �� *Calculator.ClearCommand+6N   �� 55   
   $ 	Backspace��6    6%   �� *Calculator.BackspaceCommand+�� 56   
   $ OemPlus��6    6#   �� *Calculator.EqualCommand+�� 58   
   $ R��6   $	 1/x��6L    6   �� *Calculator.OperatorCommand+6L   �� 59   
   $ D5��6   $	 Shiftf�6   $ %��6_    60   �� *Calculator.OperatorCommand+6_   �� 5:   
   $ D2��6   $	 Shiftf�6   $ √(x)��6_    60   �� *Calculator.OperatorCommand+6_   �� 5;   
   $ Q��6   $	 x²��6L    6   �� *Calculator.OperatorCommand+6L   �� 5=   
   $ L��6   $
	 Ctrlf�6    6.   �� *CompositeMemoryClearCommand+�� 5>   
   $ R��6   $
	 Ctrlf�6    6.   �� * Calculator.MemoryRecallCommand+ 6a   �� *Memory.RecalledMemory+�� 5?   
   $ P��6   $
	 Ctrlf�6    6.   �� *CompositeMemoryAddCommand+ 6\   �� *Calculator.Display+�� 5@   
   $ Q��6   $
	 Ctrlf�6    6.   �� * CompositeMemorySubtractCommand+ 6a   �� *Calculator.Display+�� 5A   
   $ M��6   $
	 Ctrlf�6    6.   �� *CompositeMemoryStoreCommand+ 6^   �� *Calculator.Display+�� 5C   
   $ C��6   $
	 Ctrlf�6   $ C��6c    6.   �� *" Calculator.FileManagementCommand+6c   �� 5D   
   $ X��6   $
	 Ctrlf�6   $ X��6c    6.   �� *" Calculator.FileManagementCommand+6c   �� 5E   
   $ V��6   $
	 Ctrlf�6   $ V��6c    6.   �� *" Calculator.FileManagementCommand+6c   .��5H      ]� $	��554��6   $��Center=�6   .+�5J   
   o� $	��Top=�6   
���   �
�������.@�5K      m� $��_File��6   
���   �$|�False��69   .@�5L      m� $��_Copy��6   
 m� CommandParameter$
 C��6_   
���   �
�������$|�False��6�    m� Command 6*   �� *" Calculator.FileManagementCommand+6�   m� 5M      $
��_Cut��6   $
 X��6^   
���   �
�������$|�False��6�    6)   �� *" Calculator.FileManagementCommand+6�   m� 5N      $��_Paste��6   $
 V��6`   
���   �
�������$|�False��6�    6+   �� *" Calculator.FileManagementCommand+6�   m� 5O      $��Digit Grouping��6   
���   �
�������$|�False��5Q   A    5P      �� *'%Calculator.ToggleDigitGroupingCommand+5Q   A   5R      m� 5S      $��_Help��6   
���   �.@�5T      m� $��_About��6   
���   �
�������$|�False��6r    6+   �� *AboutCommand+6r   5U      m� 5V      $��_Programmer��6   
���   �$|�False��6?   .@�5W      m� $#��Standard Mode/Programmer Mode��6   
���   �$|�False��5Y       5X      �� *(&Calculator.ToggleProgrammerModeCommand+5Y      m� 5Z      $
��Base��6   
���   �.@�5[      m� $��Binary��6   
���   �$
 2��6p   $|�False��6�    6B   �� *Calculator.SetBaseCommand+6�   m� 5\      $��Octal��6   
���   �$
 8��6p   $|�False��6�    6B   �� *Calculator.SetBaseCommand+6�   m� 5]      $��Decimal��6   
���   �$
 10��6q   $|�False��6�    6C   �� *Calculator.SetBaseCommand+6�   m� 5^      $��Hexadecimal��6   
���   �$
 16��6u   $|�False��6�    6G   �� *Calculator.SetBaseCommand+6�   5_      5`      5a      �� 5d   
   $	��538��6   .+�5f      �� $��16(�5g      
�������$��Right=�5i      $
��10,5q�5j      ��5f      �� *Calculator.ModeText+5j      �� 5l      -   5         ��Name$ DisplayTextBlock��5l      $��40(�5n      $��60��5o      
���   �
������� �� TextAlignment$ Right=�5r      $��Center=�5s      $��
10,0,10,10q�5t      ��5m      �� *Calculator.Display+5t      A� 5v       A� Columns$ 6��6   $��0,0,0,10q�6&   
���   �.+�5w      �� $��MC��6   
���   �
������� � ToolTip$ Clear All Memory (Ctrl+L)��5z      $|�False��5{      ��5x      �� *Memory.MemoryClearAllCommand+{�5y      �� *Memory.HasMemory+5{      �� 5|      $��MR��6   
���   �
�������$ Memory Recall (Ctrl+R)��5      $|�False��5�      ��5}      �� * Calculator.MemoryRecallCommand+��5~      �� *Memory.RecalledMemory+{�5�      �� *Memory.HasMemory+5�      �� 5�      $��M+��6   
���   �
�������$ Memory Add (Ctrl+P)��5�      $|�False��5�      ��5�      �� *CompositeMemoryAddCommand+��5�      �� *Calculator.Display+5�      �� 5�      $��M-��6   
���   �
�������$ Memory Subtract (Ctrl+Q)��5�      $|�False��5�      ��5�      �� * CompositeMemorySubtractCommand+��5�      �� *Calculator.Display+5�      �� 5�      $��MS��6   
���   �
�������$ Memory Store (Ctrl+M)��5�      $|�False��5�      ��5�      �� *CompositeMemoryStoreCommand+��5�      �� *Calculator.Display+5�      �� 5�      $��Mv��6   
���   �
�������$ Memory (View)��5�      $|�False��5�      ��5�      �� *MemoryViewCommand+{�5�      �� *Memory.HasMemory+5�      5�      � 5�      
���   �$	��407��6&    � RowDefinitions 5�      �� 5�      $��* �6$   �� 5�      $��* �6$   �� 5�      $��* �6$   �� 5�      $��* �6$   �� 5�      $��* �6$   �� 5�      $��* �6$    � ColumnDefinitions 5�      �� 5�      $��* �6'   �� 5�      $��* �6'   �� 5�      $��* �6'   �� 5�      $��* �6'   .+�5�      �� $��%��6   $��0��6%   $��0��62   $��5q�6B   
�������$��%��5�   H   $|�False��5�      ��5�      �� *Calculator.OperatorCommand+{�5�      �� *Calculator.OperatorsEnabled+5�      �� 5�      $��CE��6   $��0��6&   $��1��63   $��5q�6C   
�������$��CE��5�   E   $|�False��5�      ��5�      �� *Calculator.ClearCommand+5�      �� 5�      $��C��6   $��0��6%   $��2��62   $��5q�6B   
�������$��C��5�   E   $|�False��5�      ��5�      �� *Calculator.ClearCommand+5�      �� 5�      $
��Back��6   $��0��6(   $��3��65   $��5q�6E   
�������$|�False��5�      ��5�      �� *Calculator.BackspaceCommand+5�      �� 5�      $	��1/x��6   $��1��6'   $��0��64   $��5q�6D   
�������$	��1/x��5�   H   $|�False��5�      ��5�      �� *Calculator.OperatorCommand+{�5�      �� *Calculator.OperatorsEnabled+5�      �� 5�      $	��x²��6   $��1��6&   $��1��63   $��5q�6C   
�������$	��x²��5�   H   $|�False��5�      ��5�      �� *Calculator.OperatorCommand+{�5�      �� *Calculator.OperatorsEnabled+5�      �� 5�      $��√(x)��6   $��1��6(   $��2��65   $��5q�6E   
�������$��√(x)��5�   H   $|�False��5�      ��5�      �� *Calculator.OperatorCommand+{�5�      �� *Calculator.OperatorsEnabled+5�      �� 5�      $��/��6   $��1��6%   $��3��62   $��5q�6B   
�������$��/��5�   H   $|�False��5�      ��5�      �� *Calculator.OperatorCommand+{�5�      �� *Calculator.OperatorsEnabled+5�      �� 5�      $��7��6   $��2��6%   $��0��62   $��5q�6B   $��7��5�   E   $|�False��5�      ��5�      �� *Calculator.DigitCommand+5�      �� 5�      $��8��6   $��2��6%   $��1��62   $��5q�6B   $��8��5�   E   $|�False��5�      ��5�      �� *Calculator.DigitCommand+5�      �� 5�      $��9��6   $��2��6%   $��2��62   $��5q�6B   $��9��5�   E   $|�False��5�      ��5�      �� *Calculator.DigitCommand+5�      �� 5�      $��×��6   $��2��6%   $��3��62   $��5q�6B   
�������$��×��5�   H   $|�False��5�      ��5�      �� *Calculator.OperatorCommand+{�5�      �� *Calculator.OperatorsEnabled+5�      �� 5�      $��4��6   $��3��6%   $��0��62   $��5q�6B   $��4��5�   E   $|�False��5�      ��5�      �� *Calculator.DigitCommand+5�      �� 5�      $��5��6   $��3��6%   $��1��62   $��5q�6B   $��5��5�   E   $|�False��5�      ��5�      �� *Calculator.DigitCommand+5�      �� 5�      $��6��6   $��3��6%   $��2��62   $��5q�6B   $��6��5�   E   $|�False��5�      ��5�      �� *Calculator.DigitCommand+5�      �� 5�      $��-��6   $��3��6%   $��3��62   $��5q�6B   
�������$��-��5�   H   $|�False��5�      ��5�      �� *Calculator.OperatorCommand+{�5�      �� *Calculator.OperatorsEnabled+5�      �� 5�      $��1��6   $��4��6%   $��0��62   $��5q�6B   $��1��5�   E   $|�False��5�      ��5�      �� *Calculator.DigitCommand+5�      �� 5�      $��2��6   $��4��6%   $��1��62   $��5q�6B   $��2��5�   E   $|�False��5�      ��5�      �� *Calculator.DigitCommand+5�      �� 5�      $��3��6   $��4��6%   $��2��62   $��5q�6B   $��3��5�   E   $|�False��5�      ��5�      �� *Calculator.DigitCommand+5�      �� 5�      $��+��6   $��4��6%   $��3��62   $��5q�6B   
�������$��+��5�   H   $|�False��5�      ��5�      �� *Calculator.OperatorCommand+{�5�      �� *Calculator.OperatorsEnabled+5�      �� 5�      $	��+/-��6   $��5��6'   $��0��64   $��5q�6D   $	��+/-��5�   H   $|�False��5�      ��5�      �� *Calculator.OperatorCommand+{�5�      �� *Calculator.OperatorsEnabled+5�      �� 5�      $��0��6   $��5��6%   $��1��62   $��5q�6B   $��0��5�   E   $|�False��5�      ��5�      �� *Calculator.DigitCommand+5�      �� 5�      $��.��6   $��5��6%   $��2��62   $��5q�6B   $|�False��5�      ��5�      �� *Calculator.DigitCommand+��6E   �� *Calculator.decimalSeparator+{�5�      �� *Calculator.OperatorsEnabled+5�      �� 5�      $��=��6   $��5��6%   $��3��62   $��5q�6B   
����� �$|�False��5�      ��5�      �� *Calculator.EqualCommand+5�      5      5     5     5     