using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;

namespace SAB.Bot.Commands
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
    }
}