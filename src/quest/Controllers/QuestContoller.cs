using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace quest.Controllers
{
    [Route("api/[controller]")]
    public class QuestContoller : Controller
    {
        private IDocumentStore _store;

        public QuestContoller(IDocumentStore store)
        {
            _store = store;
        }

        private async Task SaveEvent(Guid questId, object @event)
        {
            using (var session = _store.OpenSession())
            {
                session.Events.Append(questId, @event);
                await session.SaveChangesAsync();
            }
        }

        // POST api/values
        [HttpPost("createQuest")]
        public async Task<IActionResult> CreateQuest(string name)
        {
            var questId = Guid.NewGuid();
            var questEvent = new QuestStarted { Name = name };
            await SaveEvent(questId, questEvent);
            return Ok(questId);
        }

        [HttpPost("addMember")]
        public async Task<IActionResult> AddMember(string name, string location, Guid questId)
        {
            var @event = new MembersJoined(location, new string[] { name });
            await SaveEvent(questId, @event);
            return Ok(@event.ToString());
        }

        [HttpPost("killPig")]
        public async Task<IActionResult> KillPig(Guid questId, string name)
        {
            var @event = new PigSlayed(questId, name);
            await SaveEvent(questId, @event);
            return Ok(@event.ToString());
        }

        [HttpPost("killBoss")]
        public async Task<IActionResult> KillBoss(Guid questId, string name)
        {
            var @event = new BossSlayed(questId, name);
            await SaveEvent(questId, @event);
            return Ok(@event.ToString());
        }

        [HttpGet("questInfo/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            using (var session = _store.OpenSession())
            {
                var quest = await session.Events.AggregateStreamAsync<QuestParty>(id);
                var bosses = await session.Events.AggregateStreamAsync<MonstersSlayed>(id);
                return Ok(new { Quest = quest, Bosses = bosses });
            }
        }

    }
}
