﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SharedKernel.Paging
{
    public class Pagination
    {
        private const int MaxPageSize = 100;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
