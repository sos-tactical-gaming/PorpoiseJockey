using System.Threading.Tasks;
using CitizenFX.Core;

namespace DC
{
    public class RemoveCopsServer : BaseScript
    {
        private readonly float removeRadius;
        private readonly int pollRate;

        public RemoveCopsServer()
        {
            ConfigParser config = new ConfigParser("config.ini");
            removeRadius = config.GetFloatValue("removeRadius", 600f);
            pollRate = config.GetIntValue("pollRate", 100);

            Tick += OnTick;
        }

        private async Task OnTick()
        {
            await Delay(pollRate);

            PlayerList players = new PlayerList();
            foreach (Player player in players)
                TriggerClientEvent(player, RemoveCopsShared.EVENT_REMOVE_COPS, removeRadius);
        }
    }
}