using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Interactivity;

namespace mages.editor.Converters
{
    public class RichTextSelectionBehavior : Behavior<RichTextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += RichTextBoxSelectionChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SelectionChanged -= RichTextBoxSelectionChanged;
        }

        void RichTextBoxSelectionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            SelectedContent = AssociatedObject.Selection;
        }


        public TextSelection SelectedContent
        {
            get { return (TextSelection)GetValue(SelectedContentProperty); }
            set { SetValue(SelectedContentProperty, value); }
        }

        public static readonly DependencyProperty SelectedContentProperty =
            DependencyProperty.Register(
                "SelectedContent",
                typeof(TextSelection),
                typeof(RichTextSelectionBehavior),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedContentChanged));

        private static void OnSelectedContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as RichTextSelectionBehavior;
            if (behavior == null)
                return;

            if (behavior.SelectedContent == null)
                return;

            behavior.AssociatedObject.Selection.Text = behavior.SelectedContent.Text;
        }
    }
}
