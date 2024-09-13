﻿using Discord;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenShock.DiscordBot.OpenShockDiscordDb;
using OpenShock.DiscordBot.Services;
using OpenShock.SDK.CSharp.Models;
using System.Text.RegularExpressions;

namespace OpenShock.DiscordBot.MessageHandler;

public sealed partial class MessageHandler
{
    private readonly IServiceProvider _serviceProvider;


    public MessageHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [GeneratedRegex("\bbot\b", RegexOptions.IgnoreCase | RegexOptions.Singleline)]
    private partial Regex BotChannelMatchingRegex();

    public async Task HandleMessageAsync(SocketMessage message)
    {
        if (message.Author.IsBot || string.IsNullOrEmpty(message.Content)) return;

        // Check if the message contains a swear word
        if (ProfanityDetector.TryGetProfanityWeight(message.Content, out int count, out float weight))
        {
            var intensity = (byte)(weight * 100f);

            // If the channel is a bot channel, respond with debug message
            if (BotChannelMatchingRegex().Match(message.Channel.Name).Success)
            {
                await message.Channel.SendMessageAsync($"Profanity detected! {count} bad {(count > 1 ? "words" : "word")}, shocking at {intensity}%");
            }

            // Trigger the shock
            await using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<OpenShockDiscordContext>();
                var user = await db.Users.FirstOrDefaultAsync(x => x.DiscordId == message.Author.Id);
                if (user == null) return;
                if (!user.ProfanityShocking) return;

                var backendService = scope.ServiceProvider.GetRequiredService<IOpenShockBackendService>();
                await backendService.ControlAllShockers(message.Id, intensity, 1000, ControlType.Shock);
            }

            // Add shock emoji on complete
            await message.AddReactionAsync(new Emoji("⚡"));
        }
    }
}