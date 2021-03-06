﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace TCAdminApiSharp.Entities.API
{
    public class ListResponse<T> : BaseResponse<IList<T>>
    {
        /// <summary>
        /// Only returns when "RowCount" was specified in the request.
        /// </summary>
        [JsonProperty("VirtualCount")] public int VirtualCount { get; internal set; }
    }
}