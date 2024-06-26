﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManager.Services
{
    public class ServiceResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
        public object Payload { get; set; }
        public IEnumerable<object> Errors { get; set; }
    }
}
