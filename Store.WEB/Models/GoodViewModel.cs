﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.WEB.Models
{
    public class GoodViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public int Count { get; set; }

        //public int Size { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Color { get; set; }
    }
}