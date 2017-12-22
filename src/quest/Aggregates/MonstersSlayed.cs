using System;
using System.Collections.Generic;
using Marten.Events;
using quest;

namespace quest
{
    public class MonstersSlayed
    {
        public Guid Id { get; set; }
        public int PigsKilled { get; private set; }
        public List<string> BossNames { get; private set; } = new List<string>();

        public void Apply(PigSlayed @event) => PigsKilled += 1;

        public void Apply(BossSlayed @event) => BossNames.Add(@event.Name);

        public static MonstersSlayed Reduce(Guid id, List<object> events)
        {
            var monsters = new MonstersSlayed { Id = id };
            events.ForEach(@event =>
            {
                try
                {
                    monsters.Apply((dynamic)@event);
                }
                catch { }
            });
            return monsters;
        }

    }
}