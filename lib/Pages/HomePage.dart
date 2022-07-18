import 'package:discordbotadminui/Components/ServerStatus.dart';
import 'package:discordbotadminui/Helpers/ColorHelper.dart';
import 'package:discordbotadminui/Pages/NotFoundPage.dart';
import 'package:discordbotadminui/Pages/RolesPage.dart';
import 'package:discordbotadminui/Pages/UsersPage.dart';
import 'package:flutter/material.dart';
import 'package:discordbotadminui/Components/NavMenuButton.dart';
import 'package:go_router/go_router.dart';
import 'package:sizer/sizer.dart';

class HomePage extends StatefulWidget {
  static const String route = '/home';
  const HomePage({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  var navButtonsList = [ColorHelper.activeColor, null, null];

  @override
  Widget build(BuildContext context) {
    return Material(
      child: Column(children: [
        Expanded(
          flex: 1,
          child: Container(
            color: ColorHelper.defaultNavMenuBackgroundColor,
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Row(
                  children: [
                    NavMenuButton(
                      text: "Home",
                      opClick: () {
                        navButtonsList[0] = ColorHelper.activeColor;
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
                        navButtonsList[1] = ColorHelper.activeColor;
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
                        navButtonsList[2] = ColorHelper.activeColor;
                        setState(() {});
                      },
                      choosedColor: navButtonsList[2],
                    ),
                  ],
                ),
                IconButton(
                    onPressed: () {
                      GoRouter.of(context).go("/register");
                    },
                    iconSize: 10.sp,
                    icon: const Icon(
                      Icons.person,
                      color: ColorHelper.defaultNavMenuTextColor,
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
                    return const UsersPage();
                  } else if (navButtonsList[2] != null) {
                    return const RolesPage();
                  } else if (navButtonsList[0] != null) {
                    return const ServerStatusComponent();
                  } else {
                    return const NotFoundPage();
                  }
                },
              )),
        ),
      ]),
    );
  }
}
