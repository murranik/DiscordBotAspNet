class DiscordRole {
  int discordId;
  int? userId;
  int? discordUserId;
  String name;

  DiscordRole(
      {required this.discordId,
      this.userId,
      this.discordUserId,
      required this.name});

  factory DiscordRole.fromMap(Map<String, dynamic> json) => DiscordRole(
      discordId: json['discordId'],
      discordUserId: json['discordUserId'],
      userId: json['userId'],
      name: json['name']);

  Map<String, dynamic> toMap() {
    return {
      'discordId': discordId,
      'discordUserId': discordUserId,
      'userId': userId,
      'name': name,
    };
  }
}
