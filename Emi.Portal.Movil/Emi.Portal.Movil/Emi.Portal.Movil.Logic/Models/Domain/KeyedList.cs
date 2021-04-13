namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    public class KeyedList<TKey, TItem> : List<TItem>
    {
        public TKey Key { protected set; get; }
        public KeyedList(TKey key, IEnumerable<TItem> items) : base(items)
        {
            Key = key;
        }
        public KeyedList(IGrouping<TKey, TItem> grouping) : base(grouping)
        {
            Key = grouping.Key;
        }
    }
}
