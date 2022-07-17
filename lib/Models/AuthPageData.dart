import 'package:discordbotadminui/Enums/ValidationTypes.dart';
import 'package:flutter/cupertino.dart';

class AuthPageData {
  TextEditingController controller;
  ValidationTypes validationType;
  String label;

  AuthPageData(
      {required this.controller,
      required this.label,
      required this.validationType});
}
