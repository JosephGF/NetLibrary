using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace NetLibrary.Archives
{
    public enum Action
    {
        None, Move, MultiMove, Copy, MultiCopy
    }
    public enum State
    {
        Wait, Start, Error, Complete
    }

    public class FileEventArgs : EventArgs
    {
        public FileInfo File { get; set; }
        public Action Action { get; set; }
        public State State { get; set; }
        public int Progress { get; set; }
        public string Error { get; set; }

        public FileEventArgs(FileInfo file)
        {
            this.File = file;
            this.Action = Archives.Action.None;
            this.State = State.Wait;
        }

        public FileEventArgs(FileInfo file, Action action, State state, int progress)
        {
            this.File = file;
            this.Action = action;
            this.State = state;
            this.Progress = progress;
        }

        public FileEventArgs(FileInfo file, Action action, string error)
        {
            this.File = file;
            this.Action = action;
            this.State = State.Error;
            this.Error = error;
        }
    }
}
