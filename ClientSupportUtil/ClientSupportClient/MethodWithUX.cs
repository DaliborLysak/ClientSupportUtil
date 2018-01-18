using ClientSupport;
using System.Windows.Forms;

namespace ClientSupportClient
{
    public class MethodWithUX
    {
        protected SupportProcessor Processor { get; private set; }

        protected UXData Data { get; private set; }

        protected CommonDialog Dialog { get; set; }

        public void Execute(SupportProcessor processor, UXData data)
        {
            if ((processor != null) && (data != null))
            {
                Processor = processor;
                Data = data;
                Dialog = GetDialog();
                var run = Dialog != null ? Dialog.ShowDialog() == DialogResult.OK : true;
                if (run)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        Process();
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                        Dialog?.Dispose();
                    }
                }
            }
        }

        protected virtual void Process()
        {
        }

        protected virtual CommonDialog GetDialog()
        {
            return null;
        }
    }
}
