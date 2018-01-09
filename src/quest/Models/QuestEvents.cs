using System;
using Baseline;

namespace quest
{
    public class Quest
    {
        public Guid Id { get; set; }
    }

    // SAMPLE: sample-events
    public class ArrivedAtLocation
    {
        public string Location { get; set; }

        public override string ToString()
        {
            return $"Arrived at {Location}";
        }
    }

    public class MembersJoined
    {

        public MembersJoined()
        {
        }

        public MembersJoined(string location, params string[] members)
        {
            Location = location;
            Members = members;
        }

        public Guid QuestId { get; set; }

        public int Day { get; set; }

        public string Location { get; set; }

        public string[] Members { get; set; }

        public override string ToString()
        {
            return $"Members {Members.Join(", ")} joined at {Location}";
        }
    }


    public class QuestStarted
    {
        public string Name { get; set; }
        public Guid Id { get; set; }

        public override string ToString()
        {
            return $"Quest {Name} started";
        }
    }

    public class QuestEnded
    {
        public string Name { get; set; }
        public Guid Id { get; set; }

        public override string ToString()
        {
            return $"Quest {Name} ended";
        }
    }

    public class MembersDeparted
    {
        public Guid Id { get; set; }

        public int Day { get; set; }

        public string Location { get; set; }

        public string[] Members { get; set; }

        public override string ToString()
        {
            return $"Members {Members.Join(", ")} departed at {Location} on Day {Day}";
        }
    }
}