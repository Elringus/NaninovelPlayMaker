using HutongGames.PlayMaker;

namespace Naninovel.PlayMaker
{
    [ActionCategory("Naninovel")]
    public class ExecuteCommand : FsmStateAction
    {
        [Tooltip("The text of the script command to execute.")]
        [UIHint(UIHint.FsmString)]
        public FsmString CommandText;

        public override void Reset ()
        {
            CommandText = null;
        }

        public override void OnEnter ()
        {
            DoAsync();
        }

        private async void DoAsync ()
        {
            if (string.IsNullOrEmpty(CommandText.Value)) { Finish(); return; }

            var scriptLine = new CommandScriptLine(string.Empty, 0, CommandText.Value, null);
            if (scriptLine is null) { Finish(); return; }

            var command = Commands.Command.FromScriptLine(scriptLine);
            if (command is null) { Finish(); return; }

            if (command.ShouldExecute)
                await command.ExecuteAsync();

            Finish();
        }
    }
}
