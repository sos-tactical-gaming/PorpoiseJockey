using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace DC
{
    public class StreetNameClient : BaseScript
    {
        private readonly CitizenFX.Core.UI.Text streetText = null;
        private readonly CitizenFX.Core.UI.Text zoneText = null;

        public StreetNameClient()
        {
            streetText = new CitizenFX.Core.UI.Text("", new System.Drawing.PointF(1280f * 0.5f, 8f), 0.64f)
            {
                Font = CitizenFX.Core.UI.Font.ChaletComprimeCologne,
                Alignment = CitizenFX.Core.UI.Alignment.Center,
                Outline = true
            };
            zoneText = new CitizenFX.Core.UI.Text("", new System.Drawing.PointF(1280f * 0.5f, streetText.Position.Y + 28f), 0.54f)
            {
                Font = CitizenFX.Core.UI.Font.ChaletComprimeCologne,
                Alignment = CitizenFX.Core.UI.Alignment.Center,
                Outline = true
            };

            Tick += OnTick;
        }

        private void UpdateStreetText()
        {
            uint street = 0, crossingRoad = 0;
            Vector3 localPos = API.GetEntityCoords(API.PlayerPedId(), false);
            API.GetStreetNameAtCoord(localPos.X, localPos.Y, localPos.Z, ref street, ref crossingRoad);
            string crossingRoadName = API.GetStreetNameFromHashKey(crossingRoad);
            streetText.Caption = "~y~" + API.GetStreetNameFromHashKey(street) + "~w~" + (crossingRoadName.Length > 0 ? " | " + crossingRoadName : "");
            streetText.Draw();
        }

        private void UpdateZoneText()
        {
            Vector3 localPos = API.GetEntityCoords(API.PlayerPedId(), false);
            if (Zone.Names.TryGetValue(API.GetNameOfZone(localPos.X, localPos.Y, localPos.Z), out string name))
                zoneText.Caption = name;
            zoneText.Draw();
        }

        private async Task OnTick()
        {
            UpdateStreetText();
            UpdateZoneText();          
        }
    }
}
