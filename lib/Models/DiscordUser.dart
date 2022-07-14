class DiscordUser {
  int id;
  String discordId;
  String guildId;
  String name;
  int prestigeLevel;

  DiscordUser(
      {required this.id,
      required this.discordId,
      required this.guildId,
      required this.name,
      required this.prestigeLevel});

  factory DiscordUser.fromMap(Map<String, dynamic> json) => DiscordUser(
      id: json['id'],
      discordId: json['discordId'],
      guildId: json['guildId'],
      name: json['name'],
      prestigeLevel: json['prestigeLevel']);

  Map<String, dynamic> toMap() {
    return {
      'id': id,
      'discordId': discordId,
      'guildId': guildId,
      'name': name,
      'prestigeLevel': prestigeLevel,
    };
  }
}
