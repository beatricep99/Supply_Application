using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Supply_Application.Models
{
    public class ListProduct
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [ForeignKey(typeof(SupplyList))]
        public int SupplyListID { get; set; }
        public int ProductID { get; set; }
    }
}
