﻿using Microsoft.EntityFrameworkCore;

namespace NetCoreApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }
    }
}