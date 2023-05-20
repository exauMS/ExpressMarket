using System.Collections;

namespace ExpressMarket.Services
{

    public partial class DeviceOrientationServices
    {
        public DeviceOrientationServices() { }
        public QueueBuffer SerialBuffer;

        public partial void ConfigureScanner();
    }

    public class QueueBuffer : Queue
    {
        
        public event EventHandler Changed;

        protected virtual void OnChanged()
        {
            if (Changed != null) Changed(this, EventArgs.Empty);
        }

        public override void Enqueue(object item)
        {
            base.Enqueue(item);
            OnChanged();
        }

    }
}
