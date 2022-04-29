﻿using System;

namespace LaserAPI.Models.FromFrontend.Zones
{
    public class ZonesPosition
    {
        public Guid Uuid { get; set; }
        public Guid ZoneUuid { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Order { get; set; }
    }
}
