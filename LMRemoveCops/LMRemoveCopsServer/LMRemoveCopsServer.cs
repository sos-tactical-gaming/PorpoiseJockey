using System;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace LMRemoveCopsServer
{
    public class LMRemoveCopsServer : BaseScript
    {
        private readonly float radius = 600f;

        public LMRemoveCopsServer()
        {
            Tick += OnTick;
        }

        private async Task OnTick()
        {
            await Delay(100);

            PlayerList players = new PlayerList();
            foreach (Player player in players)
                TriggerClientEvent(player, "LMRemoveCops:removeCops", radius);
        }
    }
}