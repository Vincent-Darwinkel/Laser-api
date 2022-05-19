﻿using LaserAPI.Models.Dto.LasershowSpotify;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaserAPI.Interfaces.Dal
{
    public interface ILasershowSpotifyConnectorDal
    {
        public Task Add(LasershowSpotifyConnectorDto connector);
        public Task<List<LasershowSpotifyConnectorDto>> All();
        public Task<bool> Exists(Guid lasershowUuid);
        public Task<bool> SongsExists(List<string> spotifySongIds);
        public Task Remove(Guid lasershowUuid);
    }
}
