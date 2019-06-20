using Naninovel.Commands;
using System.Threading.Tasks;

namespace Naninovel.PlayMaker
{
    /// <summary>
    /// Broadcasts a global PlayMaker event with the provided name.
    /// </summary>
    [CommandAlias("playmaker")]
    public class BroadcastPlayMakerEvent : Command
    {
        /// <summary>
        /// Name of the event to broadcast.
        /// </summary>
        [CommandParameter(NamelessParameterAlias)]
        public string EventName { get => GetDynamicParameter<string>(null); set => SetDynamicParameter(value); }

        public override Task ExecuteAsync ()
        {
            PlayMakerFSM.BroadcastEvent(EventName);

            return Task.CompletedTask;
        }

        public override Task UndoAsync () => Task.CompletedTask;
    }
}
