using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Erwine.Leonard.T.RexT.Views
{
    public partial class NestableNodeControl : UserControl
    {
        public NestableNodeControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private Dictionary<int, CancellationTokenSource> _tokenSources = new Dictionary<int, CancellationTokenSource>();
        private Task _updateHighlightingTask = null;
        public static readonly Regex GroupingRegex = new Regex(RegexPatterns.RegexGroupParse, RegexOptions.IgnorePatternWhitespace);
        private void UpdateSyntacticalHighlighting(object parameter)
        {
            object[] args = parameter as object[];
            List<Tuple<TextPointer, TextPointer, string>> contents = args[0] as List<Tuple<TextPointer, TextPointer, string>>;
            Task previousTask = args[1] as Task;
            CancellationToken token = (CancellationToken)(args[2]);

            if (previousTask != null)
            {
                lock (this._tokenSources)
                {
                    if (this._tokenSources.ContainsKey(previousTask.Id))
                    {
                        CancellationTokenSource previousTokenSource = this._tokenSources[previousTask.Id];
                        this._tokenSources.Remove(previousTask.Id);
                        previousTokenSource.Cancel();
                    }
                }

                if (!token.IsCancellationRequested)
                    previousTask.Wait();
            }

            if (token.IsCancellationRequested)
                return;

            int position = 0;
            var positionData = contents.Select(c =>
            {
                var result = new { StartPointer = c.Item1, EndPointer = c.Item2, StartPosition = position, Length = c.Item3.Length };
                position += c.Item3.Length;
                return result;
            }).ToArray();
            string allText = String.Join("", contents.Select(c => c.Item3).ToArray());

            MatchCollection matches = NestableNodeControl.GroupingRegex.Matches(allText);

            foreach (Match m in matches)
            {
                if (token.IsCancellationRequested)
                    break;

                var pos = positionData.TakeWhile(p => p.StartPosition <= m.Index).ToArray();
                positionData = positionData.Skip(pos.Length).ToArray();
                if (pos.Length > 1)
                    this.Dispatcher.BeginInvoke(new SetTextBoldDelegate(this.SetTextBold), pos[0].StartPointer, pos[pos.Length - 2].EndPointer, token, false);
                var lp = pos.Last();
                this.Dispatcher.BeginInvoke(new SetTextBoldDelegate(this.SetTextBold), lp.StartPointer, lp.EndPointer, token, true);
            }

            if (positionData.Length > 0)
                this.Dispatcher.BeginInvoke(new SetTextBoldDelegate(this.SetTextBold), positionData[0].StartPointer, positionData[positionData.Length - 1].EndPointer, token, false);
        }

        private void SetTextBold(TextPointer start, TextPointer end, CancellationToken token, bool isBold)
        {
            lock (this._tokenSources)
            {
                if (!token.IsCancellationRequested)
                {
                    TextPointer originalStart = this.RegexTextBox.Selection.Start;
                    TextPointer originalEnd = this.RegexTextBox.Selection.End;
                    this.RegexTextBox.Selection.Select((start == null) ? this.RegexTextBox.ContentStart : start, (end == null) ? this.RegexTextBox.ContentEnd : end);
                    System.Windows.FontWeight? fw = this.RegexTextBox.Selection.GetPropertyValue(Run.FontWeightProperty) as System.Windows.FontWeight?;
                    if (!fw.HasValue || isBold != (fw.Value == System.Windows.FontWeights.Bold))
                        this.RegexTextBox.Selection.ApplyPropertyValue(Run.FontWeightProperty, (isBold) ? System.Windows.FontWeights.Bold : System.Windows.FontWeights.Normal);
                    this.RegexTextBox.Selection.Select(originalStart, originalEnd);
                }
            }
        }

        public delegate void SetTextBoldDelegate(TextPointer start, TextPointer end, CancellationToken token, bool isBold);

        private void TaskCleanup(Task task)
        {
            lock (this._tokenSources)
            {
                CancellationTokenSource cts = this._tokenSources[task.Id];
                this._tokenSources.Remove(task.Id);
                cts.Dispose();
            }
        }

        private void RegexTextBox_ContentChanged(object sender, ContentChangedEventArgs e)
        {
            lock (this._tokenSources)
            {
                TextPointer start = this.RegexTextBox.Selection.Start;
                TextPointer end = this.RegexTextBox.Selection.End;
                List<Tuple<TextPointer, TextPointer, string>> contents = new List<Tuple<TextPointer, TextPointer, string>>();
                TextPointer tempS = this.RegexTextBox.ContentStart;
                for (TextPointer tempE = tempS.GetNextInsertionPosition(LogicalDirection.Forward); tempE != null; tempE = tempS.GetNextInsertionPosition(LogicalDirection.Forward))
                {
                    this.RegexTextBox.Selection.Select(tempS, tempE);
                    contents.Add(new Tuple<TextPointer, TextPointer, string>(tempS, tempE, this.RegexTextBox.Selection.Text));
                    tempS = tempE;
                }
                this.RegexTextBox.Selection.Select(start, end);
                CancellationTokenSource ts = new CancellationTokenSource();
                this._updateHighlightingTask = Task.Factory.StartNew(new Action<object>(this.UpdateSyntacticalHighlighting),
                    new object[] { contents, this._updateHighlightingTask, ts.Token } as object, ts.Token);
                this._tokenSources.Add(this._updateHighlightingTask.Id, ts);
                this._updateHighlightingTask.ContinueWith(this.TaskCleanup);
            }
        }

        private void RegexTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //ResultsDataContext dataContext = this.ResultsDataGrid.DataContext as ResultsDataContext;
            //dataContext.Items.Add(new Tuple<DateTime, string, string>(DateTime.Now, "RegexTextBox_SelectionChanged",
            //    String.Format("this.RegexTextBox.Selection.Start = {0}, this.RegexTextBox.Selection.End = {1}", this.RegexTextBox.Selection.GetPropertyValue(, this.RegexTextBox.Selection.End)));
        }

        private void RegexTextBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            ResultsDataContext dataContext = this.ResultsDataGrid.DataContext as ResultsDataContext;
            dataContext.Items.Add(new Tuple<DateTime, string, string>(DateTime.Now, "RegexTextBox_TextInput",
                String.Format("e.Text = {0}", e.Text)));
        }

        private void RegexTextBox_TextInputStart(object sender, TextCompositionEventArgs e)
        {
            ResultsDataContext dataContext = this.ResultsDataGrid.DataContext as ResultsDataContext;
            dataContext.Items.Add(new Tuple<DateTime, string, string>(DateTime.Now, "RegexTextBox_TextInputStart",
                String.Format("e.Text = {0}", e.Text)));
        }

        private void RegexTextBox_TextInputUpdate(object sender, TextCompositionEventArgs e)
        {
            ResultsDataContext dataContext = this.ResultsDataGrid.DataContext as ResultsDataContext;
            dataContext.Items.Add(new Tuple<DateTime, string, string>(DateTime.Now, "RegexTextBox_TextInputUpdate",
                String.Format("e.Text = {0}", e.Text)));
        }
    }
}
