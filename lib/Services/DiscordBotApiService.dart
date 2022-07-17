import 'dart:convert';
import 'dart:io';

import 'package:discordbotadminui/Models/AdministrationGuild.dart';
import 'package:discordbotadminui/Models/Administrator.dart';
import 'package:discordbotadminui/Models/DiscordRole.dart';
import 'package:discordbotadminui/Models/DiscordUser.dart';
import 'package:http/http.dart';

class DiscordBotApiService {
  static List<T> getModelsListFromMap<T>(request) {
    List<T> list = [];
    var data = json.decode(request.body);

    for (var element in data) {
      if (T == AdministrationGuild) {
        list.add(AdministrationGuild.fromMap(element) as T);
      } else if (T == DiscordUser) {
        list.add(DiscordUser.fromMap(element) as T);
      } else if (T == DiscordRole) {
        list.add(DiscordRole.fromMap(element) as T);
      }
    }

    return list;
  }

  static Future<String?> getServerStatus() async {
    var request = await get(Uri.parse('https://localhost:5001/api/Status'));

    if (request.statusCode == 200) {
      return request.body;
    }

    return null;
  }

  static Future<bool?> UserExist(String email) async {
    var request =
        await get(Uri.parse('https://localhost:5001/api/IsExist?email=$email'));

    if (request.statusCode == 200) {
      return request.body.toLowerCase() == "true" ? true : false;
    }

    return null;
  }

  static Future<List<T>> fetchData<T>(String uri) async {
    var request = await get(Uri.parse(uri));
    if (request.statusCode == 200) {
      return getModelsListFromMap<T>(request);
    }

    return [];
  }

  static Future<String> registerUser(Administrator admin) async {
    var headers = {
      'accept': '*/*',
      'Content-Type': 'application/json',
    };

    var data = '''{
      "nickname": "${admin.nickname}",
      "email": "${admin.email}",
      "password": "${admin.password}",
      "guildId": ${admin.guildId}
    }''';

    var url = Uri.parse('https://localhost:5001/api/Auth/Register');
    var res = await post(url, headers: headers, body: data);
    if (res.statusCode != 200) {
      throw Exception('http.post error: statusCode= ${res.statusCode}');
    }

    return "";
  }
}
