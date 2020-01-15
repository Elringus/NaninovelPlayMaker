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

            var commandBodyText = CommandText.Value.GetAfterFirst(CommandScriptLine.IdentifierLiteral).Trim();
            var command = Commands.Command.FromScriptText(string.Empty, 0, 0, commandBodyText, out var errors);
            if (command is null || !string.IsNullOrEmpty(errors)) { Finish(); return; }

            if (command.ShouldExecute)
                await command.ExecuteAsync();

            Finish();
        }
    }
}
