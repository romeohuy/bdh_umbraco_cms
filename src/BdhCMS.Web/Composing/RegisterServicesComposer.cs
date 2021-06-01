using BdhCMS.Web.ServicesMail;
using Umbraco.Core;
using Umbraco.Core.Composing;
namespace BdhCMS.Web.Composing
{
    public class RegisterServicesComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<ISmtpService, SmtpService>(Lifetime.Singleton);
        }
    }
}
