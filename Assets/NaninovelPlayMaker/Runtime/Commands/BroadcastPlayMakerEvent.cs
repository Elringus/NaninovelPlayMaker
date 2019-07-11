using Naninovel.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace Naninovel.PlayMaker
{
    /// <summary>
    /// Broadcasts a PlayMaker event with the provided name.
    /// When neither `fsm` nor `object` params are specified, the event will be broadcasted globally to all the active FSMs on scene.
    /// </summary>
    [CommandAlias("playmaker")]
    public class BroadcastPlayMakerEvent : Command
    {
        /// <summary>
        /// Name of the event to broadcast.
        /// </summary>
        [CommandParameter(NamelessParameterAlias)]
        public string EventName { get => GetDynamicParameter<string>(null); set => SetDynamicParameter(value); }
        /// <summary>
        /// Names of FSMs for which to broadcast the event.
        /// </summary>
        [CommandParameter("fsm", true)]
        public string[] FsmNames { get => GetDynamicParameter<string[]>(null); set => SetDynamicParameter(value); }
        /// <summary>
        /// Names of game objects for which to broadcast the event. The objects should have an FSM component attached.
        /// </summary>
        [CommandParameter("object", true)]
        public string[] GameObjectNames { get => GetDynamicParameter<string[]>(null); set => SetDynamicParameter(value); }

        public override Task ExecuteAsync ()
        {
            if (FsmNames is null && GameObjectNames is null)
            {
                PlayMakerFSM.BroadcastEvent(EventName);
                return Task.CompletedTask;
            }

            var fsmNames = FsmNames?.ToList();
            var objectNames = GameObjectNames?.ToList();
            foreach (var fsm in PlayMakerFSM.FsmList)
            {
                if (objectNames != null && !objectNames.Contains(fsm.gameObject.name)) continue;
                if (fsmNames != null && !fsmNames.Contains(fsm.FsmName)) continue;

                fsm.SendEvent(EventName);
            }

            return Task.CompletedTask;
        }

        public override Task UndoAsync () => Task.CompletedTask;
    }
}
