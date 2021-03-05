using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace FloridaLottery.Services
{
    public class MockGemStore : IDataStore<Gem>
    {
        readonly List<Gem> items;

        public MockGemStore()
        {
            items = new List<Gem>()
            {
                new Gem { Id = Guid.NewGuid().ToString(), Name = "First Gem", Draws = GetDraws() },
                new Gem { Id = Guid.NewGuid().ToString(), Name = "Second Gem", Draws= GetDraws() },
                new Gem { Id = Guid.NewGuid().ToString(), Name = "Third Gem", Draws= GetDraws() },
                new Gem { Id = Guid.NewGuid().ToString(), Name = "Fourth Gem", Draws= GetDraws() },
                new Gem { Id = Guid.NewGuid().ToString(), Name = "Fifth Gem", Draws= GetDraws() },
                new Gem { Id = Guid.NewGuid().ToString(), Name = "Sixth Gem", Draws= GetDraws() }
            };
        }

        List<Draw> GetDraws()
        {

            return new List<Draw>()
            {
                new Draw(1234),
                new Draw(4321),
                new Draw(1234),
                new Draw(4321),
                new Draw(1234),
                new Draw(4321),
                new Draw(1234),
                new Draw(4321)
            };
        }

        public async Task<bool> AddItemAsync(Gem Gem)
        {
            items.Add(Gem);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Gem arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Gem> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Gem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<bool> UpdateItemAsync(Gem Gem)
        {
            var oldItem = items.Where((Gem arg) => arg.Id == Gem.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(Gem);

            return await Task.FromResult(true);
        }
    }
}
