using System;
using System.Collections.Generic;
using System.Linq;
using Baseline;

namespace quest
{
    public class QuestParty
    {
        private readonly IList<string> _members = new List<string>();

        public string[] Members
        {
            get
            {
                return _members.ToArray();
            }
            set
            {
                _members.Clear();
                _members.AddRange(value);
            }
        }

        public void Apply(MembersJoined joined)
        {
            _members.Fill(joined.Members);
        }

        public void Apply(MembersDeparted departed)
        {
            _members.RemoveAll(x => departed.Members.Contains(x));
        }

        public void Apply(QuestStarted started)
        {
            Console.WriteLine(started.Name);
            Name = started.Name;
        }

        public string Name { get; set; }

        public Guid Id { get; set; }

        public override string ToString()
        {
            return $"Quest party '{Name}' is {Members.Join(", ")}";
        }
    }
}