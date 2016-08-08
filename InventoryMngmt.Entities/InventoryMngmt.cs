using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMngmt.Entities
{
    public class Product
    {
        [Required]
        public int ProductID { get; set; }

        public string Serial { get; set; }

        public InventoryType Type { get; set; }

        public Manufacturer Brand { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AcquisitionDate { get; set; }

        public Warehouse Location { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Transactions> Transactions { get; set; }

    }

    public class Transactions
    {
        [Required]
        [Key]
        public int TransactionID { get; set; }

        [DisplayName("Product Serial")]
        public int ProductID { get; set; }

        [DisplayName("Warehouse From")]
        public Warehouse WarehouseFrom { get; set; }

        [DisplayName("Warehouse To")]
        public Warehouse WarehouseTo { get; set; }

        public string Reason { get; set; }

        public virtual Product Product { get; set; }

    }

    public enum InventoryType
    {
        [Display(Name="Computer")]
        Computer,

        [Display(Name = "Printer")]
        Printer

    }

    public enum Manufacturer
    {
        [Display(Name = "Acer")]
        Acer,

        [Display(Name = "Asus")]
        Asus,

        [Display(Name = "Dell")]
        Dell,

        [Display(Name = "HP")]
        HP,

        [Display(Name = "Samsung")]
        Samsung,

        [Display(Name = "Sony")]
        Sony
    }

    public enum Warehouse
    {
        [Display(Name = "Dallas")]
        Dallas,
        [Display(Name = "Austin")]
        Austin,
        [Display(Name = "Chicago")]
        Chicago,
        [Display(Name = "Lansing")]
        Lansing,
        [Display(Name = "Denver")]
        Denver

    }
}