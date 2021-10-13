namespace Battleship
{
    public record HitResponse
    {
        public bool IsHit { get; set; }
        private string? BoatName { get; }
        public bool IsDestroyed { get; }
        public HitResponse(string boatName, int boatHp)
        {
            IsHit = true;
            BoatName = boatName;
            IsDestroyed = boatHp == 0;
        }

        public HitResponse()
        {
        }

        public string GetMessage()
        {
            string message = IsHit switch
            {
                true when IsDestroyed => $"You destroyed a {BoatName}!",
                true => "You made a hit!",
                _ => "You missed"
            };

            return message;
        }
    }
}
