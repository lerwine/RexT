using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Erwine.Leonard.T.RexT.ViewModel.Home
{
    public class EvalResultsViewModel : DependencyObject
    {
        #region Input Property Members

        public const string PropertyName_Input = "Input";

        public static readonly DependencyProperty InputProperty =
            DependencyProperty.Register(EvalResultsViewModel.PropertyName_Input, typeof(InputTextItem), typeof(EvalResultsViewModel),
                new PropertyMetadata(null));

        public InputTextItem Input
        {
            get { return (InputTextItem)(this.GetValue(EvalResultsViewModel.InputProperty)); }
            set { this.SetValue(EvalResultsViewModel.InputProperty, value); }
        }

        #endregion

        #region Results Property Members

        public const string PropertyName_Results = "Results";

        public static readonly DependencyProperty ResultsProperty =
            DependencyProperty.Register(EvalResultsViewModel.PropertyName_Results, typeof(ObservableCollection<EvaluationResultItem>), typeof(EvalResultsViewModel),
                new PropertyMetadata(null));

        public ObservableCollection<EvaluationResultItem> Results
        {
            get { return (ObservableCollection<EvaluationResultItem>)(this.GetValue(EvalResultsViewModel.ResultsProperty)); }
            set { this.SetValue(EvalResultsViewModel.ResultsProperty, value); }
        }

        #endregion

        #region Operation Property Members

        public const string PropertyName_Operation = "Operation";

        public static readonly DependencyProperty OperationProperty =
            DependencyProperty.Register(EvalResultsViewModel.PropertyName_Operation, typeof(DataModel.OperationType), typeof(EvalResultsViewModel),
                new PropertyMetadata(DataModel.OperationType.SingleMatch));

        public DataModel.OperationType Operation
        {
            get { return (DataModel.OperationType)(this.GetValue(EvalResultsViewModel.OperationProperty)); }
            set { this.SetValue(EvalResultsViewModel.OperationProperty, value); }
        }

        #endregion

        public EvalResultsViewModel() : base() { }

        public EvalResultsViewModel(PageViewModel pageViewModel, InputTextItem input, Regex regex, IEnumerable<Match> matches, DataModel.OperationType operationType) :
            this(input, matches.Select(m => new EvaluationResultItem(pageViewModel, m, regex)), operationType) { }

        public EvalResultsViewModel(PageViewModel pageViewModel, InputTextItem input, IEnumerable<string> results, DataModel.OperationType operationType) :
            this(input, results.Select(r => new EvaluationResultItem(pageViewModel, r)), operationType) { }

        private EvalResultsViewModel(InputTextItem input, IEnumerable<EvaluationResultItem> items, DataModel.OperationType operationType)
        {
            this.Input = input;
            this.Results = new ObservableCollection<EvaluationResultItem>(items);
            this.Operation = operationType;
        }
    }
}
