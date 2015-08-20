using DryIoc;
using Hackaton.Boilerplate.Abstraction.DAO;
using Hackaton.Boilerplate.DAO;
using Hackaton.Boilerplate.Model;

namespace Hackaton.Boilerplate.Bind
{
    public sealed class DAOBinder
    {
        public static void Setup(IContainer container)
        {
            container.Register<IRepositoryAsync<News>, NewsRepository>(Reuse.Transient);
        }
    }
}
