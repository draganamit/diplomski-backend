namespace diplomski_backend.Dtos
{
    public class UpdatePasswordDto
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}