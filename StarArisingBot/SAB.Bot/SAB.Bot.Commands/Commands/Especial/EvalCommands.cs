using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SAB.Bot.Commands
{
    [Hidden]
    public class EvalCommands : BaseCommandModule
    {
        [Command("EvalHelp")]
        public async Task EvalHelp(CommandContext ctx, [RemainingText] string code)
        {


            HttpClient client = new HttpClient();
            HttpResponseMessage DsharpPlusSite = await client.GetAsync("https://dsharpplus.github.io/api/index.html");
        }

        [Command("Script"), Aliases("Eval", "E", "S")]
        public async Task Script(CommandContext ctx, [RemainingText] string expression)
        {
            if (ctx.Member.Id == 642435831239409668 || ctx.Member.Id == 477534823011844120)
            {
                try
                {
                    ScriptOptions scriptOptions = ScriptOptions.Default
                        .AddReferences(typeof(DiscordMessage).Assembly).AddImports("DSharpPlus")
                        .AddReferences(typeof(CommandContext).Assembly).AddImports("DSharpPlus.CommandsNext")
                        .AddReferences(typeof(ReadyEventArgs).Assembly).AddImports("DSharpPlus.EventArgs")
                        .AddReferences(typeof(DiscordMessage).Assembly).AddImports("DSharpPlus.Entities")
                        .AddReferences(typeof(Random).Assembly).AddImports("System")
                        .AddReferences(typeof(List<object>).Assembly).AddImports("System.Collections.Generic")
                        .AddReferences(typeof(Task).Assembly).AddImports("System.Threading.Tasks");

                    object result = Execute(expression, scriptOptions, ctx);
                    await ctx.Channel.SendMessageAsync($"``` {result} ```");
                }
                catch (Exception e)
                {
                    await ctx.RespondAsync($"Algo deu errado! \n Descrição do Erro: {e.Message}");
                }
            }
            else
            {
                await ctx.RespondAsync(":star: ⦁ **Este comando é privado amiguinho!**");
            }
        }
        public static object Execute(string code, ScriptOptions scriptOptions, CommandContext ctx)
        {
            Global variables = new Global(ctx.Message, ctx.Client, ctx);

            Script<object> script = CSharpScript.Create(code, scriptOptions, typeof(Global));
            script.Compile();

            Task<ScriptState<object>> result = script.RunAsync(variables);

            if (result.Result.ReturnValue != null && !string.IsNullOrEmpty(result.Result.ReturnValue.ToString()))
            {
                return result.Result.ReturnValue.ToString();
            }
            return null;
        }
    }

    public class Global
    {
        public DiscordMessage Message;
        public DiscordChannel Channel;
        public DiscordGuild Guild;
        public DiscordUser User;
        public DiscordMember Member;
        public CommandContext ctx;

        public Random Random = new Random();

        public DiscordClient Client;

        public Global()
        {

        }

        public Global(DiscordMessage msg, DiscordClient client, CommandContext ctx)
        {
            Client = client;

            Message = msg;
            Channel = msg.Channel;
            Guild = Channel.Guild;
            User = Message.Author;
            this.ctx = ctx;
        }

        public static string Help()
        {
            return $"Todos os comandos de codigos: \n " +
                   $"Servers(ctx) >> Retorna todos os servidores que estou \n";
        }

        public static string Servers(CommandContext ctx)
        {
            string guildsList = "";

            foreach (KeyValuePair<ulong, DiscordGuild> guild in ctx.Client.Guilds)
            {
                guildsList += $"{ctx.Client.GetGuildAsync(guild.Key).Result.Name} \n";
            }

            return guildsList;
        }
    }
}
