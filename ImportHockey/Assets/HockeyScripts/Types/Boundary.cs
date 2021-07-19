
namespace HockeyScripts.Types
{
    internal readonly struct Boundary
    {
        public readonly float Up;
        public readonly float Down;
        public readonly float Left;
        public readonly float Right;

        public Boundary(float up, float down, float left, float right)
        {
            this.Up = up;
            this.Down = down;
            this.Left = left;
            this.Right = right;
        }
    }
}