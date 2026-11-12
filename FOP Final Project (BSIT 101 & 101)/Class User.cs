using System;

namespace FOP_Final_Project__BSIT_101___106_
{
    public class User
{
	public User()
	{
		public string Username { get; set; };
		public string Password { get; set; };
		public string Role { get; set; };
		public bool IsActive { get; set; };

	public User(string username, string password, string role, bool isActive)
    {
        Username = username;
        Password = password;
        Role = role;
        IsActive = true;
    }
}
