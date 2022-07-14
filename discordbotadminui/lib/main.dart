import 'package:discordbotadminui/Pages/HomePage.dart';
import 'package:discordbotadminui/Pages/LoginPage.dart';
import 'package:discordbotadminui/Pages/RegisterPage.dart';
import 'package:discordbotadminui/Pages/RolesPage.dart';
import 'package:discordbotadminui/Pages/UsersPage.dart';
import 'package:flutter/material.dart';
import 'package:sizer/sizer.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Sizer(
      builder: (context, orientation, deviceType) {
        return MaterialApp(
          theme: ThemeData(primarySwatch: Colors.blue, fontFamily: "Open Sans"),
          debugShowCheckedModeBanner: false,
          initialRoute: HomePage.route,
          themeMode: ThemeMode.dark,
          routes: {
            HomePage.route: (context) => const HomePage(),
            RegisterPage.route: (context) => const RegisterPage(),
            LoginPage.route: (context) => const LoginPage(),
            UsersPage.route: (context) => const UsersPage(),
            RolesPage.route: (context) => const RolesPage(),
          },
        );
      },
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({Key? key}) : super(key: key);

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  @override
  Widget build(BuildContext context) {
    return const HomePage();
  }
}
