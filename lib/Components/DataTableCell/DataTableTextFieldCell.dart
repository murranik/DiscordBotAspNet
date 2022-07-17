import 'package:discordbotadminui/Helpers/ColorHelper.dart';
import 'package:discordbotadminui/Interfaces/DataTableCellInterface.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter/widgets.dart';
import 'package:sizer/sizer.dart';

class DataTableTextFieldCell extends StatelessWidget
    implements DataTableCellInterface {
  @override
  Color? backgroundColor;

  @override
  int flex;

  @override
  String name;

  DataTableTextFieldCell(
      {Key? key, this.backgroundColor, this.flex = 3, required this.name})
      : super(key: key);
  final _editController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    _editController.text = name;
    return Expanded(
      flex: flex,
      child: Container(
          //margin: EdgeInsets.all(1.sp),
          height: 8.sp,
          alignment: Alignment.centerLeft,
          decoration: BoxDecoration(
              boxShadow: [
                BoxShadow(
                    color:
                        ColorHelper.dataTableCellColors.defaultBoxShadowColor)
              ],
              color: backgroundColor ?? Colors.transparent,
              border: Border.fromBorderSide(BorderSide(
                color: ColorHelper.dataTableCellColors.defaultBorderColor,
                width: 0.5.sp,
              ))),
          child: Row(
            children: [
              Expanded(
                  child: TextField(
                controller: _editController,
                inputFormatters: [FilteringTextInputFormatter.digitsOnly],
                style: TextStyle(
                    fontSize: 6.sp,
                    color:
                        ColorHelper.dataTableCellColors.defaultInputTextColor),
              ))
            ],
          )),
    );
  }
}
