using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ExampleSqlite
{
    [Table("tblUsers")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(256), Required]
        public string Name { get; set; }
        [StringLength(256), Required]
        public string Email { get; set; }
    }
}
