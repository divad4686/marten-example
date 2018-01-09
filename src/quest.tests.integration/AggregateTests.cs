using System;
using System.Linq;
using System.Threading.Tasks;
using Marten;
using Xunit;

namespace quest.tests.integration
{
    public class QuestPartyAggregateTest : IClassFixture<DocumentStoreFixture>
    {
        private readonly IDocumentStore _documentStore;

        public QuestPartyAggregateTest(DocumentStoreFixture fixture)
        {
            _documentStore = fixture.DocumentStore;
        }

        [Fact]
        public async Task QuestPartyAggregate()
        {
            using (var session = _documentStore.OpenSession())
            {
                var questId = Guid.NewGuid();

                var events = new Object[]
                {
                    new QuestStarted { Id = questId, Name = "test quest 1" },
                    new MembersJoined { QuestId = questId, Day = 0, Location = "ildrasil", Members = new string[] { "jalvar", "rei" } },
                    new MembersDeparted{Id = questId,Day = 1,Location="ironforge",Members = new string[]{"rei"}}
                };

                session.Events.Append(questId, events);
                session.SaveChanges();

                var eventsFromStream = (await session.Events.FetchStreamAsync(questId)).Select(@event => @event.Data).ToList();

                var questParty = QuestPartyF.Aggregate(eventsFromStream.ToList());
                Assert.Equal("test quest 1", questParty.Name);
                Assert.True(questParty.Members.Contains("jalvar"));
                Assert.False(questParty.Members.Contains("rei"));
            }
        }
    }
}
