import 'package:discordbotadminui/Enums/ValidationTypes.dart';
import 'package:discordbotadminui/Helpers/ColorHelper.dart';
import 'package:flutter/material.dart';
import 'package:sizer/sizer.dart';

class AuthPageInput extends StatefulWidget {
  final TextEditingController controller;
  final String text;
  final ValidationTypes validationType;
  final Function validationResultCallback;
  const AuthPageInput({
    Key? key,
    required this.controller,
    required this.text,
    required this.validationType,
    required this.validationResultCallback,
  }) : super(key: key);

  @override
  State<AuthPageInput> createState() => _AuthPageInputState();
}

class _AuthPageInputState extends State<AuthPageInput> {
  var valid = false;
  var repeatValid = false;
  var repeatConttoller = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: EdgeInsets.only(top: 3.sp),
      height: widget.validationType == ValidationTypes.repeat ? 17.h : 8.h,
      child: Column(children: [
        Row(
          mainAxisSize: MainAxisSize.min,
          children: [
            Expanded(
              child: TextField(
                style: TextStyle(fontSize: 4.sp),
                controller: widget.controller,
                onChanged: (value) {
                  switch (widget.validationType) {
                    case ValidationTypes.notEmpty:
                      if (value.isNotEmpty) {
                        valid = true;
                      } else {
                        valid = false;
                      }
                      setState(() {});
                      break;
                    case ValidationTypes.email:
                      if (RegExp(
                              r"^[a-zA-Z0-9.a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9]+\.[a-zA-Z]+")
                          .hasMatch(value)) {
                        valid = true;
                      } else {
                        valid = false;
                      }
                      setState(() {});
                      break;
                    case ValidationTypes.repeat:
                      if (value.isNotEmpty) {
                        valid = true;
                      } else {
                        valid = false;
                      }
                      setState(() {});
                      break;
                  }

                  var repeatValidationResult =
                      valid == true && repeatValid == true;

                  widget.validationResultCallback(
                      widget.validationType == ValidationTypes.repeat
                          ? repeatValidationResult
                          : valid);
                },
                decoration: InputDecoration(
                    labelText: widget.text,
                    suffixIcon: valid
                        ? const Icon(
                            Icons.done,
                            color: ColorHelper.activeColor,
                          )
                        : const Icon(
                            Icons.close,
                            color: ColorHelper.cancelColor,
                          ),
                    labelStyle: TextStyle(
                        fontSize: 5.sp, color: Color.fromARGB(150, 0, 0, 0)),
                    enabledBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(7),
                        borderSide: BorderSide(color: ColorHelper.activeColor)),
                    focusedBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(7),
                        borderSide:
                            BorderSide(color: ColorHelper.activeColor))),
              ),
            ),
          ],
        ),
        if (widget.validationType == ValidationTypes.repeat)
          Container(
            margin: EdgeInsets.only(top: 3.sp),
            child: Row(
              mainAxisSize: MainAxisSize.min,
              children: [
                Expanded(
                  child: TextField(
                    style: TextStyle(fontSize: 4.sp),
                    controller: repeatConttoller,
                    onChanged: (value) {
                      if (value == widget.controller.text) {
                        repeatValid = true;
                      } else {
                        repeatValid = false;
                      }

                      var repeatValidationResult =
                          valid == true && repeatValid == true;
                      if (widget.validationType == ValidationTypes.repeat) {
                        widget.validationResultCallback(repeatValidationResult);
                      }
                      setState(() {});
                    },
                    decoration: InputDecoration(
                        labelText: "Repeat ${widget.text.toLowerCase()}",
                        suffixIcon: repeatValid
                            ? const Icon(
                                Icons.done,
                                color: Colors.green,
                              )
                            : const Icon(
                                Icons.close,
                                color: Colors.red,
                              ),
                        labelStyle: TextStyle(
                            fontSize: 5.sp,
                            color: Color.fromARGB(150, 0, 0, 0)),
                        enabledBorder: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(7),
                            borderSide:
                                BorderSide(color: ColorHelper.activeColor)),
                        focusedBorder: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(7),
                            borderSide:
                                BorderSide(color: ColorHelper.activeColor))),
                  ),
                ),
              ],
            ),
          )
      ]),
    );
  }
}
