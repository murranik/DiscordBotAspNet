﻿namespace Models
{
	public class DiscordRole
    {
        public ulong DiscordId { get; set; }
        public virtual ulong? UserId { get; set; }
        public virtual ulong? DiscordUserId { get; set; }
        public string Name { get; set; }
    }
}
