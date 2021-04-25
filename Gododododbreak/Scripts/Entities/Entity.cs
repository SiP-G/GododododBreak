using System;
using Godot;

namespace Godododod.Entities
{
    public abstract class Entity
    {
        protected EntityBody Body;
        protected EntityData Data;

        public Entity()
        {
            Data = new EntityData();
            Body = new EntityBody(Data);

            AddChild(Data.Sprite);
            AddChild(Data.Collider);

            Body.PhysicsProcess = PhysicsProcess;
            Body.Process = Process;
            Body.Input = Input;

        }

        public void PhysicsProcess(float delta)
        {

        }

        public void Process(float delta)
        {

        }

        public void Input(InputEvent ev)
        {

        }

        public void ConnectToNode(Node parent)
        {
            parent.AddChild(Body);
        }

        public void DisconnectFromNode(Node parent)
        {
            parent.RemoveChild(Body);
        }

        public void AddChild(Node child)
        {
            Body.AddChild(child);
        }

        public void RemoveChild(Node child)
        {
            Body.RemoveChild(child);
        }
    }
}
