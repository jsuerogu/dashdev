﻿using System;

namespace DashboarLaboral.Data
{
    public class EventLog
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public DateTime Timestamp { get; set; }
        public string Exception { get; set; }
        public string LogEvent { get; set; }
    }
}
