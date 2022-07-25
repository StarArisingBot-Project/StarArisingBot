using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using System.Collections.Generic;

namespace StarArisingBot.Core.Modules.Help
{
    internal class HelpModule : BaseHelpFormatter
    {
        public DiscordEmbedBuilder helpEmbed = null;
        private readonly CommandContext ctx = null;

        public HelpModule(CommandContext ctx) : base(ctx)
        {
            helpEmbed = new DiscordEmbedBuilder().WithDescription("**:jigsaw: │ Menu de ajuda │ :jigsaw:** \n" +
                                                                  $"\n**:star: • Precisando de ajuda {ctx.User.Username}? Aqui esta uma lista completa de todos os meus comandos e geradores! • :star:** \n\n")
                                                 .WithColor(DiscordColor.Purple);

            this.ctx = ctx;
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

    public class CodeHelpBlock
    {
        public string GroupTitle { get; set; }
        public List<string> Commands = new List<string>();

        public int Position { get; set; }

        public string ReturnBlockCode()
        {
            string commandsInfo = "";
            foreach (string command in Commands)
            {
                commandsInfo += $"{command} \n";
            }

            return $"**{GroupTitle}** \n" +
                   $"{commandsInfo} \n";
        }
    }
}
