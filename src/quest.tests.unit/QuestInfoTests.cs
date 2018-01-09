using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace quest.tests.unit
{
    [TestClass]
    public class QuestInfoTests
    {
        [TestMethod]
        public void QuestPartyAggregateTest()
        {
            var questId = Guid.NewGuid();

            var events = new Object[]
            {
                new QuestStarted { Id = questId, Name = "test quest 1" },
                new MembersJoined { QuestId = questId, Day = 0, Location = "ildrasil", Members = new string[] { "jalvar", "rei" } },
                new MembersDeparted{Id = questId,Day = 1,Location="ironforge",Members = new string[]{"rei"}}
            };

            var questParty = QuestPartyF.Aggregate(events.ToList());
            Assert.AreEqual("test quest 1", questParty.Name);
            Assert.IsTrue(questParty.Members.Contains("jalvar"));
            Assert.IsFalse(questParty.Members.Contains("rei"));
        }

        [TestMethod]
        public void MonstersSlayedAggregateTest()
        {
            var questId = Guid.NewGuid();

            var events = new Object[]
            {
                new QuestStarted { Id = questId, Name = "test quest 1" },
                new MembersJoined { QuestId = questId, Day = 0, Location = "ildrasil", Members = new string[] { "jalvar", "rei" } },
                new PigSlayed(questId),
                new PigSlayed(questId),
                new BossSlayed(questId,"Lich King"),
                new PigSlayed(questId),
            };

            var monstersSlayed = MonstersSlayedF.Aggregate(events.ToList());
            Assert.AreEqual(3, monstersSlayed.PigsKilled);
            Assert.IsTrue(monstersSlayed.BossNames.Contains("Lich King"));
        }
    }
}
