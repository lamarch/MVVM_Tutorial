namespace MVVM_Tutorial.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class RoomID
    {
        public RoomID(int floorNumber, int roomNumber)
        {
            FloorNumber = floorNumber;
            RoomNumber = roomNumber;
        }

        public int FloorNumber { get; }
        public int RoomNumber { get; }

        public override bool Equals(object? obj)
        {
            return obj is RoomID roomID
                   && roomID.FloorNumber == FloorNumber
                   && roomID.RoomNumber == RoomNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FloorNumber, RoomNumber);
        }

        public override string ToString()
        {
            return $"{FloorNumber}:{RoomNumber}";
        }

        public static bool operator ==(RoomID? roomID1, RoomID? roomID2)
        {
            if (roomID1 is null && roomID2 is null) return true;

            return roomID1 is not null && roomID1.Equals(roomID2);
        }

        public static bool operator !=(RoomID? roomID1, RoomID? roomID2)
        {
            return !(roomID1 == roomID2);
        }
    }
}
