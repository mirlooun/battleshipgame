namespace Battleship
{
    public class LocationPoint : Location
    {
        public LocationPoint(int x, int y, ECellState pointState) : base(x, y)
        {
            PointState = pointState;
        }

        public ECellState PointState { get; set; }
    }
}
