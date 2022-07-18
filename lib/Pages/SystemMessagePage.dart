import 'package:discordbotadminui/Helpers/ColorHelper.dart';
import 'package:flutter/material.dart';
import 'package:sizer/sizer.dart';

class SystemMessagePage extends StatelessWidget {
  static const String route = '/systemmessage';
  final String message;
  const SystemMessagePage({
    Key? key,
    required this.message,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Material(
      child: Center(
          child: Text(
        message,
        style: TextStyle(fontSize: 14.sp, color: ColorHelper.defaultTextColor),
        textAlign: TextAlign.center,
      )),
    );
  }
}
