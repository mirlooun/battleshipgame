using Helpers;

namespace InitMenu.Helpers
{
    public static class PlayerValidator
    {
        public static InitPlayerResponse IsNameValid(string? name)
        {
            var response = new InitPlayerResponse();

            if (string.IsNullOrEmpty(name))
            {
                response.Message = "Name can't be empty";
            } else if (name.Length > 25)
            {
                response.Message = "Name can't be longer than 25 characters";
            }

            response.IsValid = response.Message == null;

            return response;
        }
    }
}
