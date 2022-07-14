import 'package:discordbotadminui/Components/ServerStatus.dart';
import 'package:discordbotadminui/Pages/NotFoundPage.dart';
import 'package:discordbotadminui/Pages/RegisterPage.dart';
import 'package:discordbotadminui/Pages/RolesPage.dart';
import 'package:discordbotadminui/Pages/UsersPage.dart';
import 'package:flutter/material.dart';
import 'package:discordbotadminui/Components/NavMenuButton.dart';
import 'package:sizer/sizer.dart';

class HomePage extends StatefulWidget {
  static const String route = '/home';
  const HomePage({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  final activeColor = Colors.green;
  var navButtonsList = [Colors.green, null, null];

  @override
  Widget build(BuildContext context) {
    return Material(
      child: Column(children: [
        Expanded(
          flex: 1,
          child: Container(
            color: const Color(0xff333333),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Row(
                  children: [
                    NavMenuButton(
                      text: "Home",
                      opClick: () {
                        navButtonsList[0] = activeColor;
                        navButtonsList[1] = null;
                        navButtonsList[2] = null;
                        setState(() {});
                      },
                      choosedColor: navButtonsList[0],
                    ),
                    NavMenuButton(
                      text: "Users",
                      opClick: () {
                        navButtonsList[0] = null;
                        navButtonsList[1] = activeColor;
                        navButtonsList[2] = null;
                        setState(() {});
                      },
                      choosedColor: navButtonsList[1],
                    ),
                    NavMenuButton(
                      text: "Roles",
                      opClick: () {
                        navButtonsList[0] = null;
                        navButtonsList[1] = null;
                        navButtonsList[2] = activeColor;
                        setState(() {});
                      },
                      choosedColor: navButtonsList[2],
                    ),
                  ],
                ),
                IconButton(
                    onPressed: () {
                      Navigator.of(context).pushNamed(RegisterPage.route);
                    },
                    iconSize: 10.sp,
                    icon: const Icon(
                      Icons.person,
                      color: Colors.white,
                    ))
              ],
            ),
          ),
        ),
        Expanded(
          flex: 9,
          child: Container(
              color: Colors.transparent,
              child: Builder(
                builder: (BuildContext context) {
                  if (navButtonsList[1] != null) {
                    return UsersPage();
                  } else if (navButtonsList[2] != null) {
                    return RolesPage();
                  } else if (navButtonsList[0] != null) {
                    return ServerStatusComponent();
                  } else {
                    return NotFoundPage();
                  }
                },
              )),
        ),
      ]),
    );
  }
}
