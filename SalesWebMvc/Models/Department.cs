﻿using System.Security.Policy;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department()
        {
        }



        public Department(string name)
        {
            Name = name;
        }

        public void Add(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime inicial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSalles(inicial, final));
        }

    }
}
