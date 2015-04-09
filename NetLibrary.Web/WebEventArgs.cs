using System;

namespace NetLibrary.Web
{
    public class WebEventArgs : EventArgs
    {
        public Action Action { get; set; }
        public int Progress { get; set; }
        public Exception Error { get; set; }
        public bool Canceled { get; set; }
        public long BytesReceived { get; set; }
        public long TotalBytesToReceive { get; set; }
        public object Result { get; set; }

        public WebEventArgs()
        {
        }
        public WebEventArgs(Exception error, bool cancel)
        {
            this.Error = error;
            this.Canceled = cancel;
        }
        public WebEventArgs(int progress)
        {
            this.Progress = progress;
        }
        public WebEventArgs(int progress, long bytes, long totalbytes)
        {
            this.Progress = progress;
            this.BytesReceived = bytes;
            this.TotalBytesToReceive = totalbytes;
        }
    }
}
