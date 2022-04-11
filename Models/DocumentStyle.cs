using mages.framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace mages.editor.Models
{
    public class DocumentStyle : BaseModel
    {
        bool _isBold;
        bool _isItalic;
        bool _isUnderline;
        bool _isLeftAligned;
        bool _isRightAligned;
        bool _isCenterAligned;
        bool _isJustified;
        bool _isSubscript;
        bool _isSuperscript;

        public bool IsBold { get => _isBold; set => SetProperty(ref _isBold, value); }
        public bool IsItalic { get => _isItalic; set => SetProperty(ref _isItalic, value); }
        public bool IsUnderlined { get => _isUnderline; set => SetProperty(ref _isUnderline, value); }
        public bool IsLeftAligned { get => _isLeftAligned; set => SetProperty(ref _isLeftAligned, value); }
        public bool IsRightAligned { get => _isRightAligned; set => SetProperty(ref _isRightAligned, value); }
        public bool IsCenterAligned { get => _isCenterAligned; set => SetProperty(ref _isCenterAligned, value); }
        public bool IsJustified { get => _isJustified; set => SetProperty(ref _isJustified, value); }
        public bool IsSubscript { get => _isSubscript; set => SetProperty(ref _isSubscript, value); }
        public bool IsSuperscript { get => _isSuperscript; set => SetProperty(ref _isSuperscript, value); }
    }
}
