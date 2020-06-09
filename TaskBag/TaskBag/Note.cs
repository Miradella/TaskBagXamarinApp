using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TaskBag
{
    [Table("Notes")]
    public class Note
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public string Note_Name { get; set; }
        public string Text { get; set; }
    }
}
