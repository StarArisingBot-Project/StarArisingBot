using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using System.Collections.Generic;

namespace StarArisingBot.Core.Modules.Help
{
    internal class HelpModule : BaseHelpFormatter
    {
        public readonly DiscordEmbedBuilder helpEmbed = null;
        private readonly CommandContext context = null;

        public HelpModule(CommandContext ctx) : base(ctx)
        {
            helpEmbed = new DiscordEmbedBuilder().WithDescription("**:jigsaw: │ Menu de ajuda │ :jigsaw:** \n" +
                                                                  $"\n**:star: • Precisando de ajuda {ctx.User.Username}? Aqui esta uma lista completa de todos os meus comandos e geradores! • :star:** \n\n")
                                                 .WithColor(DiscordColor.Purple);

            context = ctx;
        }

        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> subCommands)
        {
            return this;
        }

        public override BaseHelpFormatter WithCommand(Command command)
        {
            return this;
        }
        public override CommandHelpMessage Build()
        {
            return new CommandHelpMessage(null, helpEmbed);
        }
    }
}
