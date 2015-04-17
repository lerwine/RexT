using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erwine.Leonard.T.FolderSync
{
    public class FolderAccessManager : IDisposable
    {
        private FileSystemWatcher _fileSystemWatcher;

        public FolderAccessManager(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            if (path.Trim().Length == 0)
                throw new ArgumentException("Path cannot be empty.", "path");

            this._fileSystemWatcher = new FileSystemWatcher(path);
            try
            {
                this._fileSystemWatcher.Created += this.FileSystemWatcher_Created;
                this._fileSystemWatcher.Changed += this.FileSystemWatcher_Changed;
                this._fileSystemWatcher.Renamed += this.FileSystemWatcher_Renamed;
                this._fileSystemWatcher.Deleted += this.FileSystemWatcher_Deleted;
                this._fileSystemWatcher.Error += this.FileSystemWatcher_Error;
                this._fileSystemWatcher.EnableRaisingEvents = true;
            }
            catch
            {
                this._fileSystemWatcher.Dispose();
                throw;
            }
        }

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FileSystemWatcher_Error(object sender, ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
