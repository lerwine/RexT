using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace Erwine.Leonard.T.RexT.ViewModel
{
    public class RegexOptionsViewModel : ReadOnlyObservableCollection<RegexOptionItem>
    {
        public event EventHandler ValueChanged;

        private RegexOptions _value = RegexOptions.None;

        public RegexOptions Value
        {
            get { return this._value; }
            set
            {
                RegexOptions oldValue = this._value;
                if (oldValue == value)
                    return;

                this._value = value;

                this.OnValueChanged(oldValue, value);

                this.DisplayText = (value == RegexOptions.None) ? "None" : String.Join(", ", this.Select(i => i.DisplayText).ToArray());

                this.OnPropertyChanged(new PropertyChangedEventArgs("Value"));
                this.OnPropertyChanged(new PropertyChangedEventArgs("DisplayText"));
            }
        }

        public string DisplayText { get; private set; }

        public RegexOptionsViewModel()
            : base(new ObservableCollection<RegexOptionItem>(Enum.GetValues(typeof(RegexOptions)).OfType<RegexOptions>().Where(o => o != RegexOptions.None).Select(o => new RegexOptionItem(o))))
        {
            foreach (RegexOptionItem item in this)
                item.SelectedChanged += this.Item_SelectedChanged;

            this.OnValueChanged(RegexOptions.None, this.Value);
        }

        private bool _ignoreSelectedChanged = false;

        private void OnValueChanged(RegexOptions oldValue, RegexOptions newValue)
        {
            if (this._ignoreSelectedChanged)
                return;

            this._ignoreSelectedChanged = true;
            try
            {
                if (newValue == RegexOptions.None)
                {
                    foreach (RegexOptionItem item in this.Where(i => i.IsSelected))
                        item.IsSelected = false;
                }
                else
                {
                    foreach (RegexOptionItem item in this.Where(i => i.IsSelected))
                        item.IsSelected = this.Value.HasFlag(item.Value);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                this._ignoreSelectedChanged = false;
            }

            if (this.ValueChanged != null)
                this.ValueChanged(this, EventArgs.Empty);
        }

        private void Item_SelectedChanged(object sender, EventArgs e)
        {
            if (this._ignoreSelectedChanged)
                return;

            this._ignoreSelectedChanged = true;
            try
            {
                this.Value = this.Where(i => i.IsSelected).Aggregate<RegexOptionItem, RegexOptions>(RegexOptions.None, (RegexOptions x, RegexOptionItem y) => x | y.Value);
            }
            catch
            {
                throw;
            }
            finally
            {
                this._ignoreSelectedChanged = false;
            }

            if (this.ValueChanged != null)
                this.ValueChanged(this, EventArgs.Empty);
        }
    }
}
