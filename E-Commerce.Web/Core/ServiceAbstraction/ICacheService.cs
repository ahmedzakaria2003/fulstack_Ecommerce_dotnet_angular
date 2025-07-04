﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface ICacheService
    {

        Task<string?> GetAsync(string Cachekey);

        Task SetAsync(string Cachekey, string value, TimeSpan TimeToLive);

    }
}
