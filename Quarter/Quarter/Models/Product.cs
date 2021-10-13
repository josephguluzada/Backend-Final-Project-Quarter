using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Models
{
    public class Product
    {
        public int Id { get; set; }
        [StringLength (maximumLength:60)]
        public string Name { get; set; }
        [StringLength(maximumLength: 300)]
        public string Desc { get; set; }
        public double SalePrice { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLength (maximumLength:60)]
        public string Location { get; set; }
        public int ProductFloorCount { get; set; }
        public int ProductFloor { get; set; }
        public int RoomCount { get; set; }
        public int BedCount { get; set; }
        public int ParkingCount { get; set; }
        public int BathCount { get; set; }
        public bool IsFeature { get; set; }
        public double AreaSize { get; set; }
        public int Rate { get; set; }


        public int CityId { get; set; }
        public int SaleManagerId { get; set; }
        public int SaleStatusId { get; set; }
        public int CategoryId { get; set; }
        public City City { get; set; }
        public Category Category { get; set; }
        public SaleManager SaleManager { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public SaleStatus SaleStatus { get; set; }
        public List<ProductAminity> ProductAminities { get; set; }
        [NotMapped]
        public List<int> AminityIds { get; set; } = new List<int>();
        [NotMapped]
        public IFormFile PosterImage { get; set; }
    }
}
