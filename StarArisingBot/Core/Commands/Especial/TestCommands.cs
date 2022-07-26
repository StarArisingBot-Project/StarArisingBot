using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;

namespace StarArisingBot.Core.Commands
{
    [Hidden]
    internal class TestCommands : BaseCommandModule
    {
        public Random random = new Random();

        [Command("Matriz"), Hidden]
        public async Task Matriz(CommandContext ctx, int number)
        {
            #region blocos para o mapa
            string terra = DiscordEmoji.FromName(ctx.Client, ":brown_square:").GetDiscordName().ToString();
            string grama = DiscordEmoji.FromName(ctx.Client, ":green_square:").GetDiscordName().ToString();
            string agua = DiscordEmoji.FromName(ctx.Client, ":blue_square:").GetDiscordName().ToString();
            string loot = DiscordEmoji.FromName(ctx.Client, ":orange_square:").GetDiscordName().ToString();
            string enemy = DiscordEmoji.FromName(ctx.Client, ":red_square:").GetDiscordName().ToString();
            string wall = DiscordEmoji.FromName(ctx.Client, ":black_large_square:").GetDiscordName().ToString();
            #endregion

            int[,] matriz = new int[number, number];
            string matrizVisual = "";

            try
            {
                for (int x = 0; x < matriz.GetUpperBound(0); x++)
                {
                    for (int y = 0; y < matriz.GetUpperBound(1); y++)
                    {
                        matrizVisual += (y == 0 ? "" : " ") + matriz[x, y];
                    }
                    matrizVisual += "\n";
                }
                await ctx.RespondAsync(matrizVisual);
            }
            catch (Exception e)
            {
                await ctx.RespondAsync($"**Algo deu errado!** \n *Erro:* {e.Message}");
            }
        }

        [Command("Reação"), Hidden]
        public async Task Reaction(CommandContext ctx)
        {
            await ctx.Message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":star:"));
        }

        [Command("Porcentagem"), Hidden]
        public async Task Porcetagem(CommandContext ctx, params DiscordMember[] members)
        {
            int value = ctx.Guild.Members.Count / members.Length;
            int porcentagem = value * 100;

            await ctx.RespondAsync($"Os usuarios selecionados representam {porcentagem}% do servidor \n\n *{members.Length}/{ctx.Guild.Members.Count}*");
        }

        [Command("MedidorPenis"), Aliases("MedidorDePenis"), Hidden]
        public async Task MedidorPenis(CommandContext ctx)
        {
            int size = random.Next(1, 40);
            string fraseExtra = "";

            string[] more40 = { "CARALHO, ARROMBA MEU CU PORRA FILHA DA PUTA", "COMO TU TEM ISSO E NÃO FOI PRO PORNO FILHA DA PUTA", "brabissimo" };
            string[] more30 = { "PUTA QUE PARIU, MALUCO TEM O DRIPE NO PAU", "Já da pra ir pro pau awards lá", "Já da pra atirar goza a 30kg na cara do amigo" };
            string[] more20 = { "puta que pariu que pinto", "Gostei", "já arromba uma parede", "Só vapo" };
            string[] more10 = { "ta desandando mas ta bom pra uma vadiakkkkkk", "KKKKKK pelo menos tu não tem micro penis", "Da pro gasto desgraçakkkk" };
            string[] less10 = { "KKKKKKKK VOCÊ CHAMA ISSO DE PAU?KKKKKKKKKKKKKKK", "KKKKKKKKKKKK ah não, cara desiste", "Tu tem micro penis bro, nem fala comigo", "Tamo junto parceirokkkk :pensive:", "Nenhuma muie vai vim mas se compra uma boneca se couber" };

            switch (size)
            {
                case >= 40:
                    fraseExtra = more40[random.Next(0, more40.Length)];
                    break;

                case >= 30:
                    fraseExtra = more30[random.Next(0, more30.Length)];
                    break;

                case > 20:
                    fraseExtra = more20[random.Next(0, more20.Length)];
                    break;

                case >= 10:
                    fraseExtra = more10[random.Next(0, more10.Length)];
                    break;

                case < 10:
                    fraseExtra = less10[random.Next(0, less10.Length)];
                    break;

            }

            await ctx.RespondAsync($"{ctx.Member.DisplayName} medi seu pinto aqui e descobri que se tem **{size}cm**, {fraseExtra}");
        }

        [Command("MedidorGay"), Hidden]
        public async Task MedidorGay(CommandContext ctx)
        {
            int gayMeter = random.Next(0, 100);
            string gaySentenceSelected = string.Empty;

            string[] gaySentence0 =
            {
                "Macho Alfa",
                "Agride minorias",
                "Bate em mulher",
                "Bate em víado",
                "Come e cospe no chão",
                "Manda a muié lavar a louça",
                "Ogro",
            };
            string[] gaySentence10 =
            {
                "Homem",
                "Não se deixa abalar",
                "Pensa em gays de vez em quando"
            };
            string[] gaySentence20 =
            {
                "As vezes dá uns deslizes",
                "Já se viu gay mas não admite",
            };
            string[] gaySentence30 =
            {
                "Se finge",
                "Bate pensando nele",
                "Comeria um gay"
            };
            string[] gaySentence40 =
            {
                "Fala que é hetero mas daria pro amigo",
                "Tendencias gays",
                "Quadro grave",
            };
            string[] gaySentence50 =
            {
                "Bi",
                "Caiu na vila, peixe fuzila",
                "O que vier ta bom",
                "🌈",
            };
            string[] gaySentence60 =
            {
                "Como da mesma fruta das putas",
                "Gosta de 24",
                "Agora todo mundo é gay",
                "Bixisse sobre controle",
            };
            string[] gaySentence70 =
            {
                "Bicha",
                "Viadasso",
            };
            string[] gaySentence80 =
            {
                "Dá sem olhar",
                "Putinha",
                "Muito gay"
            };
            string[] gaySentence90 =
            {
                "Gazela saltitante",
                "Gosta de uma metida com força",
                "Bixa assumida"
            };
            string[] gaySentence100 =
            {
                "Bixona sem cura",
                "Gay",
            };

            switch (gayMeter)
            {
                case 100:
                    gaySentenceSelected = gaySentence100[random.Next(0, gaySentence100.Length - 1)];
                    break;

                case > 90:
                    gaySentenceSelected = gaySentence90[random.Next(0, gaySentence90.Length - 1)];
                    break;

                case > 80:
                    gaySentenceSelected = gaySentence80[random.Next(0, gaySentence80.Length - 1)];
                    break;

                case > 70:
                    gaySentenceSelected = gaySentence70[random.Next(0, gaySentence70.Length - 1)];
                    break;

                case > 60:
                    gaySentenceSelected = gaySentence60[random.Next(0, gaySentence60.Length - 1)];
                    break;

                case > 50:
                    gaySentenceSelected = gaySentence50[random.Next(0, gaySentence50.Length - 1)];
                    break;

                case > 40:
                    gaySentenceSelected = gaySentence40[random.Next(0, gaySentence40.Length - 1)];
                    break;

                case > 30:
                    gaySentenceSelected = gaySentence30[random.Next(0, gaySentence30.Length - 1)];
                    break;

                case > 20:
                    gaySentenceSelected = gaySentence20[random.Next(0, gaySentence20.Length - 1)];
                    break;

                case > 10:
                    gaySentenceSelected = gaySentence10[random.Next(0, gaySentence10.Length - 1)];
                    break;

                case >= 0:
                    gaySentenceSelected = gaySentence0[random.Next(0, gaySentence0.Length - 1)];
                    break;
            }

            await ctx.RespondAsync($"<@{ctx.User.Id}> pelos meus cáculos, você é **{gayMeter}%** gay :rainbow:.\n" +
                                   $"{gaySentenceSelected}");
        }

        [Command("MedidorGay")]
        public async Task MedidorGay(CommandContext ctx, DiscordMember member)
        {
            int gayMeter = random.Next(0, 100);
            string gaySentenceSelected = string.Empty;

            string[] gaySentence0 =
            {
                "Macho Alfa",
                "Agride minorias",
                "Bate em mulher",
                "Bate em víado",
                "Come e cospe no chão",
                "Manda a muié lavar a louça",
                "Ogro",
            };
            string[] gaySentence10 =
            {
                "Homem",
                "Não se deixa abalar",
                "Pensa em gays de vez em quando"
            };
            string[] gaySentence20 =
            {
                "As vezes dá uns deslizes",
                "Já se viu gay mas não admite",
            };
            string[] gaySentence30 =
            {
                "Se finge",
                "Bate pensando nele",
                "Comeria um gay"
            };
            string[] gaySentence40 =
            {
                "Fala que é hetero mas daria pro amigo",
                "Tendencias gays",
                "Quadro grave",
            };
            string[] gaySentence50 =
            {
                "Bi",
                "Caiu na vila, peixe fuzila",
                "O que vier ta bom",
                "🌈",
            };
            string[] gaySentence60 =
            {
                "Como da mesma fruta das putas",
                "Gosta de 24",
                "Agora todo mundo é gay",
                "Bixisse sobre controle",
            };
            string[] gaySentence70 =
            {
                "Bicha",
                "Viadasso",
            };
            string[] gaySentence80 =
            {
                "Dá sem olhar",
                "Putinha",
                "Muito gay"
            };
            string[] gaySentence90 =
            {
                "Gazela saltitante",
                "Gosta de uma metida com força",
                "Bixa assumida"
            };
            string[] gaySentence100 =
            {
                "Bixona sem cura",
                "Gay",
            };

            switch (gayMeter)
            {
                case 100:
                    gaySentenceSelected = gaySentence100[random.Next(0, gaySentence100.Length - 1)];
                    break;

                case > 90:
                    gaySentenceSelected = gaySentence90[random.Next(0, gaySentence90.Length - 1)];
                    break;

                case > 80:
                    gaySentenceSelected = gaySentence80[random.Next(0, gaySentence80.Length - 1)];
                    break;

                case > 70:
                    gaySentenceSelected = gaySentence70[random.Next(0, gaySentence70.Length - 1)];
                    break;

                case > 60:
                    gaySentenceSelected = gaySentence60[random.Next(0, gaySentence60.Length - 1)];
                    break;

                case > 50:
                    gaySentenceSelected = gaySentence50[random.Next(0, gaySentence50.Length - 1)];
                    break;

                case > 40:
                    gaySentenceSelected = gaySentence40[random.Next(0, gaySentence40.Length - 1)];
                    break;

                case > 30:
                    gaySentenceSelected = gaySentence30[random.Next(0, gaySentence30.Length - 1)];
                    break;

                case > 20:
                    gaySentenceSelected = gaySentence20[random.Next(0, gaySentence20.Length - 1)];
                    break;

                case > 10:
                    gaySentenceSelected = gaySentence10[random.Next(0, gaySentence10.Length - 1)];
                    break;

                case >= 0:
                    gaySentenceSelected = gaySentence0[random.Next(0, gaySentence0.Length - 1)];
                    break;
            }

            await ctx.RespondAsync($"<@{member.Id}> pelos meus cáculos, você é **{gayMeter}%** gay :rainbow:.\n" +
                                   $"{gaySentenceSelected}");
        }
    }
}