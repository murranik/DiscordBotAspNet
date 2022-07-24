import 'package:discordbotadminui/Components/GuildsDropDownButton.dart';
import 'package:discordbotadminui/Components/AuthPageInput.dart';
import 'package:discordbotadminui/Helpers/ColorHelper.dart';
import 'package:discordbotadminui/Models/Administrator.dart';
import 'package:discordbotadminui/Models/AuthPageData.dart';
import 'package:discordbotadminui/Services/DiscordBotApiService.dart';
import 'package:discordbotadminui/Services/UserService.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:sizer/sizer.dart';

//Todo make super admin with all servers
class AuthPage extends StatefulWidget {
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

  @override
  void initState() {
    validationResults.addAll(widget.data.map((e) => false).toList());
    super.initState();
  }

  Future login() async {
    var admin = Administrator(
        nickname: '',
        email: widget.data[0].controller.text,
        password: widget.data[1].controller.text,
        guildId: guildId);
    var res = await DiscordBotApiService.login(admin);
    if (res) {
      UserService.confirmedEmail = true;
      GoRouter.of(context).push("/home");
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
                            validationResultCallback: (result) {
                              validationResults[i] = result;
                            },
                          )
                      ],
                    ),
                    if (widget.title == "Login")
                      GuildsDropDownButton(
                        guildIdCallback: (newGuildId) {
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
                            onTap: login,
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
                  ],
                ),
              ))),
    );
  }
}
