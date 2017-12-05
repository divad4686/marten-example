using System;
using System.Collections.Generic;
using quest;

namespace quest
{
    public class MonstersSlayed
    {
        public Guid Id;
        public int PigsKilled { get; private set; }
        public List<string> BossNames { get; private set; }

        public void Apply(PigSlayed @event) => PigsKilled += 1;

        public void Apply(BossSlayed @event) => BossNames.Add(@event.Name);
    }
}