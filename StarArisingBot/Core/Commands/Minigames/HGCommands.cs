using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarArisingBot.MinigameEngine;
using DSharpPlus.Interactivity;
using DSharpPlus.EventArgs;
using DSharpPlus;
using StarArisingBot.Minigames.HungerGames;

namespace StarArisingBot.Core.Commands
{
    public class HGCommands : BaseCommandModule
    {
        [Command("HungerGames"), Aliases("HG"), Description("Quem séra o ultimo a sair vivo dos Jogos Vorazes?")]
        public async Task HungerGames(CommandContext ctx)
        {
            MinigameSessionBuilder sessionBuilder = new()
            {
                AuthorType = MinigameSessionAuthorType.Guild,
                InvokeType = MinigameSessionInvokeType.Guild,
            };
            if (await MinigameInstanceClient.GetInstanceAsync<HGMinigame>().Result.GetSessionAsync(ctx.Guild.Id) != null)
            {
                await ctx.RespondAsync($"<@{ctx.User.Id}> **JÁ ESTÁ OCORRENDO UM HUNGER GAMES NO SERVIDOR, ESPERE TERMINAR ANTES DE COMEÇAR OUTRO**");
                return;
            }

            //======================================//

            DiscordMessageBuilder actionMenuMessage = new();

            actionMenuMessage.AddEmbed(new DiscordEmbedBuilder()
            {
                Title = ":crossed_swords: ● JOGOS VORAZES ● :crossed_swords:",
                Description = "Seja bem-vindo ao **Simulador dos jogos Vorazes**! Crie partidas **Tensas, Emocionantes e de Abalar Corações** com este simulador. \n\n" +
                              "Antes de começar, escolha abaixo como será a seleção de jogadores: \n\n" +
                              ":zero: ● **All** ➤ O Bot irá selecionar todos os membros do servidor. \n" +
                              ":one: ● **Members** ➤ O Bot irá selecionar todos os membros do servidor (Sem bots). \n" +
                              ":two: ● **Bots** ➤ O Bot irá selecionar todos os membros que são bots no servidor. \n" +
                              ":three: ● **NPCs** ➤ O Bot irá criar seus proprios NPCs para os jogos vorazes. \n" +
                              ":four: ● **Select** ➤ Selecione apenas membros desejados. \n\n" +
                              "*Clique em um dos botões abaixo para continuar.*",
                Color = DiscordColor.Green,
            });
            actionMenuMessage.AddComponents(new DiscordComponent[] {
                new DiscordButtonComponent(ButtonStyle.Primary, "all_action", "All", false, new DiscordComponentEmoji(DiscordEmoji.FromName(ctx.Client, ":zero:"))),
                new DiscordButtonComponent(ButtonStyle.Primary, "members_action", "Members", false, new DiscordComponentEmoji(DiscordEmoji.FromName(ctx.Client, ":one:"))),
                new DiscordButtonComponent(ButtonStyle.Primary, "bots_action", "Bots", false, new DiscordComponentEmoji(DiscordEmoji.FromName(ctx.Client, ":two:"))),
                new DiscordButtonComponent(ButtonStyle.Primary, "npcs_action", "NPCs", false, new DiscordComponentEmoji(DiscordEmoji.FromName(ctx.Client, ":three:"))),
                new DiscordButtonComponent(ButtonStyle.Primary, "select_action", "Select", false, new DiscordComponentEmoji(DiscordEmoji.FromName(ctx.Client, ":four:"))),
            });

            //======================================//

            DiscordMessage currentMessage = await ctx.Channel.SendMessageAsync(actionMenuMessage);

            var buttonsResult = await currentMessage.WaitForButtonAsync(ctx.User);
            if (!buttonsResult.TimedOut)
            {
                await buttonsResult.Result.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);

                if (buttonsResult.Result.Id == "all_action")
                {
                    await SendStartMessage();
                    await MinigameInstanceClient.GetInstanceAsync<HGMinigame>().Result.CreateNewSessionAsync(ctx, new HGMinigame(), sessionBuilder, new List<DiscordMember>(ctx.Guild.Members.Values.ToList()));
                }
                else if (buttonsResult.Result.Id == "members_action")
                {
                    await SendStartMessage();
                    await MinigameInstanceClient.GetInstanceAsync<HGMinigame>().Result.CreateNewSessionAsync(ctx, new HGMinigame(), sessionBuilder, new List<DiscordMember>(ctx.Guild.Members.Values.Where(x => !x.IsBot).ToList()));
                }
                else if (buttonsResult.Result.Id == "bots_action")
                {
                    await SendStartMessage();
                    await MinigameInstanceClient.GetInstanceAsync<HGMinigame>().Result.CreateNewSessionAsync(ctx, new HGMinigame(), sessionBuilder, new List<DiscordMember>(ctx.Guild.Members.Values.Where(x => x.IsBot).ToList()));
                }
                else if (buttonsResult.Result.Id == "npcs_action")
                {
                    int amount = 0;
                    await ctx.Channel.SendMessageAsync($"<@{ctx.User.Id}> **Quantos npcs estarão participando?** \n" +
                                                       $"*(Digite apenas numeros)*");

                    InteractivityResult<DiscordMessage> result = ctx.Channel.GetNextMessageAsync(ctx.User, TimeSpan.FromSeconds(20)).Result;
                    if (!result.TimedOut)
                    {
                        try
                        {
                            amount = int.Parse(result.Result.Content);
                        }
                        catch (Exception)
                        {
                            await ctx.Channel.SendMessageAsync("**OH NÃO, PARECE QUE ALGO DEU ERRADO** \n" +
                                                              $"<@{ctx.User.Id}> **Verifique se você escreveu corretamente o numero, caso esteja em duvida, veja se você passou por estas condições:** \n" +
                                                              $"● Não digite numeros Negativos ou Valores absurdos. \n" +
                                                              $"● Não digite palavras. \n" +
                                                              $"● Não digite espaços. \n" +
                                                              $"● Não envie arquivos anexados.");

                            return;
                        }
                    }

                    await SendStartMessage();
                    await MinigameInstanceClient.GetInstanceAsync<HGMinigame>().Result.CreateNewSessionAsync(ctx, new HGMinigame(), sessionBuilder, amount);
                }
                else if (buttonsResult.Result.Id == "select_action")
                {
                    List<DiscordUser> members = new();
                    await ctx.Channel.SendMessageAsync($"**<@{ctx.User.Id}> MENCIONE OS MEMBROS QUE IRÃO PARTICIPAR DO JOGO.** \n" +
                                                       $"*(Apenas menções)*");

                    InteractivityResult<DiscordMessage> result = ctx.Channel.GetNextMessageAsync(ctx.User, TimeSpan.FromSeconds(20)).Result;
                    if (!result.TimedOut)
                    {
                        try
                        {
                            members = new List<DiscordUser>(result.Result.MentionedUsers);
                        }
                        catch (Exception)
                        {
                            await ctx.Channel.SendMessageAsync("**OH NÃO, PARECE QUE ALGO DEU ERRADO** \n" +
                                                              $"<@{ctx.User.Id}> **Verifique se você escreveu corretamente, caso esteja em duvida, veja se você passou por estas condições:** \n" +
                                                              $"● Digite apenas menções. \n" +
                                                              $"● Deixe um pequeno espaço entre as menções.");
                        }

                        await SendStartMessage();
                        await MinigameInstanceClient.GetInstanceAsync<HGMinigame>().Result.CreateNewSessionAsync(ctx, new HGMinigame(), sessionBuilder, new List<DiscordUser>(members));
                    }
                }
            }

            //======================================//

            async Task SendStartMessage()
            {
                await ctx.Channel.SendMessageAsync(":crossed_swords: ● **OS JOGOS VORAZES ESTÃO COMEÇANDO** ● :crossed_swords: \n" +
                                                   "Aguarde enquanto eu Organizo o Evento!");
            }
        }
    }
}
