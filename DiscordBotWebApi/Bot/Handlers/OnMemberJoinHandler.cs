﻿using Discord.WebSocket;
using Interfaces;
using Models;
using System.Collections.ObjectModel;

namespace DiscordBotWebApi.Bot.Handlers
{
	public class OnMemberJoinHandler
	{
		public readonly IList<string> strings = new ReadOnlyCollection<string>(new List<string> {
			"Легендарный", "Невероянтый", "Жесткий", "Разрывной", ":male_sign:Dungeon Master:male_sign: " });
		private readonly DiscordSocketClient _client;
		private readonly IUserService _userService;

		public OnMemberJoinHandler(DiscordSocketClient client, IUserService userService)
		{
			_client = client;
			_userService = userService;
		}

		public async Task MessageSender(SocketGuildUser user)
		{
			if (_client != null)
			{
				Random random = new();
				var channel = _client.GetChannel(942780457232257044) as SocketTextChannel;
				await _userService.AddUser(new DiscordUser()
				{
					GuildId = user.Guild.Id.ToString(),
					Name = user.Username,
					PrestigeLevel = 0,
					DiscordId = user.Id.ToString()
				});
				await _userService.GeneratePersonalStatisticChat(user, _client);
				await channel.SendMessageAsync($"Это же тот самый {strings[random.Next(strings.Count)]} {user.Mention} добро пожаловать на {channel.Guild.Name}");
			}
		}
	}
}
