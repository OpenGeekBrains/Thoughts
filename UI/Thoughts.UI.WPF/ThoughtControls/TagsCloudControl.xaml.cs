using System;
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
        /// <summary>Path to Text propperty of TextBlock</summary>
        public static DependencyProperty TagTextBlockProperty = DependencyProperty.Register(
                nameof(TagTextBlock),
                typeof(string),
                typeof(TagsCloudControl),
                new PropertyMetadata(default(object)));
        /// <summary>Path to Text propperty of TextBlock</summary>
        //[Category("")]
        [Description("Path to Text propperty of TextBlock")]


        public string TagTextBlock
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



        public TagsCloudControl()
        {
            InitializeComponent();
        }
    }
}
