import 'package:discordbotadminui/Interfaces/IDataTableCell.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter/widgets.dart';
import 'package:sizer/sizer.dart';

class DataTableTextFieldCell extends StatelessWidget implements IDataTableCell {
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
              boxShadow: const [
                BoxShadow(color: Color.fromARGB(255, 218, 217, 217))
              ],
              color: backgroundColor ?? Colors.transparent,
              border: Border.fromBorderSide(BorderSide(
                color: const Color.fromARGB(255, 200, 200, 200),
                width: 0.5.sp,
              ))),
          child: Row(
            children: [
              Expanded(
                  child: TextField(
                controller: _editController,
                inputFormatters: [FilteringTextInputFormatter.digitsOnly],
                style: TextStyle(fontSize: 6.sp, color: Colors.black),
              ))
            ],
          )),
    );
  }
}
