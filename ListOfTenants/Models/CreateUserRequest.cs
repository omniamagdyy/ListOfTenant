﻿namespace ListOfTenants.Models
{
    public class CreateUserRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string OwnerId { get; set; }
    }
}
