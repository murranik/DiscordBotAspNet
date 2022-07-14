import 'package:discordbotadminui/Models/AdministrationGuild.dart';
import 'package:discordbotadminui/Services/DiscordBotApiService.dart';
import 'package:dropdown_button2/dropdown_button2.dart';
import 'package:flutter/material.dart';
import 'package:sizer/sizer.dart';

class GuildsDropDownButton extends StatelessWidget {
  final Function setGuildId;
  const GuildsDropDownButton({Key? key, required this.setGuildId})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: EdgeInsets.only(top: 3.sp),
      child: FutureBuilder(
          future: DiscordBotApiService.fetchGuilds(),
          builder:
              (context, AsyncSnapshot<List<AdministrationGuild>> snapshot) {
            switch (snapshot.connectionState) {
              case ConnectionState.done:
                {
                  if (snapshot.data != null) {
                    setGuildId(snapshot.data!.first.id);
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
                            .map<DropdownMenuItem<String>>((String value) {
                            return DropdownMenuItem<String>(
                              value: value,
                              child: Text(value),
                            );
                          }).toList(),
                    value: snapshot.data == null
                        ? "No connected guild"
                        : snapshot.data!.first.name,
                    style: TextStyle(fontSize: 5.sp),
                    onChanged: (value) {
                      if (snapshot.data != null) {
                        setGuildId(snapshot.data!
                            .firstWhere((element) => element.name == value)
                            .id);
                      }
                    },
                    barrierColor: const Color.fromARGB(130, 232, 226, 220),
                    iconSize: 6.sp,
                    iconEnabledColor: Colors.black,
                    iconDisabledColor: Colors.grey,
                    buttonHeight: 50,
                    buttonWidth: 24.w,
                    buttonPadding: const EdgeInsets.only(left: 14, right: 14),
                    buttonDecoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(8),
                      border: Border.all(
                        color: Colors.green,
                      ),
                      color: Colors.transparent,
                    ),
                    dropdownDecoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(7),
                      color: Colors.white,
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
                    color: Colors.green,
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
