using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTaking
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUp
    {
        bool edit { get; set; }
        public PopUp(bool edit)
        {
            InitializeComponent();
            this.edit = edit;
            if (edit)
            {
                title.Text = NoteList.Instance.noteList[NoteList.Instance.lastChangedIndex].title;
                content.Text = NoteList.Instance.noteList[NoteList.Instance.lastChangedIndex].content;
            }
        }

        private void Confirm(object sender, EventArgs e)
        {
            titleError.IsVisible = false;
            SQLiteAsyncConnection connection = DependencyService.Get<SQLiteInterface>().GetConnection();

            if (title.Text == "" || title.Text == null)
            {
                titleError.IsVisible = true;
                return;
            }

            Note newNote = new Note()
            {
                title = title.Text,
                content = content.Text,
                date = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
            };

            if (edit)
            {
                Note editNote = NoteList.Instance.noteList[NoteList.Instance.lastChangedIndex];
                NoteList.Instance.noteList.Remove(editNote);
                connection.DeleteAsync(editNote);
                NoteList.Instance.noteList.Insert(0, newNote);
            }
            else
            {
                NoteList.Instance.noteList.Insert(0,newNote);
            }           
            connection.InsertAsync(newNote);
        }
    }
}