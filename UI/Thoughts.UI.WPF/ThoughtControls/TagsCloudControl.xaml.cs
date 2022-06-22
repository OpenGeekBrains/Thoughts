using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Thoughts.UI.WPF.ThoughtControls
{
    /// <summary>
    /// Логика взаимодействия для TagsCloudControl.xaml
    /// </summary>
    public partial class TagsCloudControl : UserControl
    {

        #region TagTextBlock: string - Path to Text propperty of TextBlock
        /// <summary>Path to Text property of TextBlock</summary>
        public static DependencyProperty TagTextBlockProperty = DependencyProperty.Register(
                nameof(TagTextBlock),
                typeof(string),
                typeof(TagsCloudControl),
                new PropertyMetadata(default(object)));
        /// <summary>Path to Text property of TextBlock</summary>
        //[Category("")]
        [Description("Path to Text propperty of TextBlock")]


        public IEnumerable TagTextBlock
        {
            get => (string)GetValue(TagTextBlockProperty);
            set => SetValue(TagTextBlockProperty, value);
        }
        #endregion


        #region RemoveButtonCommand: ICommand - Dependency property for controls button.
        /// <summary>Dependency property for controls button.</summary>
        public static DependencyProperty RemoveButtonCommandProperty = DependencyProperty.Register(
                nameof(RemoveButtonCommand),
                typeof(ICommand),
                typeof(TagsCloudControl),
                new PropertyMetadata(default(object)));
        /// <summary>Dependency property for controls button.</summary>
        //[Category("")]
        [Description("Dependency property for controls button.")]
        public ICommand RemoveButtonCommand
        {
            get => (ICommand)GetValue(RemoveButtonCommandProperty);
            set => SetValue(RemoveButtonCommandProperty, value);
        }
        #endregion


        #region TagsSource: ICollection - ItemsSource property of ListBox
        /// <summary>ItemsSource property of ListBox</summary>
        public static DependencyProperty TagsSourceProperty = DependencyProperty.Register(
                nameof(TagsSource),
                typeof(ICollection),
                typeof(TagsCloudControl),
                new PropertyMetadata(default(object)));
        /// <summary>ItemsSource property of ListBox</summary>
        //[Category("")]
        [Description("ItemsSource property of ListBox")]
        public ICollection TagsSource
        {
            get => (ICollection)GetValue(TagsSourceProperty);
            set => SetValue(TagsSourceProperty, value);
        }
        #endregion


        #region SelectedPostTagsSource: ICollection - ItemsSource of selected post tags.
        /// <summary>summary</summary>
        public static DependencyProperty SelectedPostTagsSourceProperty = DependencyProperty.Register(
                nameof(SelectedPostTagsSource),
                typeof(ICollection),
                typeof(TagsCloudControl),
                new PropertyMetadata(default(object)));
        /// <summary>summary</summary>
        //[Category("")]
        [Description("summary")]
        public ICollection SelectedPostTagsSource
        {
            get => (ICollection)GetValue(SelectedPostTagsSourceProperty);
            set => SetValue(SelectedPostTagsSourceProperty, value);
        }
        #endregion




        public TagsCloudControl()
        {
            InitializeComponent();
        }
    }
}
