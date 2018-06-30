using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace DC
{
    public class DisableDispatchClient : BaseScript
    {
        public DisableDispatchClient()
        {
            Tick += OnTick;
        }

        private void DisableDispatch()
        {
            for (int i = 1; i <= 15; ++i)
                API.EnableDispatchService(i, false);
            API.SetMaxWantedLevel(0);
        }

        private async Task OnTick()
        {
            await Delay(0);
            if (API.NetworkIsPlayerActive(API.PlayerId()))
            {
                DisableDispatch();
                Tick -= OnTick;
                return;
            }
        }
    }
}
