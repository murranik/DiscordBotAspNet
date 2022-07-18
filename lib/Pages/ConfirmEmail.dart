import 'package:discordbotadminui/Components/Timer.dart';
import 'package:discordbotadminui/Helpers/ColorHelper.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:sizer/sizer.dart';

class ConfirmEmailPage extends StatelessWidget {
  static const String route = "/confirmemail";
  const ConfirmEmailPage({
    Key? key,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Material(
        child: Column(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        Center(
          child: Text(
            "Email succsessfully confirmed",
            style:
                TextStyle(fontSize: 14.sp, color: ColorHelper.defaultTextColor),
            textAlign: TextAlign.center,
          ),
        ),
        RefreshTimer(
          onEnd: () {
            GoRouter.of(context).go("/");
          },
          seconds: 5,
          textColor: ColorHelper.defaultTextColor,
          timePrefix: "Redirect after ",
        ),
      ],
    ));
  }
}
