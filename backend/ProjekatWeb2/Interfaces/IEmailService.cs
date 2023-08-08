using ProjekatWeb2.Models;

namespace ProjekatWeb2.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string mailReceiver, string statusVerifikacije);

    }
}
