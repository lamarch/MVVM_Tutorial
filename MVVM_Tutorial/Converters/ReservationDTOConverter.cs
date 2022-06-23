namespace MVVM_Tutorial.Converters;

using MVVM_Tutorial.DTOs;
using MVVM_Tutorial.Models;

internal static class ReservationDTOConverter
{
    public static Reservation ToReservationModel(this ReservationDTO reservationDTO)
    {
        return new Reservation(
            new RoomID(reservationDTO.FloorNumber, reservationDTO.RoomNumber),
            reservationDTO.StartTime,
            reservationDTO.EndTime,
            reservationDTO.Username ?? ""
            );
    }

    public static ReservationDTO ToReservationDTO(this Reservation reservation)
    {
        return new ReservationDTO()
        {
            FloorNumber = reservation.RoomID.FloorNumber,
            RoomNumber = reservation.RoomID.RoomNumber,
            StartTime = reservation.StartTime,
            EndTime = reservation.EndTime,
            Username = reservation.Username,
        };
    }
}
