﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStudioCodingChallenge.Persistence.DbConfiguration
{
    public class MongoConfiguration
    {
        public string ConnectionString { get; set; } = null;
        public string DatabaseName { get; set; } = null;
    }
}
