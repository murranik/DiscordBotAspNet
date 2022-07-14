import 'package:discordbotadminui/Interfaces/IDataTableCell.dart';
import 'package:flutter/material.dart';
import 'package:sizer/sizer.dart';

class DataTableToolsCell extends StatefulWidget implements IDataTableCell {
  @override
  Color? backgroundColor;
  @override
  int flex;
  @override
  String name;

  final Function callBack;
  final Function save;
  final bool edit;

  DataTableToolsCell(
      {Key? key,
      required this.name,
      this.backgroundColor,
      this.flex = 3,
      required this.callBack,
      required this.edit,
      required this.save})
      : super(key: key);

  @override
  State<DataTableToolsCell> createState() => _DataTableToolsCellState();
}

class _DataTableToolsCellState extends State<DataTableToolsCell> {
  @override
  Widget build(BuildContext context) {
    return Expanded(
      flex: widget.flex,
      child: Container(
          //margin: EdgeInsets.all(1.sp),
          alignment: Alignment.centerLeft,
          decoration: BoxDecoration(
              boxShadow: const [
                BoxShadow(color: Color.fromARGB(255, 218, 217, 217))
              ],
              color: widget.edit
                  ? const Color.fromARGB(141, 9, 231, 9)
                  : const Color.fromARGB(255, 218, 217, 217),
              border: Border.fromBorderSide(BorderSide(
                color: const Color.fromARGB(255, 200, 200, 200),
                width: 0.5.sp,
              ))),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              SizedBox(
                height: 7.sp,
                child: IconButton(
                    mouseCursor: SystemMouseCursors.click,
                    padding: EdgeInsets.zero,
                    iconSize: 5.sp,
                    splashRadius: 3.sp,
                    onPressed: () {
                      widget.callBack(!widget.edit);
                      if (widget.edit) {
                        widget.save();
                      }
                    },
                    icon: Icon(
                      widget.edit ? Icons.cancel : Icons.edit,
                      color: widget.edit
                          ? const Color.fromARGB(255, 203, 42, 30)
                          : Colors.green,
                    )),
              ),
              if (widget.edit)
                SizedBox(
                  height: 7.sp,
                  child: IconButton(
                      mouseCursor: SystemMouseCursors.click,
                      padding: EdgeInsets.zero,
                      iconSize: 5.sp,
                      splashRadius: 3.sp,
                      onPressed: () {
                        widget.callBack(!widget.edit);
                      },
                      icon: const Icon(
                        Icons.check_circle_outline,
                        color: Colors.green,
                      )),
                )
            ],
          )),
    );
  }
}
