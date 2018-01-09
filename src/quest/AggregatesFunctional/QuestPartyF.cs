using System.Collections.Generic;
using System.Linq;
using Baseline;

namespace quest
{
    public class QuestPartyF
    {
        private QuestPartyF() { }
        private List<string> _members = new List<string>();

        public string[] Members
        {
            get
            {
                return _members.ToArray();
            }
        }

        public string Name { get; private set; }

        public override string ToString() => $"Quest party '{Name}' is {Members.Join(", ")}";

        private static QuestPartyF Aggregator(QuestPartyF state, object @event)
        {
            switch (@event)
            {
                case MembersJoined joined:
                    state._members.Fill(joined.Members);
                    break;
                case MembersDeparted departed:
                    state._members.RemoveAll(x => departed.Members.Contains(x));
                    break;
                case QuestStarted started:
                    state.Name = started.Name;
                    break;
            }
            return state;
        }

        public static QuestPartyF Aggregate(List<object> events) => events.Aggregate(new QuestPartyF(), Aggregator);
    }
}