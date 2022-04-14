using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace lab_9.Models
{
    public class FileAndDirectoryTree : ReactiveObject
    {
        #region FIELDS

        #region PRIVATE

        private string __path;
        private string __name;
        private FileSystemWatcher? __watcher;
        private ObservableCollection<FileAndDirectoryTree>? __children;
        private bool __isExpanded;

        #endregion

        #region PUBLIC

        public bool IsChecked { get; set; }
        public FileAndDirectoryTree(string path,
        bool isRoot = false)
        {
            __path = path;
            if (!path.Contains(".jpg"))
            {
                var t = new DirectoryInfo(path);
                __name = t.Name;
            }
            else
            {
                __name = isRoot ? path : System.IO.Path.GetFileName(Path);
            }
            __isExpanded = isRoot;
        }
        public string Path
        {
            get => __path;
            private set => this.RaiseAndSetIfChanged(ref __path, value);
        }
        public string Name
        {
            get => __name;
            private set => this.RaiseAndSetIfChanged(ref __name, value);
        }
        public bool IsExpanded
        {
            get => __isExpanded;
            set => this.RaiseAndSetIfChanged(ref __isExpanded, value);
        }
        public bool IsDirectory { get; }
        public IReadOnlyList<FileAndDirectoryTree> Children => __children ??= LoadChildren();

        #endregion

        #endregion

        #region METHODS

        #region PRIVATE

        private ObservableCollection<FileAndDirectoryTree> LoadChildren()
        {
            var options = new EnumerationOptions { IgnoreInaccessible = true };
            var result = new ObservableCollection<FileAndDirectoryTree>();

            foreach (var d in Directory.EnumerateDirectories(Path, "*", options))
            {
                result.Add(new FileAndDirectoryTree(d, true));
            }

            foreach (var f in Directory.EnumerateFiles(Path, "*.jpg", options))
            {
                result.Add(new FileAndDirectoryTree(f, false));
            }
            __watcher = new FileSystemWatcher
            {
                Path = Path,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size | NotifyFilters.LastWrite,
            };
            __watcher.EnableRaisingEvents = true;
            return result;
        }

        #endregion

        #endregion
    }
}
