using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Matrimonial.Model
{
    

    public class ModelMap
    {
        public ModelMap(EntityTypeBuilder<RegisterUser> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Password).IsRequired();
            entityBuilder.Property(t => t.EmailID).IsRequired();
            entityBuilder.Property(e => e.AddedDate).IsRequired();
                
        }
    }
}
