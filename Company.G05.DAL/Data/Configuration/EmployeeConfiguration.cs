﻿using Company.G05.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G05.DAL.Data.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(E => E.Id);
            builder.HasOne(E => E.WorkFor).WithMany(D => D.Employees).OnDelete(DeleteBehavior.Cascade);
            builder.Property(E => E.Salary)
                   .HasColumnType("decimal(18,2)");
        }
    }
}
