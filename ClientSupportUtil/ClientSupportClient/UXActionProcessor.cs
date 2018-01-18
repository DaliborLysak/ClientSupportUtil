using ClientSupport;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ClientSupportClient
{
    public class UXActionProcessor
    {
        protected SupportProcessor Processor { get; private set; } = new SupportProcessor();

        public Dictionary<object, MethodWithUX> Actions { get; set; }

        public UXData Data { get; set; }

        public void Process(object sender)
        {
            if ((Actions != null) && (Data != null))
            {
                try
                {
                    EnableControls(false);
                    if ((sender != null) && Actions.ContainsKey(sender))
                    {
                        Actions[sender]?.Execute(Processor, Data);
                    }
                }
                finally
                {
                    EnableControls(true);
                }
            }
        }

        private void EnableControls(bool enable)
        {
            Actions.Keys.OfType<Control>().ToList().ForEach(c => c.Enabled = enable);
        }
    }
}
