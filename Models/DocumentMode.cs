using mages.framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace mages.editor.Models
{
    public class DocumentMode : BaseModel
    {
        private bool _isTextMode;

        public bool IsTextMode
        {
            get { return _isTextMode; }
            set { SetProperty(ref _isTextMode, value); }
        }

        private bool _isHtmlMode;

        public bool IsHtmlMode
        {
            get { return _isHtmlMode; }
            set { SetProperty(ref _isHtmlMode, value); }
        }

        public DocumentMode()
        {
            IsTextMode = true;
            IsHtmlMode = false;
        }

        public void SetMode(bool plainView)
        {
            IsTextMode = plainView;
            IsHtmlMode = !plainView;
        }
    }
}
