using System;

namespace SharpSteamWebApi
{
    // Steam friend.
    public class Friend : SSWAObject
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public int Relationship { get; set; }

        // Constructor of a friend.
        public Friend()
        {
            Id = -1;
            Date = DateTime.MinValue;
            Relationship = -1;
        }

        // Checks if Id info is known.
        public bool HasId()
        {
            return Id != -1;
        }

        // Checks if date info is known.
        public bool HasDate()
        {
            return Date != DateTime.MinValue;
        }

        // Checks if relationship info is known.
        public bool HasRelationship()
        {
            return Relationship != -1;
        }
    }
}
