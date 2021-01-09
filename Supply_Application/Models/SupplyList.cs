using SQLite;
using System;

namespace Supply_Application.Models
{
    public class SupplyList
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } = -1;
        public bool Exists { get => ID != -1; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
