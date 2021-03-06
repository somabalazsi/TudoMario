﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TudoMario
{
    public class PlayerActor : ActorBase
    {
        public PlayerActor(Vector2 position, Vector2 size) : base(position, size, "Player", false)
        {

        }

        protected override void PerformBehaviour()
        {
            // jumping
            if ((UserControlHandler.PressedKeys & KeyAction.Up) != 0 && MovementSpeed.Y == 0 && CanJump)
            {
                MovementSpeed.Y = 20;
                CanJump = false;
            }

            // walking left-right
            if ((UserControlHandler.PressedKeys & KeyAction.Right) != 0)
                MovementSpeed.X += PhysicsController.friction * 2;
            if ((UserControlHandler.PressedKeys & KeyAction.Left) != 0)
                MovementSpeed.X -= PhysicsController.friction * 2;
        }
    }
}
