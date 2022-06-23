namespace MVVM_Tutorial.Models;

using System;

internal class Reservation
{
    public Reservation(RoomID roomID, DateTime startTime, DateTime endTime, string username)
    {
        RoomID = roomID;
        StartTime = startTime;
        EndTime = endTime;
        Username = username;
    }

    public RoomID RoomID { get; }
    public string Username { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    public TimeSpan Length => EndTime - StartTime;
}
