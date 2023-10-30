namespace CurrentAccount.Domain.Notifications.Interfaces
{
    public interface INotification
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
