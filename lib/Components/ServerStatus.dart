import 'dart:async';

import 'package:discordbotadminui/Components/Timer.dart';
import 'package:discordbotadminui/Services/DiscordBotApiService.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:sizer/sizer.dart';

class ServerStatusComponent extends StatefulWidget {
  final String? serverLink;
  const ServerStatusComponent({Key? key, this.serverLink}) : super(key: key);

  @override
  State<ServerStatusComponent> createState() => _ServerStatusComponentState();
}

class _ServerStatusComponentState extends State<ServerStatusComponent> {
  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      mainAxisSize: MainAxisSize.min,
      children: [
        FutureBuilder(
          future: DiscordBotApiService.getServerStatus(),
          builder: (BuildContext context, AsyncSnapshot<String?> snapshot) {
            switch (snapshot.connectionState) {
              case ConnectionState.done:
                {
                  return StatefulBuilder(
                      builder: (BuildContext context, update) {
                    if (snapshot.data != null) {
                      return Container(
                          width: 85.w,
                          alignment: Alignment.center,
                          padding: EdgeInsets.all(3.sp),
                          decoration: BoxDecoration(
                            borderRadius: BorderRadius.circular(15),
                            boxShadow: [
                              BoxShadow(
                                color: Colors.grey.withOpacity(0.5),
                                spreadRadius: 5,
                                blurRadius: 7,
                                offset: const Offset(
                                    0, 3), // changes position of shadow
                              ),
                            ],
                            color: Colors.white60,
                          ),
                          child: Column(
                            mainAxisAlignment: MainAxisAlignment.center,
                            mainAxisSize: MainAxisSize.min,
                            children: [
                              Icon(
                                Icons.check_circle,
                                color: Colors.green,
                                size: 14.sp,
                              ),
                              Text(
                                snapshot.data!,
                                style: TextStyle(
                                    fontSize: 14.sp, color: Colors.green),
                              ),
                              RefreshTimer(
                                onEnd: () {
                                  setState(() {});
                                },
                                seconds: 20,
                                timePrefix: "Remaining to check connection:",
                                textColor: Colors.green,
                              )
                            ],
                          ));
                    } else {
                      return Container(
                          width: 70.w,
                          alignment: Alignment.center,
                          padding: EdgeInsets.symmetric(vertical: 3.sp),
                          decoration: BoxDecoration(
                            borderRadius: BorderRadius.circular(15),
                            boxShadow: [
                              BoxShadow(
                                color: Colors.grey.withOpacity(0.5),
                                spreadRadius: 5,
                                blurRadius: 7,
                                offset: const Offset(
                                    0, 3), // changes position of shadow
                              ),
                            ],
                            color: Colors.white60,
                          ),
                          child: Column(
                            mainAxisAlignment: MainAxisAlignment.center,
                            mainAxisSize: MainAxisSize.min,
                            children: [
                              Icon(
                                CupertinoIcons.clear_thick_circled,
                                color: Colors.red,
                                size: 14.sp,
                              ),
                              Text(
                                "Server offline",
                                style: TextStyle(
                                    fontSize: 14.sp, color: Colors.red),
                              ),
                              RefreshTimer(
                                onEnd: () {
                                  setState(() {});
                                },
                                seconds: 20,
                                timePrefix: "Remaining to reconnect:",
                                textColor: Colors.red,
                              )
                            ],
                          ));
                    }
                  });
                }
              case ConnectionState.waiting:
                {
                  return Expanded(
                      child: Container(
                          alignment: Alignment.center,
                          child: SizedBox(
                            width: 20.sp,
                            height: 20.sp,
                            child: const CircularProgressIndicator(
                              color: Colors.green,
                            ),
                          )));
                }
              default:
                {
                  return const Expanded(
                      child: Center(
                    child: Text("No data"),
                  ));
                }
            }
          },
        )
      ],
    );
  }
}
