import 'package:discordbotadminui/Components/DataTableCell/DataTableCell.dart';
import 'package:discordbotadminui/Components/DataTableCell/DataTableTextFieldCell.dart';
import 'package:discordbotadminui/Components/DataTableCell/DataTableToolsCell.dart';
import 'package:discordbotadminui/Helpers/ColorHelper.dart';
import 'package:discordbotadminui/Models/DiscordUser.dart';
import 'package:discordbotadminui/Services/DiscordBotApiService.dart';
import 'package:flutter/material.dart';
import 'package:sizer/sizer.dart';

class UsersPage extends StatefulWidget {
  static const String route = '/users';
  const UsersPage({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _UsersPageState();
}

class _UsersPageState extends State<UsersPage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Column(
        children: [
          Row(
            mainAxisSize: MainAxisSize.min,
            children: [
              DataTableCell(name: "Id", flex: 6),
              DataTableCell(name: "DiscordId", flex: 22),
              DataTableCell(name: "GuildId", flex: 22),
              DataTableCell(
                name: "Name",
                flex: 20,
              ),
              Expanded(
                  flex: 30,
                  child: Row(
                    children: [
                      DataTableCell(
                        name: "PrestigeLevel",
                        flex: 20,
                      ),
                      DataTableCell(name: "Tools", flex: 10),
                    ],
                  ))
            ],
          ),
          FutureBuilder(
            future: DiscordBotApiService.fetchData<DiscordUser>(
                "https://localhost:5001/api/Get/DiscordUser"),
            builder:
                (BuildContext context, AsyncSnapshot<List<dynamic>> snapshot) {
              var edit = false;
              switch (snapshot.connectionState) {
                case ConnectionState.done:
                  if (snapshot.hasData) {
                    return Expanded(
                        child: SingleChildScrollView(
                      child: Column(
                        mainAxisSize: MainAxisSize.min,
                        children: [
                          ListView(
                            shrinkWrap: true,
                            padding: EdgeInsets.zero,
                            children: [
                              for (var discordUser in snapshot.data!)
                                Row(
                                  mainAxisSize: MainAxisSize.min,
                                  children: [
                                    DataTableCell(
                                        name: discordUser.id.toString(),
                                        flex: 6),
                                    DataTableCell(
                                        name: discordUser.discordId, flex: 22),
                                    DataTableCell(
                                        name: discordUser.guildId, flex: 22),
                                    DataTableCell(
                                      name: discordUser.name,
                                      flex: 20,
                                    ),
                                    StatefulBuilder(
                                      builder: (BuildContext context,
                                          void Function(void Function())
                                              update) {
                                        return Expanded(
                                            flex: 30,
                                            child: Row(
                                              children: [
                                                if (edit)
                                                  DataTableTextFieldCell(
                                                    name: discordUser
                                                        .prestigeLevel
                                                        .toString(),
                                                    flex: 20,
                                                  )
                                                else
                                                  DataTableCell(
                                                    name: discordUser
                                                        .prestigeLevel
                                                        .toString(),
                                                    flex: 20,
                                                  ),
                                                DataTableToolsCell(
                                                  name: "Management",
                                                  flex: 10,
                                                  edit: edit,
                                                  callBack: (value) {
                                                    edit = value;
                                                    update(() {});
                                                  },
                                                  save: () {},
                                                ),
                                              ],
                                            ));
                                      },
                                    )
                                  ],
                                ),
                            ],
                          )
                        ],
                      ),
                    ));
                  } else {
                    return Expanded(
                        child: Center(
                      child: Text("No data",
                          style: TextStyle(
                              fontSize: 10.sp,
                              color: ColorHelper.defaultTextColor)),
                    ));
                  }
                case ConnectionState.waiting:
                  return Expanded(
                      child: Container(
                          alignment: Alignment.center,
                          child: SizedBox(
                            width: 20.sp,
                            height: 20.sp,
                            child: const CircularProgressIndicator(
                              color: ColorHelper.activeColor,
                            ),
                          )));
                default:
                  return Expanded(
                      child: Center(
                    child: Text("No data default",
                        style: TextStyle(
                            fontSize: 10.sp,
                            color: ColorHelper.defaultTextColor)),
                  ));
              }
            },
          ),
        ],
      ),
    );
  }
}
