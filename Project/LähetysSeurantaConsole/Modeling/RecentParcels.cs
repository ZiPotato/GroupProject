using OrderTracking.Core.Models.Package;

namespace LähetysSeurantaConsole.Modeling
{
    internal sealed class RecentParcels
    {
        private readonly Parcel?[] _items;

        internal int Capacity => _items.Length;
        internal bool IsEmpty => _items.All(p => p is null);

        internal RecentParcels(int capacity = 3)
        {
            if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than 0.");
            _items = new Parcel?[capacity];
        }

        internal void Add(Parcel parcel)
        {
            for (int i = _items.Length - 1; i > 0; i--)
            {
                _items[i] = _items[i - 1];
            }

            _items[0] = parcel;
        }

        internal Parcel? GetBySlot(int slot)
        {
            if (slot < 1 || slot > Capacity) return null;
            return _items[slot - 1];
        }

        internal IEnumerable<(int Slot, Parcel? Parcel)> GetSlots()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                yield return (i + 1, _items[i]);
            }
        }
    }
}