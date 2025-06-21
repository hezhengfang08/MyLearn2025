﻿using MySelf.MSACommerce.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SeckillService.Core.Entities
{
    public class SecKillProduct: BaseAuditEntity
    {
        public long SpuId { get; set; }
        public long SkuId { get; set; }
        public required string Name { get; set; }
        public required string SmallPic { get; set; }
        public long Price { get; set; }
        public long CostPrice { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int Num { get; set; }
        public long StockCount { get; set; }
        public string? Introduction { get; set; }
    }
}
