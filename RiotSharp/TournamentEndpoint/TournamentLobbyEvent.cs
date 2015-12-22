﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotSharp.TournamentEndpoint
{
    public class TournamentLobbyEvent
    {
        internal TournamentLobbyEvent() { }

        /// <summary>
        /// The type of event that was triggered
        /// </summary>
        [JsonProperty("eventType")]
        public string EventType { get; set; }

        /// <summary>
        /// The summoner that triggered the event
        /// </summary>
        [JsonProperty("summonerId")]
        public long SummonerId { get; set; }

        /// <summary>
        /// Timestamp from the event
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}
