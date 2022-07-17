import 'package:discordbotadminui/Helpers/ColorHelper.dart';
import 'package:flutter/material.dart';
import 'package:sizer/sizer.dart';

class NavMenuButton extends StatelessWidget {
  final String text;
  final Color? choosedColor;
  final Function opClick;
  const NavMenuButton(
      {Key? key, required this.text, required this.opClick, this.choosedColor})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
        height: double.infinity,
        margin: EdgeInsets.symmetric(horizontal: 0.2.w),
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(2),
          color: choosedColor ?? const Color(0xff333333),
        ),
        child: TextButton(
          style: ButtonStyle(
            overlayColor: MaterialStateProperty.all(ColorHelper.activeColor),
          ),
          onPressed: () => opClick(),
          child: Text(
            text,
            style: TextStyle(
                fontSize: 8.sp, color: ColorHelper.defaultNavMenuTextColor),
          ),
        ));
  }
}
