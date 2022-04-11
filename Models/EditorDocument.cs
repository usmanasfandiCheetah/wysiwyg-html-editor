using mages.framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace mages.editor.Models
{
    public class EditorDocument : BaseModel
    {
        public event EventHandler OnTextChanged;
        string _text;
        DocumentStyle _currentStyle;
        DocumentStyle _defaultStyle;

        public string Text { 
            get => _text; set => SetProperty(ref _text, value); }
        public DocumentStyle CurrentStyle { get => _currentStyle; set => SetProperty(ref _currentStyle, value); }
        public DocumentStyle DefaultStyle { get => _defaultStyle; set => SetProperty(ref _defaultStyle, value); }

        public EditorDocument()
        {
            Text = "";
            CurrentStyle = new DocumentStyle();
            DefaultStyle = new DocumentStyle();
        }



    }
}
