using HutongGames.PlayMaker;

namespace Naninovel.PlayMaker
{
    [ActionCategory("Naninovel")]
    public class GetCustomVariable : FsmStateAction
    {
        [Tooltip("The name of the variable to get.")]
        [UIHint(UIHint.FsmString)]
        public FsmString VariableName;

        [Tooltip("The retrieved value of the variable.")]
        [UIHint(UIHint.Variable)]
        public FsmString VariableValue;

        public override void Reset ()
        {
            VariableName = null;
            VariableValue = null;
        }

        public override void OnEnter ()
        {
            var customVarManager = Engine.GetService<CustomVariableManager>();
            if (customVarManager is null) { Finish(); return; }

            VariableValue.Value = customVarManager.GetVariableValue(VariableName.Value);
        }
    }
}
