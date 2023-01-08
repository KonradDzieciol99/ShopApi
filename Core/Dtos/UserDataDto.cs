namespace Core.Dtos
{
    public class UserDataDto
    {
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string? Email { get; set; }//jeśli zmienimy adres to musi wysłać nowy token jwt
        public string? PhoneNumber { get; set; }
    }
}
