using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace NoteTaking
{
    class NoteList : INotifyPropertyChanged
    {
        private static NoteList _instance = null;

        public event PropertyChangedEventHandler PropertyChanged;
        public static NoteList Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NoteList();
                }
                return _instance;
            }
        }

        private ObservableCollection<Note> _noteList;

        public ObservableCollection<Note> noteList
        {
            get
            {
                return _noteList;
            }
            set
            {
                _noteList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("noteList"));
            }
        }

        public int lastChangedIndex { get; set; }
    }
}
