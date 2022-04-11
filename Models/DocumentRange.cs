using mages.framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace mages.editor.Models
{
    public class DocumentRange : BaseModel
    {
        private string _text;

        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
        private TextPointer _start;

        public TextPointer Start
        {
            get { return _start; }
            set { SetProperty(ref _start, value); }
        }

        private TextPointer _end;

        public TextPointer End
        {
            get { return _end; }
            set { SetProperty(ref _end, value); }
        }

        private DocumentStyle _textStyle;

        public DocumentRange(string text, TextPointer start, TextPointer end, DocumentStyle textStyle)
        {
            Text = text;
            Start = start;
            End = end;
            TextStyle = textStyle;
        }

        public DocumentRange()
        {
            Text = "";
            TextStyle = new DocumentStyle();
        }

        public DocumentStyle TextStyle
        {
            get { return _textStyle; }
            set { SetProperty(ref _textStyle, value); }
        }

        public void SetToolbar()
        {
            if (string.IsNullOrEmpty(Text))
                return;

            //if (SelectionStart == null || SelectionEnd == null)
            //    return;

            // Set font family combo
            var textRange = new TextRange(Start, End);
            var fontFamily = textRange.GetPropertyValue(TextElement.FontFamilyProperty);
            ////FontFamilyCombo.SelectedItem = fontFamily;

            //// Set font size combo
            var fontSize = textRange.GetPropertyValue(TextElement.FontSizeProperty);
            //FontSizeCombo.Text = fontSize.ToString();

            //// Set Font buttons
            if (!String.IsNullOrEmpty(textRange.Text))
            {
                TextStyle.IsBold = textRange.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold);
                TextStyle.IsItalic = textRange.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic);
                TextStyle.IsUnderlined = textRange.GetPropertyValue(Inline.TextDecorationsProperty).Equals(TextDecorations.Underline);
            }

            //// Set Alignment buttons
            TextStyle.IsLeftAligned = textRange.GetPropertyValue(FlowDocument.TextAlignmentProperty).Equals(TextAlignment.Left);
            TextStyle.IsCenterAligned = textRange.GetPropertyValue(FlowDocument.TextAlignmentProperty).Equals(TextAlignment.Center);
            TextStyle.IsRightAligned = textRange.GetPropertyValue(FlowDocument.TextAlignmentProperty).Equals(TextAlignment.Right);
            TextStyle.IsJustified = textRange.GetPropertyValue(FlowDocument.TextAlignmentProperty).Equals(TextAlignment.Justify);
        }

    }
}
