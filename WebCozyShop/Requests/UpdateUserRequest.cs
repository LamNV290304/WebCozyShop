namespace WebCozyShop.Requests
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public DateOnly? Dob { get; set; }
    }
}
