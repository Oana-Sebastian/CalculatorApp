   M S B A M L   `   `   `  ����H  DCalculatorApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:CalculatorApp.ViewModelCalculatorApp.ViewModel  5   	   %    CalculatorApp.View.MemoryWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   7local%clr-namespace:CalculatorApp.ViewModel     5   	     � Title$  Memory Management��5   	   $	��400��6#   $	��300��60    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5   	    � DataContext 5	      $   CalculatorApp.ViewModel.MainVM  5
   
   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��16(�5      $��0,0,0,10q�5      $	��Top=�5      ��5      �� *Memory.MemoryStack+ �� SelectedItem 5      �� *Memory.SelectedMemory+ �� Mode$ TwoWay=�5      �� 5   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   .+�5      �� $��M+��6   $��5q�5       $
��10,5q�6    ��5   "   �� *!Memory.MemoryAddSelectedCommand+��5      �� *Calculator.Display+{�5      �� * Memory.SelectedMemory.HasValue+5        �� 5!      $
��M–��6   $��5q�5$      $
��10,5q�6    ��5!   "   �� *&$Memory.MemorySubtractSelectedCommand+��5"      �� *Calculator.Display+{�5#      �� * Memory.SelectedMemory.HasValue+5$       �� 5%      $��MC��6   $��5q�6�   $
��10,5q�6�   ��6&   �� *Memory.ClearSelectedCommand+{�6V   �� * Memory.SelectedMemory.HasValue+6�   5&      5'      5(      