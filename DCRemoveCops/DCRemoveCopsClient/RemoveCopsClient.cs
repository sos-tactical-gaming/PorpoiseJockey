using System;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace DC
{
    public class RemoveCopsClient : BaseScript
    {
        public RemoveCopsClient()
        {
            EventHandlers[RemoveCopsShared.EVENT_REMOVE_COPS] += new Action<float>(OnRemoveCops);
        }

        private void OnRemoveCops(float radius)
        {
            Vector3 localPos = API.GetEntityCoords(API.PlayerPedId(), false);
            API.ClearAreaOfCops(localPos.X, localPos.Y, localPos.Z, radius, 0);
        }
    }
}
