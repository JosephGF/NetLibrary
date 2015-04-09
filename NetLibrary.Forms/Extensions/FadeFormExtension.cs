using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Forms.Extensions
{
    public static class FadeFormExtension
    {
        public static event EventHandler<EventArgs> OnFadeOutEnd;
        public static event EventHandler<EventArgs> OnFadeInEnd;
        public static void FadeOut(this Form form, double minOpacity, int steps = 100, int duration = 1000, bool dispose = false)
        {
            if (form.Opacity <= minOpacity)
                return;

            Timer timer = new Timer();
            timer.Interval = duration / steps;

            int currentStep = 0;
            timer.Tick += (arg1, arg2) =>
            {
                form.Opacity = (((double)currentStep) / steps - 1) * -1;
                currentStep++;

                if (form.Opacity == minOpacity)
                    timer.Stop();

                if (currentStep >= steps)
                {
                    timer.Stop();
                    if (OnFadeOutEnd != null)
                        OnFadeOutEnd(form, new EventArgs());
                    timer.Dispose();
                }
            };

            timer.Start();

            if (dispose)
                form.Dispose();
        }

        public static void FadeIn(this Form form, double maxOpacity, int steps = 100, int duration = 1000, bool dispose = false)
        {
            if (form.Opacity >= maxOpacity)
                return;

            Timer timer = new Timer();
            timer.Interval = duration / steps;

            int currentStep = 0;
            timer.Tick += (arg1, arg2) =>
            {
                form.Opacity = ((double)currentStep) / steps;
                currentStep++;

                if (form.Opacity == maxOpacity)
                    timer.Stop();

                if (currentStep >= steps)
                {
                    timer.Stop();

                    if (OnFadeInEnd != null)
                        OnFadeInEnd(form, new EventArgs());

                    timer.Dispose();
                }
            };

            timer.Start();

            if (dispose)
                form.Dispose();
        }
    }
}
