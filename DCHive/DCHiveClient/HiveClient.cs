using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace DC
{
    public class HiveClient : BaseScript
    {
        private readonly Action<ExpandoObject> onRegisterUserDelegate = null;

        public HiveClient()
        {
            onRegisterUserDelegate = new Action<ExpandoObject>(OnRegisterUserResponse);
            EventHandlers[HiveShared.EventRegisterUserResponse] += onRegisterUserDelegate;

            Tick += OnTick;
        }

        private async Task OnTick()
        {
            await Delay(0);

            if (API.NetworkIsPlayerActive(API.PlayerId()))
            {
                RegisterUser();
                Tick -= OnTick;
                return;
            }
        }

        private void RegisterUser()
        {
            TriggerServerEvent(HiveShared.EventRegisterUser);
        }

        private void OnRegisterUserResponse(ExpandoObject user)
        {
            EventHandlers[HiveShared.EventRegisterUserResponse] -= onRegisterUserDelegate;
        }
    }
}
