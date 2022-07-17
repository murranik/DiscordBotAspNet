import 'package:flutter/material.dart';

class _DataTableCellColors {
  final defaultBoxShadowColor = const Color.fromARGB(255, 218, 217, 217);
  final defaultBorderColor = const Color.fromARGB(255, 200, 200, 200);
  final defaultInputTextColor = Colors.black;
  final defaultEditColor = const Color.fromARGB(141, 9, 231, 9);
}

class _DropdownButtonColors {
  final defaultBarrierColor = const Color.fromARGB(130, 232, 226, 220);
  final defaultIconEnableColor = Colors.black;
  final defaultIconDisabledColor = Colors.grey;
}

class _ServerStatusColors {
  final defaultShadowColor = Colors.grey.withOpacity(0.5);
  final defaultBackgroundColor = Colors.white60;
  final onlineColor = Colors.green;
  final offlineColor = const Color.fromARGB(255, 203, 42, 30);
}

class ColorHelper {
  static final dataTableCellColors = _DataTableCellColors();
  static final dropdownButtonColors = _DropdownButtonColors();
  static final serverStatusColors = _ServerStatusColors();
  static const Color defaultTextColor = Colors.black;
  static const Color defaultNavMenuTextColor = Colors.white;
  static const Color defaultNavMenuBackgroundColor = Color(0xff333333);
  static const Color activeColor = Colors.green;
  static const Color cancelColor = Color.fromARGB(255, 203, 42, 30);
}
