
namespace InterviewPrep.LLD.OOPS.Polymorphism
{
    #region Real Product Scenario
    //    Product Scenario

    //A company supports multiple notification providers.

    //Every provider sends notifications differently.

    //NotificationService

    //        ▲

    // ┌──────┼─────────┐

    // ▼      ▼         ▼

    //Email SMS     Push
    #endregion

    public abstract class NotificationService
    {
        public abstract void Send(string message);
    }

    public class EmailNotification : NotificationService
    {
        public override void Send(string message)
        {
            Console.WriteLine($"Email: {message}");
        }
    }

    public class SmsNotification : NotificationService
    {
        public override void Send(string message)
        {
            Console.WriteLine($"SMS: {message}");
        }
    }

}

#region Why Overriding?

//Tomorrow the company adds:

//WhatsApp

//Slack

//Teams

//The calling code remains:

//notification.Send(message);

//Only new derived classes are added.
#endregion
