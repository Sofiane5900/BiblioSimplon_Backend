namespace BiblioAPI.Models
{
    public class EmployeModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class RegisterEmployeDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class LoginEmployeDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
