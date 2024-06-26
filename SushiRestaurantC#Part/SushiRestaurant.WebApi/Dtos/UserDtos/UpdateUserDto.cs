﻿using System.ComponentModel.DataAnnotations;

namespace SushiRestaurant.WebApi.Dtos.UserDtos;

public class UpdateUserDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public required string Email { get; set; }


    [Required(ErrorMessage = "Username is required")]
    [StringLength(100, MinimumLength = 3)]
    public required string Username { get; set; }
    public required bool IsVerify {  get; set; }
    public required string Role { get; set; }
}
