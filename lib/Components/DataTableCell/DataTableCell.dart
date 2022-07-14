import 'package:discordbotadminui/Interfaces/IDataTableCell.dart';
import 'package:flutter/material.dart';
import 'package:sizer/sizer.dart';

class DataTableCell extends StatelessWidget implements IDataTableCell {
  @override
  Color? backgroundColor;
  @override
  int flex;
  @override
  String name;

  DataTableCell(
      {Key? key, required this.name, this.backgroundColor, this.flex = 3})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Expanded(
      flex: flex,
      child: Container(
        //margin: EdgeInsets.all(1.sp),
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
        child: Text(
          name,
          style: TextStyle(fontSize: 6.sp),
        ),
      ),
    );
  }
}
