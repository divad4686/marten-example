using System;
using System.Collections.Generic;

namespace quest
{
    public class PigSlayed
    {
        public readonly Guid QuestId;

        public PigSlayed(Guid questId)
        {
            QuestId = questId;
        }
    }

    public class BossSlayed
    {
        public readonly Guid QuestId;
        public readonly string Name;

        public BossSlayed(Guid questId, string name)
        {
            QuestId = questId;
            Name = name;
        }
    }
}