using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApplication.Models;

namespace TodoApplication.Data.Configurations
{
    public class TodoConfiguration: IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.Property(t => t.CreateDate).HasColumnName("CreateDate");
        }
    }
}
