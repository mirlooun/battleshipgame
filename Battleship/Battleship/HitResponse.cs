namespace Battleship
{
    public record HitResponse
    {
        private bool IsHit { get; }
        private string? BoatName { get; }
        private bool IsDestroyed { get; }
        private bool IsSameCell { get; }
        public bool IsSamePlayerMove => IsHit || IsDestroyed || IsSameCell;
        public HitResponse(string boatName, int boatHp)
        {
            IsHit = true;
            BoatName = boatName;
            IsDestroyed = boatHp == 0;
        }

        public HitResponse(ECellState cellState)
        {
            IsSameCell = cellState == ECellState.Hit;
        }
        public string GetMessage()
        {

            string message;

            if (IsDestroyed)
            {
                message = $"You destroyed {BoatName}";
            } else if (IsSameCell)
            {
                message = "You have already hit this cell!";
            } else if (IsHit)
            {
                message = "You hit a boat!";
            }
            else
            {
                message = "You missed";
            }

            return message;
        }
    }
}
