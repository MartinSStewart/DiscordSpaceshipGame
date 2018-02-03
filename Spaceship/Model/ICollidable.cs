using FixMath.NET;

namespace Spaceship.Model
{
    public interface ICollidable
    {
        Fix2 Position { get; }
        Fix Size { get; }
    }
}