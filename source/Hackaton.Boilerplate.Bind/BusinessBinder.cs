using DryIoc;
using Hackaton.Boilerplate.Abstraction.Business;
using Hackaton.Boilerplate.Business;
using Hackaton.Boilerplate.Model;

namespace Hackaton.Boilerplate.Bind
{
    public sealed class BusinessBinder
    {
        public static void Setup(IContainer container)
        {
            container.Register<IBusinessAsync<News>, NewsBusiness>(Reuse.Transient);
            container.Register<IBusinessAsync<UserAccount>, UserAccountBusiness>(Reuse.Transient);
        }
    }
}
