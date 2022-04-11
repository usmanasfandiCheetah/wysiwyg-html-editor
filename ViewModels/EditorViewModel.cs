using mages.editor.Converters;
using mages.editor.Models;
using mages.framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace mages.editor.ViewModels
{
    public class EditorViewModel : BaseViewModel
    {

        public event EventHandler OnDocumentChanged;
        DocumentStyle _style;
        DocumentMode _mode;
        FontFamily _selectedFont;
        TextSelection _selectedContent;
        OptionSet _selectedSymbol;

        private string _editorContent;

        public string EditorContent
        {
            get => _editorContent;
            set
            {
                //if (!string.IsNullOrEmpty(value))
                //{
                //    if (value.Contains("\\rtf"))
                //    {
                //        value = RtfPipe.Rtf.ToHtml(value);
                        
                //    }
                //}
                value = EditorParser.ToHtml(value);
                SetProperty(ref _editorContent, value);
                OnDocumentChanged?.Invoke(this, EventArgs.Empty);
            }
        }


        private double _selectedFontSize;

        public double SelectedFontSize
        {
            get => _selectedFontSize;
            set
            {
                SetProperty(ref _selectedFontSize, value);
                SelectedContent.ApplyPropertyValue(Inline.FontSizeProperty, value);
            }
        }

        public FontFamily SelectedFont
        {
            get => _selectedFont;
            set
            {
                SetProperty(ref _selectedFont, value);
                if (SelectedContent != null)
                    SelectedContent.ApplyPropertyValue(TextElement.FontFamilyProperty, value);
            }
        }

        public OptionSet SelectedSymbol {
            get => _selectedSymbol;
            set
            {
                SetProperty(ref _selectedSymbol, value);
                //Clipboard.GetData(value.Value);
                SelectedContent.Text = value.Value;
                
            }
        }

        public bool PlainView { get; set; }
        public bool CodeView { get; set; }
        public TextSelection SelectedContent
        {
            get => _selectedContent;
            set
            {
                SetProperty(ref _selectedContent, value);
                SetToolbar();
            }
        }
        public List<OptionSet> TextSymbols { get; set; }
        public List<FontFamily> AvailableFonts { get; set; }
        public List<double> FontSizes { get; set; }
        
        public DocumentStyle DocStyle { get => _style; set => SetProperty(ref _style, value); }
        public DocumentMode DocMode { get => _mode; set => SetProperty(ref _mode, value); }

        public EditorViewModel()
        {
            TextSymbols = new List<OptionSet>();
            TextSymbols.Add(new OptionSet("Copyright", "©"));
            TextSymbols.Add(new OptionSet("Euro Sign", "€"));
            TextSymbols.Add(new OptionSet("Pound Sign", "£"));
            TextSymbols.Add(new OptionSet("Registered", "®"));
            TextSymbols.Add(new OptionSet("Trademark", "™"));
            TextSymbols.Add(new OptionSet("Pi", "π"));

            //DocumentText = "<FlowDocument xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"><Paragraph Foreground=\"Red\"><Bold>Hello, this is RichTextBox</Bold></Paragraph></FlowDocument>";
            //EditorDoc = new EditorDocument();
            EditorContent = "";
            DocStyle = new DocumentStyle();
            DocMode = new DocumentMode();           
            AvailableFonts = new List<FontFamily>();
            var list = Fonts.SystemFontFamilies;
            AvailableFonts.AddRange(list);
            
            FontSizes = new List<double>() { 5, 6, 7, 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };

        }

        public void SetToolbar()
        {
            if (SelectedContent == null)
                return;

            // Set font family combo
            try
            {
                var fontFamily = (FontFamily)SelectedContent.GetPropertyValue(TextElement.FontFamilyProperty);
                SelectedFont = fontFamily;

                // Set font size combo
                var fontSize = SelectedContent.GetPropertyValue(TextElement.FontSizeProperty);
                SelectedFontSize = (double)fontSize;
            }
            catch (Exception)
            {

            }            

            //// Set Styles
            DocStyle.IsBold = SelectedContent.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold);
            DocStyle.IsItalic = SelectedContent.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic);
            DocStyle.IsUnderlined = SelectedContent.GetPropertyValue(Inline.TextDecorationsProperty).Equals(TextDecorations.Underline);
            
            //// Set Alignment buttons
            DocStyle.IsLeftAligned = SelectedContent.GetPropertyValue(FlowDocument.TextAlignmentProperty).Equals(TextAlignment.Left);
            DocStyle.IsCenterAligned = SelectedContent.GetPropertyValue(FlowDocument.TextAlignmentProperty).Equals(TextAlignment.Center);
            DocStyle.IsRightAligned = SelectedContent.GetPropertyValue(FlowDocument.TextAlignmentProperty).Equals(TextAlignment.Right);
            DocStyle.IsJustified = SelectedContent.GetPropertyValue(FlowDocument.TextAlignmentProperty).Equals(TextAlignment.Justify);
        }
    }
}
