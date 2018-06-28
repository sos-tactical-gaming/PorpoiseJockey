using System;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace LM
{
    public class RemoveCopsServer : BaseScript
    {
        private readonly float radius = 600f;

        public RemoveCopsServer()
        {
            Tick += OnTick;
        }

        private async Task OnTick()
        {
            await Delay(100);

            PlayerList players = new PlayerList();
            foreach (Player player in players)
                TriggerClientEvent(player, "LM:RemoveCops", radius);
        }
    }
}