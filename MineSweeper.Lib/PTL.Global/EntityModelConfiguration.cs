using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Lib.PTL
{
    public class EntityModelConfiguration : IEntityTypeConfiguration<MineSweeper>
    {
        
        public void Configure(EntityTypeBuilder<MineSweeper> builder)
        {
            /**
            builder.HasMany<T>(s => s.ManyModel)
            .WithOne(m => m.OneModel)
            .HasForeignKey(m => m.OneModelID)
            .OnDelete(DeleteBehavior.Cascade);
            **/
        }
        
    }
}
