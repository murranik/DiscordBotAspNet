import 'package:discordbotadminui/Components/DataTableCell/DataTableCell.dart';
import 'package:discordbotadminui/Components/DataTableCell/DataTableTextFieldCell.dart';
import 'package:discordbotadminui/Components/DataTableCell/DataTableToolsCell.dart';
import 'package:discordbotadminui/Models/DiscordRole.dart';
import 'package:discordbotadminui/Models/DiscordUser.dart';
import 'package:discordbotadminui/Services/DiscordBotApiService.dart';
import 'package:flutter/material.dart';
import 'package:sizer/sizer.dart';

class RolesPage extends StatefulWidget {
  static const String route = '/roles';
  const RolesPage({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _RolesPageState();
}

class _RolesPageState extends State<RolesPage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Column(
        children: [
          Row(
            mainAxisSize: MainAxisSize.min,
            children: [
              DataTableCell(name: "DiscordId"),
              DataTableCell(name: "Name"),
              DataTableCell(
                name: "Tools",
                flex: 1,
              ),
            ],
          ),
          FutureBuilder(
            future: DiscordBotApiService.fetchRoles(),
            builder: (BuildContext context,
                AsyncSnapshot<List<DiscordRole>> snapshot) {
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
                              for (var discordRole in snapshot.data!)
                                Builder(
                                  builder: (BuildContext context) {
                                    var edit = false;
                                    return Row(
                                      mainAxisSize: MainAxisSize.min,
                                      children: [
                                        DataTableCell(
                                          name: discordRole.name.toString(),
                                        ),
                                        DataTableCell(
                                            name: discordRole.discordId
                                                .toString()),
                                        StatefulBuilder(
                                          builder:
                                              (BuildContext context, update) {
                                            return DataTableToolsCell(
                                              name: "Management",
                                              flex: 1,
                                              edit: edit,
                                              callBack: (value) {
                                                edit = value;
                                                update(() {});
                                              },
                                              save: () {},
                                            );
                                          },
                                        ),
                                      ],
                                    );
                                  },
                                )
                            ],
                          )
                        ],
                      ),
                    ));
                  } else {
                    return Expanded(
                        child: Center(
                      child: Text(
                        "No data",
                        style: TextStyle(fontSize: 10.sp),
                      ),
                    ));
                  }
                case ConnectionState.waiting:
                  return Expanded(
                      child: Container(
                          alignment: Alignment.center,
                          child: SizedBox(
                            width: 20.sp,
                            height: 20.sp,
                            child: CircularProgressIndicator(
                              color: Colors.green,
                            ),
                          )));
                default:
                  return Expanded(
                      child: Text("No data default",
                          style: TextStyle(fontSize: 10.sp)));
              }
            },
          ),
        ],
      ),
    );
  }
}
