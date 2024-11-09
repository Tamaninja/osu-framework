using System.Collections.Generic;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Containers;

namespace osu.Framework.Graphics.Rendering
{
    public class Node
    {
        public SceneNode Scene { get; set; }
        public Container Visualization { get; } = [];
        public List<Node> Children { get; } = [];
        public List<NodeInstance> Instances { get; set; } = [];


        public Bindable<string> Name = new();
        public void AddSubNode(Node node)
        {

            Children.Add(node);
            Visualization.Add(node.Visualization);
        }
        public void AddInstance(NodeInstance instance)
        {
            Instances.Add(instance);
            foreach (var subNode in Children)
            {
                subNode.AddInstance(instance);
            }
        }
        public Node(Scene scene) //root node
        {
            Name.BindValueChanged(t => Visualization.Name = t.NewValue);
        }
        public Node(SceneNode scene)
        {
            Scene = scene;
            scene.AddSubNode(this);

            Name.BindValueChanged(t => Visualization.Name = t.NewValue);

        }
        public Node(Node parent)
        {
            Scene = parent.Scene;
            parent.AddSubNode(this);

            Name.BindValueChanged(t => Visualization.Name = t.NewValue);

        }
    }
}
