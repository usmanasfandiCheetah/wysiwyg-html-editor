using HtmlAgilityPack;
using mages.editor.Converters;
using mages.editor.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mages.editor.Views
{
    /// <summary>
    /// Interaction logic for HtmlEditor.xaml
    /// </summary>
    public partial class HtmlEditor : UserControl
    {
        private EditorViewModel _viewModel;

        public EditorViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        public HtmlEditor()
        {
            InitializeComponent();
            if (this.ViewModel == null)
                this.ViewModel = new EditorViewModel();

            this.DataContext = this.ViewModel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}
