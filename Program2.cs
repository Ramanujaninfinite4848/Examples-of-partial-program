using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using TruckLib;
using TruckLib.ScsMap;

namespace BuildingCreator
{
    class Program
    {
        static void Main(string[] args)
        {

            var map = new Map("example");
            
            // Define building footprint (rectangular)
            var buildingCorners = new List<Vector3>
            {
                new Vector3(0, 0, 0),    // Corner 1
                new Vector3(10, 0, 0),   // Corner 2
                new Vector3(10, 0, 8),   // Corner 3
                new Vector3(0, 0, 8)     // Corner 4
            };

            // Create building with proper method
            CreateBuilding(map, buildingCorners, "scheme76");

            var docs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var modPath = Path.Combine(docs, "Euro Truck Simulator 2/mod/user_map/map/");
            Directory.CreateDirectory(modPath);
            map.Save(modPath);
        }

        static void CreateBuilding(Map map, List<Vector3> footprint, string buildingScheme)
        {
            // Create building between two opposite corners
            var building = Buildings.Add(
                map,
                footprint[0],  // Start position (bottom-left)
                footprint[2],  // End position (top-right)
                buildingScheme
            );

            // Set available properties
            building.ViewDistance = 400;
            building.Collision = true;
            
            // Note: Height is automatically determined by the building scheme

        }
    }
}
