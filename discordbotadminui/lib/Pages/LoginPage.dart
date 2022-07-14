import 'package:discordbotadminui/Components/GuildsDropDownButton.dart';
import 'package:discordbotadminui/Components/RegisterPageInput.dart';
import 'package:discordbotadminui/Enums/ValidationTypes.dart';
import 'package:discordbotadminui/Models/Administrator.dart';
import 'package:discordbotadminui/Pages/RegisterPage.dart';
import 'package:discordbotadminui/Services/DiscordBotApiService.dart';
import 'package:flutter/material.dart';
import 'package:sizer/sizer.dart';

class LoginPage extends StatefulWidget {
  static const String route = '/login';
  const LoginPage({Key? key}) : super(key: key);

  @override
  State<LoginPage> createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  var usernameController = TextEditingController();
  var emailController = TextEditingController();
  var passwordController = TextEditingController();
  // ignore: prefer_typing_uninitialized_variables
  var guildId;
  var validationResults = [false, false, false];
  var validationColors = [Colors.green, Colors.green, Colors.green];

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
                    color: Colors.grey.withOpacity(0.5),
                    spreadRadius: 5,
                    blurRadius: 7,
                    offset: const Offset(0, 3), // changes position of shadow
                  ),
                ],
                color: Colors.white60,
              ),
              child: Container(
                margin: EdgeInsets.all(3.sp),
                child: Column(
                  mainAxisSize: MainAxisSize.min,
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Text(
                      "Login",
                      style: TextStyle(fontSize: 12.sp),
                    ),
                    Column(
                      children: [
                        RegisterPageInput(
                          controller: usernameController,
                          text: "Username",
                          validationType: ValidationTypes.notEmpty,
                          validationColor: validationColors[0],
                          validationResultCallback: (result) {
                            validationResults[0] = result;
                          },
                        ),
                        RegisterPageInput(
                          controller: emailController,
                          text: "Email",
                          validationType: ValidationTypes.email,
                          validationColor: validationColors[1],
                          validationResultCallback: (result) {
                            validationResults[1] = result;
                          },
                        ),
                        RegisterPageInput(
                          controller: passwordController,
                          text: "Password",
                          validationType: ValidationTypes.notEmpty,
                          validationColor: validationColors[2],
                          validationResultCallback: (result) {
                            validationResults[2] = result;
                          },
                        ),
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
                              border: Border.all(color: Colors.green)),
                          child: InkWell(
                            mouseCursor: SystemMouseCursors.click,
                            borderRadius: BorderRadius.circular(25),
                            onTap: () async {
                              if (validationResults
                                  .any((element) => element == false)) {
                                validationColors
                                    .forEach((element) => element = Colors.red);
                              } else {
                                validationColors.forEach(
                                    (element) => element = Colors.green);
                              }
                            },
                            child: Container(
                                padding: EdgeInsets.symmetric(vertical: 2.sp),
                                child: Text(
                                  "Continue",
                                  textAlign: TextAlign.center,
                                  style: TextStyle(
                                      fontSize: 6.sp, color: Colors.black),
                                )),
                          ),
                        )),
                      ],
                    ),
                    Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        Text(
                          "Don't have an account?",
                          style: TextStyle(
                            fontSize: 4.sp,
                          ),
                        ),
                        TextButton(
                            onPressed: () {
                              Navigator.of(context)
                                  .pushNamed(RegisterPage.route);
                            },
                            child: Text(
                              "Register",
                              style: TextStyle(
                                  color: Colors.green,
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
