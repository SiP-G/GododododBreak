using System;
using Godot;

namespace Godododod.Entities
{
    public class EntityBody : KinematicBody2D
    {
        private EntityData _Data;

        public Action<float> PhysicsProcess = null;
        public Action<float> Process = null;
        public Action<InputEvent> Input = null;

        public EntityBody(EntityData data)
        {
            _Data = data;
        }

        public override void _PhysicsProcess(float delta)
        {
            PhysicsProcess?.Invoke(delta);

            if (_Data.Velocity != Vector2.Zero)
                Move();
        }

        public override void _Process(float delta)
        {
            Process?.Invoke(delta);
        }

        public override void _Input(InputEvent ev)
        {
            Input?.Invoke(ev);
        }

        private void Move()
        {
            UpdateLookDirection();
            MoveAndSlide(_Data.Velocity.Normalized() * _Data.Speed);
        }

        private void UpdateLookDirection()
        {
            if (_Data.Velocity.x > 0)
                _Data.Sprite.FlipH = false;
            else if (_Data.Velocity.x < 0)
                _Data.Sprite.FlipH = true;
        }
    }
}
