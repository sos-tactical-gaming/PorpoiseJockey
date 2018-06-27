using System;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;

namespace LMRemoveCopsClient
{
    public class LMRemoveCopsClient : BaseScript
    {
        public LMRemoveCopsClient()
        {
            EventHandlers["LMRemoveCops:removeCops"] += new Action<float>(OnRemoveCops);
        }

        private void OnRemoveCops(float radius)
        {
            int player = API.GetPlayerPed(-1);
            Vector3 localPos = API.GetEntityCoords(player, true);
            API.ClearAreaOfCops(localPos.X, localPos.Y, localPos.Z, radius, 0);
        }
    }
}