   M S B A M L   `   `   `  ���� F  BMuzU Studio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MuzU_Studio.viewMuzU_Studio.view  3clr-namespace:MuzU_Studio.utilMuzU_Studio.util      MuzU_Studio.view.Toolbox   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:MuzU_Studio.view     /utilclr-namespace:MuzU_Studio.util       � DataContext  ��    Locator �� Source# [   �� Path$ ToolboxViewModel� � 	Resources 1   +MuzU_Studio.util.BeatToMusicalNameConverter  beatToMusicalNameConverter*  $MuzU_Studio.util.VisibilityConverter  visibilityConverter��%   &	       &	          .���� $��Disabled=�$
��Auto=�.����  �� Orientation$ Vertical=�.+��� $&�� Apply 🧲 for all visible notes��.���  � ColumnDefinitions �� $��* ��� $��* �.+��� -    ��Name$ SnapAllComboBox��$��0�� �� 
IsEditable$
 True�� �� SelectedIndex$ 0��$��0,0,5,0q�	 �� Text	 �� $ SnapAllInterval�
 �� Mode$
 TwoWay=� �� 	Converter# [  �� ValidationRules &   MuzU_Studio.util.MusicalNameRule  .@��� $
��none���� $	��bar���� $
��beat���� $��1/2 beat���� $��1/3 beat���� $��1/4 beat���� $��1/6 beat���� $
��step���� $��1/2 step���� $��1/3 step���� $��1/4 step���� $��1/6 step���� $��1/8 step���� $��1��$��SnapAll������ *SnapAllCommand+�� $��Melodize��.���  �� $��* ��� $��* �.+��� $��0��$��0,0,5,0q����� *MelodizeTypes+$
 OneWay=�	 �� *ChoosenMelodizeType+$
 TwoWay=��� $��1��$��Melodize������ *MelodizeCommand+�� $��
Change BPM��.���  �� $��* ��� $��* �.+��� $��0��$��0,0,5,0q����� *ChangeBPMParameter+$
 TwoWay=��� $��1��$��Change������ *ChangeBPMCommand+�� $��Lyrics��y��� *IsThereSequenceSelected+# [ .���  �� $��* ��� $��* � � RowDefinitions �� $
��Auto ��� $
��Auto �.+��� $��0��$��0��$��0,0,5,0q����� *AddRemoveLyricsBtn+$
 OneWay=����� *AddRemoveLyricsCommand+�� $��1��$��0��$��Divide By Words������ *DivideByWordsCommand+��  �� TextWrapping$
 Wrap=� �� AcceptsReturn$
 True��$��2��$��1������ *
LyricsText+$
 TwoWay=�y��� *LyricsEnabled+# [ 