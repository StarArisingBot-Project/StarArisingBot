using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace StarArisingBot.Core.Modules.Help
{
    [Hidden]
    public class HelpGenerators : BaseCommandModule
    {
        [Command("HelpGenerator"), Aliases("HelpGenerators", "GeratorHelp", "GeneratorsHelp")]
        public async Task HelpGenerator(CommandContext ctx)
        {
            DiscordEmbedBuilder EmbedHelp = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Red,
                Title = ":scroll: • AJUDA COM GERADORES • :scroll:",
                Description = $"**:potted_plant: • Lista de Geradores • :potted_plant:** \n\n" +
                              $":mushroom: • **Geradores Gerais** • :mushroom: \n" +
                              $"**{ctx.Prefix}NpcGenerator ➔** Gere um NPC básico com atributos simples. \n\n" +

                              $":squid: • **Geradores para Call Of Cthulhu (RPG de mesa) • :squid:** \n" +
                              $"**{ctx.Prefix}MagicGeneratorCoC ➔** Gere uma magia para o RPG de mesa Call Of Cthulhu. \n" +
                              $"**{ctx.Prefix}FichaCreatorCoC➔** Gere uma ficha de personagem para o RPG de mesa Call Of Cthulhu. \n\n",
            };

            await ctx.RespondAsync(EmbedHelp);
        }
    }
}
