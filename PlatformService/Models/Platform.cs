﻿namespace PlatformService.Models;
#nullable disable
public class Platform
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Publisher { get; set; }
    public decimal Cost { get; set; }
}