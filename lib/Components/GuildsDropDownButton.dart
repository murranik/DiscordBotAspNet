import 'package:discordbotadminui/Helpers/ColorHelper.dart';
import 'package:discordbotadminui/Models/AdministrationGuild.dart';
import 'package:discordbotadminui/Services/DiscordBotApiService.dart';
import 'package:dropdown_button2/dropdown_button2.dart';
import 'package:flutter/material.dart';
import 'package:sizer/sizer.dart';

class GuildsDropDownButton extends StatelessWidget {
  final Function guildIdCallback;
  const GuildsDropDownButton({Key? key, required this.guildIdCallback})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: EdgeInsets.only(top: 3.sp),
      height: 8.h,
      child: FutureBuilder(
          future: DiscordBotApiService.fetchData<AdministrationGuild>(
              "https://localhost:5001/api/Get/SocketGuild"),
          builder: (context, AsyncSnapshot<List<dynamic>> snapshot) {
            switch (snapshot.connectionState) {
              case ConnectionState.done:
                {
                  if (snapshot.data != null) {
                    guildIdCallback(snapshot.data!.first.id);
                  }
                  return DropdownButtonHideUnderline(
                      child: DropdownButton2(
                    items: snapshot.data == null
                        ? [
                            const DropdownMenuItem<String>(
                              value: "No connected guild",
                              child: Text("No connected guild"),
                            )
                          ]
                        : snapshot.data!
                            .map((e) => e.name)
                            .toList()
                            .map<DropdownMenuItem<String>>((dynamic value) {
                            return DropdownMenuItem<String>(
                              value: value,
                              child: Text(value),
                            );
                          }).toList(),
                    value: snapshot.data == null
                        ? "No connected guild"
                        : snapshot.data!.first.name,
                    style: TextStyle(fontSize: 4.sp),
                    onChanged: (value) {
                      if (snapshot.data != null) {
                        guildIdCallback(snapshot.data!
                            .firstWhere((element) => element.name == value)
                            .id);
                      }
                    },
                    barrierColor:
                        ColorHelper.dropdownButtonColors.defaultBarrierColor,
                    iconSize: 4.sp,
                    iconEnabledColor:
                        ColorHelper.dropdownButtonColors.defaultIconEnableColor,
                    iconDisabledColor: ColorHelper
                        .dropdownButtonColors.defaultIconDisabledColor,
                    buttonHeight: 8.h,
                    buttonWidth: MediaQuery.of(context).size.width,
                    buttonPadding: const EdgeInsets.only(left: 14, right: 14),
                    buttonDecoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(8),
                      border: Border.all(
                        color: ColorHelper.activeColor,
                      ),
                      color: Colors.transparent,
                    ),
                    dropdownDecoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(7),
                      color: ColorHelper.activeColor,
                    ),
                    dropdownMaxHeight: 200,
                    dropdownWidth: 24.w,
                    scrollbarRadius: const Radius.circular(40),
                    scrollbarThickness: 6,
                    scrollbarAlwaysShow: true,
                  ));
                }
              case ConnectionState.waiting:
                {
                  return const CircularProgressIndicator(
                    color: ColorHelper.activeColor,
                  );
                }
              default:
                {
                  return const Center(
                    child: Text("No data"),
                  );
                }
            }
          }),
    );
  }
}
