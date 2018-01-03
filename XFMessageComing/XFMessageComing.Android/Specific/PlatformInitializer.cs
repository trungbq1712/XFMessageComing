using DryIoc;
using Prism.DryIoc;
using XFMessageComing.Droid.Services;
using XFMessageComing.Services;

namespace XFMessageComing.Droid.Specific
{
    public class PlatformInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainer container)
        {
            container.RegisterInstance<IReceiverService>(new AndroidReceiverService(), Reuse.Singleton);
        }
    }
}