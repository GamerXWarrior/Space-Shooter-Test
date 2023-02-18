using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class ShipModel
    {
        public ShipModel(ShipScriptableObject ship)
        {
            MovingSpeed = ship.movingSpeed;
            RotatingSpeed = ship.rotatingSpeed;
            Health = ship.health;
        }

        public int MovingSpeed { get; }
        public int RotatingSpeed { get; }
        public float Health { get; set; }
    }