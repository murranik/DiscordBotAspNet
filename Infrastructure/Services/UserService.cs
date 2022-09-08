﻿using Discord;
using Discord.WebSocket;
using Infrastructure.Database;
using Infrastructure.Specifications;
using Interfaces;
using Models;

namespace Infrastructure.Services
{
	public class UserService : IUserService
    {
        private readonly IRepository<DiscordUser> _repository;
        private readonly IRepository<DiscordRole> _roleRepository;
        private readonly DiscordBotContext _discordBotContext;

		public UserService(IRepository<DiscordUser> repository, IRepository<DiscordRole> roleRepository, DiscordBotContext discordBotContext)
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _discordBotContext = discordBotContext;
        }

        public async Task GeneratePersonalStatisticChats(DiscordSocketClient discordSocketClient) 
        {
            var guild = discordSocketClient.GetGuild(873689102569054268) as IGuild;

            var statsCategory = (await guild.GetCategoriesAsync()).FirstOrDefault(x => x.Name == "Stats");
            if (statsCategory != null) 
            {
                await statsCategory.DeleteAsync();
            }
            var newStatsCategory = await guild.CreateCategoryAsync(
                "Stats",
                (GuildChannelProperties guildChannelProperties) => {
                    guildChannelProperties.Position = 0;
            });

            var users = await guild.GetUsersAsync();

            foreach (var user in users.Where(x => !x.IsBot).ToList())
            {
                var userPrivateChannel = await guild.CreateTextChannelAsync
                    (
                        user.Username + " stats",
                        x => x.CategoryId = newStatsCategory.Id
                    );

                var permissionOverrides = new OverwritePermissions(viewChannel: PermValue.Deny);
                await userPrivateChannel.AddPermissionOverwriteAsync(guild.EveryoneRole, permissionOverrides);
                await userPrivateChannel.AddPermissionOverwriteAsync(user, new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Deny));
                await userPrivateChannel.SendMessageAsync($"Добрий день {user.Username}. Раді вітати на нашому сервері! \nТут буде знаходитись ваша персональна статистаки з цього сервера");
            }
        }

        public async Task GeneratePersonalStatisticChat(SocketGuildUser user, DiscordSocketClient discordSocketClient) 
        {
            var guild = discordSocketClient.GetGuild(873689102569054268) as IGuild;

            var statsCategory = (await guild.GetCategoriesAsync()).FirstOrDefault(x => x.Name == "АStats");

            var users = await guild.GetUsersAsync();

            var userPrivateChannel = await guild.CreateTextChannelAsync
                        (
                            user.Username + " stats",
                            x => x.CategoryId = statsCategory.Id
                        );

            var permissionOverrides = new OverwritePermissions(viewChannel: PermValue.Deny);
            await userPrivateChannel.AddPermissionOverwriteAsync(guild.EveryoneRole, permissionOverrides);
            await userPrivateChannel.AddPermissionOverwriteAsync(user, new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Deny));
            await userPrivateChannel.SendMessageAsync($"Добрий день {user.Username}. Раді вітати на нашому сервері! \nТут буде знаходитись ваша персональна статистаки з цього сервера");
        }

        public async Task RemoveUsersStatsChannel(DiscordSocketClient discordSocketClient) 
        {
            var guild = discordSocketClient.GetGuild(873689102569054268) as IGuild;
            var channels = (await guild.GetChannelsAsync()).Where(x => x.Name.Contains("stats")).ToList();
            foreach (SocketGuildChannel channel in channels)
            {
                await channel.DeleteAsync();
                Console.WriteLine("Successfully deleted channel with name - " + channel.Name);
            }
        }

        public async Task RemoveUserStatsChannel(SocketUser user, DiscordSocketClient discordSocketClient) 
        {
            var guild = discordSocketClient.GetGuild(873689102569054268) as IGuild;
            var channel = (await guild.GetChannelsAsync()).FirstOrDefault(x => x.Name.Contains($"{user.Username}-stats"));
            if(channel != null)
                await channel.DeleteAsync();
        }

        public async Task FetchAllUsersFromDiscord(DiscordSocketClient discordSocketClient) 
        {
            var guild = discordSocketClient.GetGuild(873689102569054268) as IGuild;
            var users = await guild.GetUsersAsync();
            foreach (var user in users.Where(x => !x.IsBot))
            {
                var tmpUser = new DiscordUser
                {
                    Name = user.Username,
                    PrestigeLevel = 0,
                    GuildId = user.GuildId.ToString(),
                    DiscordId = user.Id.ToString()
                };

                await AddUser(tmpUser);
            }
        }

        public async Task FetchAllRolesFromDiscord(DiscordSocketClient discordSocketClient)
        {
            var guild = discordSocketClient.GetGuild(873689102569054268) as IGuild;
            var roles = guild.Roles;
            foreach (var role in roles)
            {
                var tmpRole = new DiscordRole
                {
                    DiscordId = role.Id,
                    Name = role.Name
                };

                await AddRole(tmpRole);
            }
        }

        public async Task AddUser(DiscordUser discordUser) 
        {
            if (await _repository.GetBySpecAsync(new CheckIsUserExistSpec(discordUser.DiscordId)) == null)
            {
                await _repository.AddAsync(discordUser);
            }
        }

        public async Task AddRole(DiscordRole role)
        {
            if (!_discordBotContext.Roles.Any(x => x.DiscordId == role.DiscordId)) 
            {
                await _discordBotContext.Roles.AddAsync(role);
                await _discordBotContext.SaveChangesAsync();
            }
                
        }

        public async Task UpdateRole(DiscordRole role)
        {
            _discordBotContext.Roles.Update(role);
            await _discordBotContext.SaveChangesAsync();
        } 


        public async Task RemoveRole(ulong roleId) 
        {
            var role = (await _roleRepository.ListAsync()).FirstOrDefault(x => x.DiscordId == roleId);
            await _roleRepository.DeleteAsync(role);
        }

        public async Task<int> AddPointsFotUser(string userId, int pointsCount)
        {
            var tmpUser = await _repository.GetBySpecAsync(new CheckIsUserExistSpec(userId));
            if (tmpUser != null)
            {
                tmpUser.PrestigeLevel += pointsCount;
                await _repository.UpdateAsync(tmpUser);
            }

            if(tmpUser != null)
                return tmpUser.PrestigeLevel;
            else
                return 0;
        }

        public async Task<List<DiscordUser>> GetAllUsers()
        {
            return await _repository.ListAsync();
        }

        public async Task<List<DiscordRole>> GetAllRoles()
        {
            return await _roleRepository.ListAsync();
        }
    }
}
