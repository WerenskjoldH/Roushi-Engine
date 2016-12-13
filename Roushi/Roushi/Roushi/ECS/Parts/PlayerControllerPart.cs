using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Roushi
{
    class PlayerControllerPart : Part
    {
        int moveSpeed;
        bool isMoving; // only gets from player applied movement from wasd
        string movement; // "walkUp" "walkDown" "runLeft" "runRight"
        Action currentAction;

        public Action CurrentAction
        {
            get { return currentAction; }
            set { currentAction = value; }
        }

        public bool IsMoving()
        {
            return isMoving;
        }

        public PlayerControllerPart(int movementSpeed)
        {
            moveSpeed = movementSpeed;
            currentAction = new Action();
            currentAction.SetIdle();
        }

        public override void CleanUp()
        {
            base.CleanUp();
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        Vector2 velocity;
        Vector2 acceleration = new Vector2(.2f,.2f);
        Vector2 externalForce = Vector2.Zero;
        float maxVelocity = 2.5f;
        float groundFriction = .2f;
        float rollPower = 4.5f;
        int rollStaminaCost = 1;
        int staminaRegenRate = 4;

        public override void Update(GameTime gameTime)
        {
            Movement();
            currentAction.Update();

            base.Update(gameTime);
            entity.Get<TransformPart>().GetPosition += (velocity + externalForce);
        }

        private void Movement()
        {
            if (InputManager.Instance.KeyDown(Keys.W) && !InputManager.Instance.KeyDown(Keys.S) && currentAction.Movement)
            {
                velocity.Y -= acceleration.Y;
                isMoving = true;
                movement = "walkUp";
                if (velocity.Y < -maxVelocity)
                    velocity.Y = -maxVelocity;
            }
            else if (InputManager.Instance.KeyDown(Keys.S) && !InputManager.Instance.KeyDown(Keys.W))
            {
                velocity.Y += acceleration.Y;
                isMoving = true;
                movement = "walkDown";
                if (velocity.Y > maxVelocity)
                    velocity.Y = maxVelocity;
            }
            else
            {
                if (velocity.Y < 0)
                {
                    velocity.Y += acceleration.Y;
                    if (velocity.Y >= 0)
                        velocity.Y = 0f;
                }
                else if (velocity.Y > 0)
                {
                    velocity.Y -= acceleration.Y;
                    if (velocity.Y <= 0)
                        velocity.Y = 0f;
                }
            }

            if (InputManager.Instance.KeyDown(Keys.A) && !InputManager.Instance.KeyDown(Keys.D))
            {
                velocity.X -= acceleration.X;
                isMoving = true;
                movement = "walkLeft";
                if (velocity.X < -maxVelocity)
                    velocity.X = -maxVelocity;
            }
            else if (InputManager.Instance.KeyDown(Keys.D) && !InputManager.Instance.KeyDown(Keys.A))
            {
                velocity.X += acceleration.X;
                isMoving = true;
                movement = "walkRight";
                if (velocity.X > maxVelocity)
                    velocity.X = maxVelocity;
            }
            else
            {
                if (velocity.X < 0)
                {
                    velocity.X += acceleration.X;
                    if (velocity.X >= 0)
                        velocity.X = 0f;
                }
                else if (velocity.X > 0)
                {
                    velocity.X -= acceleration.X;
                    if (velocity.X <= 0)
                        velocity.X = 0f;
                }
            }


            if (velocity == Vector2.Zero) // Stop animation
            {
                isMoving = false;
                if ( currentAction.Idle == true )
                    movement = "idle";
            }

            if (InputManager.Instance.KeyPressed(Keys.K) && velocity != Vector2.Zero && EnoughStamina(rollStaminaCost) && currentAction.Interruptible)
            {
                float angle = (float)Math.Atan2(velocity.Y, velocity.X);
                externalForce.X += rollPower * (float)Math.Cos(angle);
                externalForce.Y += rollPower * (float)Math.Sin(angle);
                currentAction.SetAction("dash", false, false, false, 14);
            }

            StaminaActions();
            AdjustExternalForce();
        }

        public void CancelActions()
        {
            currentAction.SetIdle();
        }

        int staminaCtr = 0;
        private void StaminaActions()
        {
            StatsPart statPart = entity.Get<StatsPart>();
            if (statPart.GetStamina != statPart.GetMaxStamina)
            {
                if(staminaCtr++ >= staminaRegenRate * 30)
                {
                    statPart.GetStamina += 1;
                    staminaCtr = 0;
                }
            }
        }

        public bool EnoughStamina(int cost) // Checks then reduces stamina if able
        {
            StatsPart statPart = entity.Get<StatsPart>();
            if (statPart.GetStamina - cost >= 0)
            {
                statPart.GetStamina -= cost;
                return true;
            }
            else
                return false;
        }

        private void AdjustExternalForce()
        {
            if ((float)Math.Abs(externalForce.X) <= groundFriction)
                externalForce.X = 0;
            if ((float)Math.Abs(externalForce.Y) <= groundFriction)
                externalForce.Y = 0;

            if (externalForce.X > 0)
            {
                externalForce.X -= groundFriction;
            }
            else if (externalForce.X < 0)
            {
                externalForce.X += groundFriction;
            }

            if (externalForce.Y > 0)
            {
                externalForce.Y -= groundFriction;
            }
            else if (externalForce.Y < 0)
            {
                externalForce.Y += groundFriction;
            }

            
        }
    }
}
