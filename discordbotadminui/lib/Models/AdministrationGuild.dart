class AdministrationGuild {
  int id;
  String name;

  AdministrationGuild({required this.id, required this.name});

  factory AdministrationGuild.fromMap(Map<String, dynamic> json) =>
      AdministrationGuild(id: json['id'], name: json['name']);

  Map<String, dynamic> toMap() {
    return {
      'id': id,
      'name': name,
    };
  }
}
