import 'dart:async';

import 'package:flutter/material.dart';
import 'package:sizer/sizer.dart';

class RefreshTimer extends StatefulWidget {
  final Function onEnd;
  final int seconds;
  final String? timePrefix;
  final Color textColor;
  const RefreshTimer(
      {Key? key,
      required this.onEnd,
      required this.seconds,
      this.timePrefix,
      this.textColor = Colors.black})
      : super(key: key);

  @override
  State<RefreshTimer> createState() => _RefreshTimerState();
}

class _RefreshTimerState extends State<RefreshTimer> {
  late Timer _timer;
  late int _countDownValue;

  @override
  void initState() {
    _countDownValue = widget.seconds;
    startTimer();
    super.initState();
  }

  void startTimer() {
    const oneSec = Duration(seconds: 1);
    _timer = Timer.periodic(
      oneSec,
      (Timer timer) {
        if (_countDownValue == 0) {
          setState(() {
            _countDownValue = widget.seconds;
            widget.onEnd();
          });
        } else {
          setState(() {
            _countDownValue--;
          });
        }
      },
    );
  }

  Widget build(BuildContext context) {
    return Text(
      "${widget.timePrefix} ${intToTimeLeft(_countDownValue)}",
      style: TextStyle(fontSize: 9.sp, color: widget.textColor),
    );
  }

  String intToTimeLeft(int value) {
    int h, m, s;

    h = value ~/ 3600;

    m = ((value - h * 3600)) ~/ 60;

    s = value - (h * 3600) - (m * 60);

    String minuteLeft =
        m.toString().length < 2 ? "0" + m.toString() : m.toString();

    String secondsLeft =
        s.toString().length < 2 ? "0" + s.toString() : s.toString();

    String result = "$secondsLeft";

    return result;
  }

  @override
  void dispose() {
    _timer.cancel();
    super.dispose();
  }
}
