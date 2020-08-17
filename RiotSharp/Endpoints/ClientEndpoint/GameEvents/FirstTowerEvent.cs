﻿namespace RiotSharp.Endpoints.ClientEndpoint.GameEvents
{
    /// <summary>
    /// Represents the destruction of the first tower in the game.
    /// </summary>
    public class FirstTowerEvent : BaseKilledGameEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirstTowerEvent"/> class.
        /// </summary>
        internal FirstTowerEvent() { }
    }
}