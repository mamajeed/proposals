using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Matrimonial.Model
{
    public class BaseEntity
    {
        [Key]
        public Int64 Id
        {
            get;
            set;
        }
        public DateTime AddedDate
        {
            get;
            set;
        }
        public DateTime ModifiedDate
        {
            get;
            set;
        }
        public string IPAddress
        {
            get;
            set;
        }
    }
}
