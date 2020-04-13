using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Masco.Display.ILSValidator.Client.Forms
{
    public class ErrorTracker
    {
        private HashSet<Control> mErrors = new HashSet<Control>();
        private ErrorProvider mProvider;

        public ErrorTracker(ErrorProvider provider)
        {
            mProvider = provider;
        }
        public void SetError(Control ctl, string text)
        {
            if (string.IsNullOrEmpty(text)) mErrors.Remove(ctl);
            else if (!mErrors.Contains(ctl)) mErrors.Add(ctl);
            mProvider.SetError(ctl, text);
        }
        public int Count { get { return mErrors.Count; } }
    }

    public class MyErrorProvider : ErrorProvider
    {

        public List<Control> GetControls()
        {
            return this.GetControls(this.ContainerControl);
        }

        public List<Control> GetControls(Control ParentControl)
        {
            List<Control> ret = new List<Control>();

            if (!string.IsNullOrEmpty(this.GetError(ParentControl)))
                ret.Add(ParentControl);

            foreach (Control c in ParentControl.Controls)
            {
                List<Control> child = GetControls(c);
                if (child.Count > 0)
                    ret.AddRange(child);
            }

            return ret;
        }
    }
}
