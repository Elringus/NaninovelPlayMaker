using HutongGames.PlayMaker;

namespace Naninovel.PlayMaker
{
    [ActionCategory("Naninovel")]
    public class PlayScript : FsmStateAction
    {
        [Tooltip("The name of the script play.")]
        [UIHint(UIHint.FsmString)]
        public FsmString ScriptName;

        [Tooltip("Event sent when the script has started playing.")]
        public FsmEvent DoneEvent;

        public override void Reset ()
        {
            DoneEvent = null;
        }

        public override void OnEnter ()
        {
            if (!Engine.IsInitialized)
            {
                UnityEngine.Debug.LogError("Can't play script: Naninovel engine is not initialized.");
                Finish();
                return;
            }

            PreloadAndPlayScriptAsync();
        }

        private async void PreloadAndPlayScriptAsync ()
        {
            var player = Engine.GetService<ScriptPlayer>();
            await player.PreloadAndPlayAsync(ScriptName.Value);

            Fsm.Event(DoneEvent);
            Finish();
        }
    }
}
