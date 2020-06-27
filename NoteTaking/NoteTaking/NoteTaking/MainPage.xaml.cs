using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoteTaking
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        readonly SQLiteAsyncConnection connection = DependencyService.Get<SQLiteInterface>().GetConnection();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new NoteList();
            listView.BindingContext = NoteList.Instance.noteList;
            setup();
        }

        private async void setup()
        {
            await connection.CreateTableAsync<Note>();
            await LoadNotes();
        }

        private async Task LoadNotes()
        {            
            NoteList.Instance.noteList = new ObservableCollection<Note>(connection.Table<Note>().ToListAsync().Result.OrderByDescending(order => DateTime.Parse(order.date)));
                       
        }

        private async void AddNoteAsync(object sender, EventArgs e)
        {
            bool edit = false;
            await PopupNavigation.Instance.PushAsync(new PopUp(edit));
        }


        private void UpdateLastModified(object sender, ItemTappedEventArgs e)
        {
            var clickedNote = e.Item as Note;
            foreach (Note note in NoteList.Instance.noteList)
            {
                if (note == clickedNote)
                {
                    note.date = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                    connection.UpdateAsync(note);
                    NoteList.Instance.noteList = new ObservableCollection<Note>(NoteList.Instance.noteList.ToList().OrderByDescending(order => DateTime.Parse(order.date)));
                }
            }           
        }

        private void Delete(object sender, EventArgs e)
        {
            var menu = sender as MenuItem;
            Note note = menu.CommandParameter as Note;
            NoteList.Instance.noteList.Remove(note);
            connection.DeleteAsync(note);
        }

        private async void EditAsync(object sender, EventArgs e)
        {
            bool edit = true;
            int i = 0;
            var menu = sender as MenuItem;
            Note editNote = menu.CommandParameter as Note;
            foreach(Note note in NoteList.Instance.noteList)
            {
                if(note == editNote)
                {
                    NoteList.Instance.lastChangedIndex = i;
                }
                i++;
            }
            await PopupNavigation.Instance.PushAsync(new PopUp(edit));
        }
    }
}
