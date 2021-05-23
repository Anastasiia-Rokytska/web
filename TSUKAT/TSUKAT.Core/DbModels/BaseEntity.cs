using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TSUKAT.Core.DbModels
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }
}
