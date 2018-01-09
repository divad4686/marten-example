using System.Collections.Generic;
using System.Linq;

namespace quest
{
    public class MonstersSlayedF
    {
        private MonstersSlayedF() { }
        public int PigsKilled { get; private set; }
        public List<string> BossNames { get; private set; } = new List<string>();

        private static MonstersSlayedF Aggregator(MonstersSlayedF state, object @event)
        {
            switch (@event)
            {
                case PigSlayed pig:
                    state.PigsKilled += 1;
                    break;
                case BossSlayed boss:
                    state.BossNames.Add(boss.Name);
                    break;
            }
            return state;
        }

        public static MonstersSlayedF Aggregate(List<object> events) => events.Aggregate(new MonstersSlayedF(), Aggregator);
    }
}