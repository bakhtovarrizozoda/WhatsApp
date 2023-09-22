namespace Domain.Entities;

public class UserBaseDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string PhoneNamber { get; set; }
    public string Password { get; set; }
    public DateTime RegistrationDate { get; set; }
}  