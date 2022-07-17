import 'package:discordbotadminui/Helpers/ColorHelper.dart';
import 'package:flutter/material.dart';
import 'package:sizer/sizer.dart';

class ConfirmEmailPage extends StatelessWidget {
  const ConfirmEmailPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Center(
        child: Text(
      "You must confirm email before using this site",
      style: TextStyle(fontSize: 14.sp, color: ColorHelper.defaultTextColor),
      textAlign: TextAlign.center,
    ));
  }
}
