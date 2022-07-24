import 'package:discordbotadminui/Helpers/ColorHelper.dart';
import 'package:flutter/material.dart';
import 'package:sizer/sizer.dart';

class NavMenuButton extends StatelessWidget {
  final String text;
  final Color? choosedColor;
  final Function onClick;
  final bool locked;
  const NavMenuButton(
      {Key? key,
      required this.text,
      required this.onClick,
      this.choosedColor,
      this.locked = false})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Tooltip(
      message: locked ? "To obtain this page please login" : '',
      child: Container(
          height: double.infinity,
          margin: EdgeInsets.symmetric(horizontal: 0.2.w),
          decoration: BoxDecoration(
            borderRadius: BorderRadius.circular(2),
            color: choosedColor ?? const Color(0xff333333),
          ),
          child: Row(children: [
            if (locked)
              Icon(
                Icons.lock,
                size: 4.sp,
                color: ColorHelper.defaultNavMenuTextColor,
              ),
            Container(
              color: ColorHelper.activeColor,
              child: InkWell(
                onTap: () => onClick(),
                child: Text(
                  text,
                  style: TextStyle(
                      fontSize: 6.sp,
                      color: ColorHelper.defaultNavMenuTextColor),
                ),
              ),
            )
          ])),
    );
  }
}
