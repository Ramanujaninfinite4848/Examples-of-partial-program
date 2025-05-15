using TruckLib.ScsMap;
using System.Numerics;
using System;
using System.IO;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {

            Map map = Map.Open(@"C:\Users\worker\Documents\Euro Truck Simulator 2\mod\user_map\map\example.mbd");
            map.NormalScale = 1;
            map.CityScale = 1;

            bool exists = map.MapItems.TryGetValue(0x521CD80FA4000001, out MapItem item);

            var allMovers = map.MapItems.Where(x => x.Value is Mover);
            //var road = ?;
            var road = allMovers;

            PolylineItem start = road.FindFirstItem();
            PolylineItem end = road.FindLastItem();

            road.Right.Look = "ger_1";
            road.Right.Variant = "broken_de";
            road.Right.LeftEdge = "ger_sh_15";
            road.Right.RightEdge = "ger_sh_15";

            foreach (var side in new[]{road.Left, road.Right})
            {

                side.Terrain.QuadData.BrushMaterials[0] = new Material("34"); // "grass_ger_main"
                side.Terrain.Profile = "profile12"; // "hills2"
                side.Terrain.Noise = TerrainNoise.Percent0;
                side.Terrain.Coefficient = 0.5f;

                side.Vegetation[0].Name = "v2_1ger"; // "ger - mixed forest"
                side.Vegetation[0].Density = 200;
                side.Vegetation[0].From = 15;
                side.Vegetation[0].To = 80;

  
                side.Models[0].Name = "ch_2y07d"; // "reflective post de"
                side.Models[0].Distance = 50;
                side.Models[0].Offset = 6;
            }


            PolylineItem.CreateItemsAlongPath(start, end, 10f, (container, point) =>
            {
                Model model = Model.Add(container, point.Position, "greece_29000", "var1", "default");
                model.Node.Rotation = point.Rotation;
                return [model];
            });
          
            // Save the map.
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var userMap = Path.Combine(documents, "Euro Truck Simulator 2/mod/user_map/map/");
            map.Save(userMap, true);

            // Remember to recalculate (Map > Recompute map) after loading it in the editor for the first time.
        }
    }
}
