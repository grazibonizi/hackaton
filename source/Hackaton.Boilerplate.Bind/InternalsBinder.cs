using DryIoc;
using Hackaton.Boilerplate.Abstraction.Internals;
using Hackaton.Boilerplate.Shared.Internals;

namespace Hackaton.Boilerplate.Bind
{
    public sealed class InternalsBinder
    {
        public static void Setup(IContainer container)
        {
            container.Register<ILogger, Log4NetAdapter>(Reuse.Transient);
            container.Register<IMailManager, MailManager>(Reuse.Transient);
        }
    }
}
