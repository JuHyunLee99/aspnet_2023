﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using aspnet02_boardapp.Models;
using Microsoft.EntityFrameworkCore;

namespace aspnet02_boardapp.Data
{

    public class ApplicationDbContext : IdentityDbContext   // 1. ASP.NET Identity:  DbContext ->  IdentityDbContext
                                                                                                // DbContext + identity 기능
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Board> Boards{get; set;}
    }
}
