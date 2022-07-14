import 'package:flutter/material.dart';
import 'package:sizer/sizer.dart';

class NotFoundPage extends StatelessWidget {
  const NotFoundPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Center(
        child: Text(
      "404\nPage not found",
      style: TextStyle(fontSize: 14.sp),
      textAlign: TextAlign.center,
    ));
  }
}
