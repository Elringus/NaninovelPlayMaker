using HutongGames.PlayMaker;

namespace Naninovel.PlayMaker
{
    [ActionCategory("Naninovel")]
    public class GetLocalizationManagerStatus : FsmStateAction
    {
        [Tooltip("Whether the game is currently running in the default locale.")]
        [UIHint(UIHint.Variable)]
        public FsmBool UsingDefaulLocale;

        [UIHint(UIHint.Variable)]
        public FsmString DefaultLocale;

        [UIHint(UIHint.Variable)]
        public FsmString SelectedLocale;

        public override void Reset ()
        {
            UsingDefaulLocale = null;
            DefaultLocale = null;
            SelectedLocale = null;
        }

        public override void OnEnter ()
        {
            var localizationManager = Engine.GetService<LocalizationManager>();
            if (localizationManager is null) { Finish(); return; }

            UsingDefaulLocale.Value = localizationManager.UsingDefaulLocale;
            DefaultLocale.Value = localizationManager.DefaultLocale;
            SelectedLocale.Value = localizationManager.SelectedLocale;

            Finish();
        }
    }
}
