using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Erwine.Leonard.T.RexT.ViewModel.Home
{
    public class TextDetailLineViewModel : DependencyObject
    {
        #region LineNumber Property Members

        public const string PropertyName_LineNumber = "LineNumber";

        public static readonly DependencyProperty LineNumberProperty =
            DependencyProperty.Register(TextDetailLineViewModel.PropertyName_LineNumber, typeof(int), typeof(TextDetailLineViewModel),
                new PropertyMetadata(0));

        public int LineNumber
        {
            get { return (int)(this.GetValue(TextDetailLineViewModel.LineNumberProperty)); }
            set { this.SetValue(TextDetailLineViewModel.LineNumberProperty, value); }
        }

        #endregion

        #region Characters Property Members

        public const string PropertyName_Characters = "Characters";

        public static readonly DependencyProperty CharactersProperty =
            DependencyProperty.Register(TextDetailLineViewModel.PropertyName_Characters, typeof(ObservableCollection<TextDetailCharViewModel>), typeof(TextDetailLineViewModel),
                new PropertyMetadata(null));

        public ObservableCollection<TextDetailCharViewModel> Characters
        {
            get { return (ObservableCollection<TextDetailCharViewModel>)(this.GetValue(TextDetailLineViewModel.CharactersProperty)); }
            set { this.SetValue(TextDetailLineViewModel.CharactersProperty, value); }
        }

        #endregion

        #region Content Property Members

        public const string PropertyName_Content = "Content";

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(TextDetailLineViewModel.PropertyName_Content, typeof(string), typeof(TextDetailLineViewModel),
                new PropertyMetadata(""));

        public string Content
        {
            get { return this.GetValue(TextDetailLineViewModel.ContentProperty) as string; }
            set { this.SetValue(TextDetailLineViewModel.ContentProperty, value); }
        }

        #endregion

        public TextDetailLineViewModel() { }

        public TextDetailLineViewModel(int lineNumber, string content)
        {
            this.LineNumber = lineNumber;
            this.Content = content;
            if (this.Characters == null)
                this.Characters = new ObservableCollection<TextDetailCharViewModel>();
            else
                this.Characters.Clear();

            for (int i = 0; i < content.Length; i++)
                this.Characters.Add(new TextDetailCharViewModel(i, content[i]));
        }
    }
}
