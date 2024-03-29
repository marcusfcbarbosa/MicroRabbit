﻿using System;

namespace MicroRabbit.Domain.Core.Events
{
    public abstract class Event
    {
        public DateTime TimeStamp { get; protected set; }

        protected Event() {
            this.TimeStamp = DateTime.Now;
        }
    }
}