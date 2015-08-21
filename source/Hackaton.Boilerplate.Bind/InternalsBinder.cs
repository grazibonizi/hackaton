using DryIoc;
using Hackaton.Boilerplate.Abstraction.Internals;
using Hackaton.Boilerplate.Shared.Internals;
using MongoDB.Driver;
using System.Configuration;

namespace Hackaton.Boilerplate.Bind
{
    public sealed class InternalsBinder
    {
        public static void Setup(IContainer container)
        {
            container.Register<ILogger, Log4NetAdapter>(Reuse.Transient);
            container.Register<IMailManager, MailManager>(Reuse.Transient);

            container.RegisterDelegate<IMongoClient>(
                resolver => 
                    new MongoClient(
                        ConfigurationManager
                            .ConnectionStrings["mongodb"]
                            .ConnectionString
                    ), 
                Reuse.Transient
            );

            container.Register<IHash, SHA512Hash>(Reuse.Transient);
        }
    }
}
