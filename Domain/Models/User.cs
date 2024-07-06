﻿namespace Data.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public ICollection<Chat> Chats { get; set; }
}