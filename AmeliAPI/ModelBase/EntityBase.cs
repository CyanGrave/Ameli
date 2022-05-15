using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ModelBase
{
    public abstract class EntityBase
    {
        [NotMapped]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long EntityTypeID 
        {
            get
            {
                return GetSimpleHash(this.GetType().Name);
            }
        } 



        [NotMapped]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public object[] PrimaryKey
        {
            get
            {
                return (from property in this.GetType().GetProperties()
                        where Attribute.IsDefined(property, typeof(KeyAttribute))
                        orderby ((ColumnAttribute)property.GetCustomAttributes(false).Single(
                            attr => attr is ColumnAttribute)).Order ascending
                        select property.GetValue(this)).ToArray();
            }
        }


        [NotMapped]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string[] LocalizableProperties
        {
            get
            {
                return (from property in this.GetType().GetProperties()
                        where Attribute.IsDefined(property, typeof(LocalizableAttribute))
                        select property.Name).ToArray();
            }
        }


        public string GetEntityUniqueKey()
        {
            return  $"{EntityTypeID}_{string.Join("_",PrimaryKey)}";
        }


        [NotMapped]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public HashSet<string> Tags { get; set; } = new HashSet<string>();

        private long GetSimpleHash(string input)
        {
            var bytes = Encoding.ASCII.GetBytes(input);
            Array.Resize(ref bytes, bytes.Length + (8 - bytes.Length % 8));


            return Enumerable.Range(0, bytes.Length / 8) 
                .Select(i => BitConverter.ToInt64(bytes, i * 8)) 
                .Aggregate((x, y) => x ^ y);

        }
    }



}
