

namespace InterviewPrep.LLD.SolidPrinciples.DIP.EmployeeAttendanceSystem.Models
{
    public class NotificationMessage
    {
        public string Recipient { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}

//Why not pass

//Send(string email,
//     string subject,
//     string message)

//Because tomorrow

//business may ask

//CC
//BCC
//Priority
//Attachment

//One model can evolve without changing the interface.