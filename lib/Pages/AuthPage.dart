import 'package:discordbotadminui/Components/GuildsDropDownButton.dart';
import 'package:discordbotadminui/Components/AuthPageInputState.dart';
import 'package:discordbotadminui/Helpers/ColorHelper.dart';
import 'package:discordbotadminui/Models/Administrator.dart';
import 'package:discordbotadminui/Models/AuthPageData.dart';
import 'package:discordbotadminui/Services/DiscordBotApiService.dart';
import 'package:flutter/material.dart';
import 'package:sizer/sizer.dart';

class AuthPage extends StatefulWidget {
  static const String route = '/auth';

  final List<AuthPageData> data;
  final String title;
  final String footerButtonText;
  final String footerText;
  final String footerButtonRoute;

  const AuthPage(
      {Key? key,
      required this.data,
      required this.title,
      required this.footerButtonText,
      required this.footerText,
      required this.footerButtonRoute})
      : super(key: key);

  @override
  State<AuthPage> createState() => _AuthPageState();
}

class _AuthPageState extends State<AuthPage> {
  // ignore: prefer_typing_uninitialized_variables
  var guildId;
  List<bool> validationResults = [];
  List<Color> validationColors = [];

  @override
  void initState() {
    validationResults.addAll(widget.data.map((e) => false).toList());
    validationColors
        .addAll(widget.data.map((e) => ColorHelper.activeColor).toList());
    super.initState();
  }

  Future register() async {
    if (validationResults.any((element) => element == false)) {
      for (var i = 0; i < validationResults.length; i++) {
        if (!validationResults[i]) {
          validationColors[i] = Colors.red;
        } else {
          validationColors[i] = Colors.green;
        }
      }
    } else {
      if (!(await DiscordBotApiService.UserExist(
              widget.data[1].controller.text) ??
          true)) {
        var admin = Administrator(
            nickname: widget.data[0].controller.text,
            email: widget.data[1].controller.text,
            password: widget.data[2].controller.text,
            guildId: guildId);
        await DiscordBotApiService.registerUser(admin);
        for (var i = 0; i < validationColors.length; i++) {
          validationColors[i] = Colors.green;
        }
      } else {
        validationResults[1] = false;
        validationColors[1] = Colors.red;
        widget.data[1].controller.clear();
        setState(() {});
      }
    }
    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    return Material(
      child: Container(
          alignment: Alignment.center,
          child: Container(
              width: 30.w,
              padding: EdgeInsets.symmetric(vertical: 1.h),
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(15),
                boxShadow: [
                  BoxShadow(
                    color: ColorHelper.serverStatusColors.defaultShadowColor,
                    spreadRadius: 5,
                    blurRadius: 7,
                    offset: const Offset(0, 3), // changes position of shadow
                  ),
                ],
                color: ColorHelper.serverStatusColors.defaultBackgroundColor,
              ),
              child: Container(
                margin: EdgeInsets.all(3.sp),
                child: Column(
                  mainAxisSize: MainAxisSize.min,
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Text(
                      widget.title,
                      style: TextStyle(fontSize: 12.sp),
                    ),
                    Column(
                      children: [
                        for (var i = 0; i < widget.data.length; i++)
                          AuthPageInput(
                            controller: widget.data[i].controller,
                            text: widget.data[i].label,
                            validationType: widget.data[i].validationType,
                            validationColor: validationColors[i],
                            validationResultCallback: (result) {
                              validationResults[i] = result;
                            },
                          )
                      ],
                    ),
                    GuildsDropDownButton(
                      setGuildId: (newGuildId) {
                        guildId = newGuildId;
                      },
                    ),
                    Row(
                      children: [
                        Expanded(
                            child: Container(
                          margin: EdgeInsets.all(3.sp),
                          decoration: BoxDecoration(
                              borderRadius: BorderRadius.circular(7),
                              border:
                                  Border.all(color: ColorHelper.activeColor)),
                          child: InkWell(
                            mouseCursor: SystemMouseCursors.click,
                            borderRadius: BorderRadius.circular(25),
                            onTap: register,
                            child: Container(
                                padding: EdgeInsets.symmetric(vertical: 2.sp),
                                child: Text(
                                  "Continue",
                                  textAlign: TextAlign.center,
                                  style: TextStyle(
                                      fontSize: 6.sp,
                                      color: ColorHelper.defaultTextColor),
                                )),
                          ),
                        )),
                      ],
                    ),
                    Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        Text(
                          widget.footerText,
                          style: TextStyle(
                            fontSize: 4.sp,
                          ),
                        ),
                        TextButton(
                            onPressed: () {
                              Navigator.of(context)
                                  .popAndPushNamed(widget.footerButtonRoute);
                            },
                            child: Text(
                              widget.footerButtonText,
                              style: TextStyle(
                                  color: ColorHelper.activeColor,
                                  fontSize: 4.sp,
                                  decoration: TextDecoration.underline),
                            ))
                      ],
                    )
                  ],
                ),
              ))),
    );
  }
}
