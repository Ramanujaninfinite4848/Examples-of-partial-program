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

            // Create building walls using available methods
            CreateBuildingStructure(map, buildingCorners, 5f, "137");

  
            var docs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var modPath = Path.Combine(docs, "Euro Truck Simulator 2/mod/user_map/map/");
            Directory.CreateDirectory(modPath);
            map.Save(modPath);
        }

        static void CreateBuildingStructure(Map map, List<Vector3> footprint, float height, string buildingStyle)
        {
            // Create nodes at each corner
            var nodes = footprint.ConvertAll(pos => map.AddNode(new Vector3(pos.X, height, pos.Z)));

            // Connect nodes to create building outline
            for (int i = 0; i < nodes.Count; i++)
            {
                int nextIndex = (i + 1) % nodes.Count;
                
                // Create building segments (using available methods)
                var segment = Building.Add(
                    map,
                    nodes[i].Position,
                    nodes[nextIndex].Position,
                    buildingStyle
                );
                
                // Set common properties
                segment.Collision = true;
                segment.ViewDistance = 400;
            }
        }
    }
}