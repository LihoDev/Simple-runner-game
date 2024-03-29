using Prop;
using System.Collections.Generic;
using UnityEngine;

namespace ObstacleGenerator
{
    public class Row : MonoBehaviour
    {
        public List<Platform> Platforms { get; private set; } = new List<Platform>();
        public List<Ramp> Ramps { get; private set; } = new List<Ramp>();
        public List<Obstacle> Obstacles { get; private set; } = new List<Obstacle>();
        public List<Coin> Coins { get; private set; } = new List<Coin>();
        public List<HealthGem> HealthGems { get; private set; } = new List<HealthGem>();

        public void AddProp<T>(T prop) where T: InstantiatedProp
        {
            if (prop is Platform)
                Platforms.Add(prop as Platform);
            else if (prop is Ramp)
                Ramps.Add(prop as Ramp);
            else if (prop is Obstacle)
                Obstacles.Add(prop as Obstacle);
            else if (prop is Coin)
                Coins.Add(prop as Coin);
            else if (prop is HealthGem)
                HealthGems.Add(prop as HealthGem);
        }

        public void ClearProps()
        {
            Platforms.Clear();
            Obstacles.Clear();
            Ramps.Clear();
            Coins.Clear();
            HealthGems.Clear();
        }
    }
}