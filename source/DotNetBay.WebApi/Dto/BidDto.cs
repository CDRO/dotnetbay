﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBay.WebApi.Dto
{
    public class BidDto
    {
        public long Id { get; set; }

        public DateTime ReceivedOnUtc { get; set; }

        public Guid TransactionId { get; set; }

        public double Amount { get; set; }

        public bool? Accepted { get; set; }

        public string BidderName { get; set; }
    }
}
